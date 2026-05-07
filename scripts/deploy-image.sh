#!/bin/bash
set -e
set -o pipefail

echo "====================================="
echo "CareTracker Application - AWS ECS Fargate Deployment"
echo "====================================="
echo ""

# Deployment configuration
PROJECT_NAME="caretracker-application"
TASK_FAMILY="${PROJECT_NAME}-task"
SERVICE_NAME="${PROJECT_NAME}-service"

echo "=== AWS Configuration ==="
read -p "Enter AWS Region (e.g., us-east-1): " AWS_REGION
read -p "Enter ECS Cluster Name (e.g., caretracker-cluster): " CLUSTER_NAME

echo ""
echo "=== Network Configuration ==="
read -p "Enter VPC ID (e.g., vpc-0abc123def456): " VPC_ID
read -p "Enter Subnet IDs comma-separated (e.g., subnet-0abc123,subnet-0def456): " SUBNETS_INPUT
read -p "Enter Security Group ID (e.g., sg-0abc123def): " SECURITY_GROUP

IFS=',' read -ra SUBNETS <<< "$SUBNETS_INPUT"
SUBNET_1="${SUBNETS[0]}"
SUBNET_2="${SUBNETS[1]:-$SUBNET_1}"

echo ""
echo "=== Container Configuration ==="
read -p "Enter Docker Image URI (e.g., 123456789.dkr.ecr.us-east-1.amazonaws.com/caretracker-application:latest): " IMAGE_URI

echo ""
echo "=== Database Configuration ==="
read -p "Enter Database Host (e.g., rds-instance.region.rds.amazonaws.com): " DB_HOST
read -p "Enter Database Username: " DB_USER
read -sp "Enter Database Password: " DB_PASSWORD
echo ""

echo ""
echo "=== Load Balancer Configuration ==="
read -p "Do you need a load balancer for this service? (y/n): " NEED_LB

if [[ "$NEED_LB" =~ ^[Yy]$ ]]; then
    echo "Creating Application Load Balancer and Target Group..."
    
    # Create ALB
    ALB_NAME="${PROJECT_NAME}-alb"
    echo "Creating ALB: $ALB_NAME"
    ALB_ARN=$(aws elbv2 create-load-balancer \
        --name "$ALB_NAME" \
        --subnets "$SUBNET_1" "$SUBNET_2" \
        --security-groups "$SECURITY_GROUP" \
        --scheme internet-facing \
        --type application \
        --ip-address-type ipv4 \
        --region "$AWS_REGION" \
        --query 'LoadBalancers[0].LoadBalancerArn' \
        --output text)
    
    if [ -z "$ALB_ARN" ]; then
        echo "ERROR: Failed to create Application Load Balancer"
        exit 1
    fi
    
    echo "ALB created: $ALB_ARN"
    
    # Get ALB DNS name
    ALB_DNS=$(aws elbv2 describe-load-balancers \
        --load-balancer-arns "$ALB_ARN" \
        --region "$AWS_REGION" \
        --query 'LoadBalancers[0].DNSName' \
        --output text)
    
    # Create Target Group with target-type ip (required for Fargate)
    TG_NAME="${PROJECT_NAME}-tg"
    echo "Creating Target Group: $TG_NAME"
    TARGET_GROUP_ARN=$(aws elbv2 create-target-group \
        --name "$TG_NAME" \
        --protocol HTTP \
        --port 80 \
        --vpc-id "$VPC_ID" \
        --target-type ip \
        --health-check-enabled \
        --health-check-protocol HTTP \
        --health-check-path "/SignUp.aspx" \
        --health-check-interval-seconds 30 \
        --health-check-timeout-seconds 10 \
        --healthy-threshold-count 2 \
        --unhealthy-threshold-count 3 \
        --region "$AWS_REGION" \
        --query 'TargetGroups[0].TargetGroupArn' \
        --output text)
    
    if [ -z "$TARGET_GROUP_ARN" ]; then
        echo "ERROR: Failed to create Target Group"
        exit 1
    fi
    
    echo "Target Group created: $TARGET_GROUP_ARN"
    
    # Create ALB Listener
    echo "Creating ALB Listener..."
    aws elbv2 create-listener \
        --load-balancer-arn "$ALB_ARN" \
        --protocol HTTP \
        --port 80 \
        --default-actions Type=forward,TargetGroupArn="$TARGET_GROUP_ARN" \
        --region "$AWS_REGION" > /dev/null
    
    echo "ALB Listener created successfully"
    
    LOAD_BALANCER_CONFIG='"loadBalancers": [{"targetGroupArn": "'"$TARGET_GROUP_ARN"'", "containerName": "'"$PROJECT_NAME"'", "containerPort": 80}],'
    HEALTH_CHECK_GRACE='"healthCheckGracePeriodSeconds": 300,'
else
    LOAD_BALANCER_CONFIG=''
    HEALTH_CHECK_GRACE=''
    echo "Skipping load balancer configuration."
fi

echo ""
echo "=== Getting AWS Account ID ==="
ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text)
echo "Account ID: $ACCOUNT_ID"

echo ""
echo "=== Checking ECS Cluster ==="
aws ecs describe-clusters --clusters "$CLUSTER_NAME" --region "$AWS_REGION" >/dev/null 2>&1 || {
    echo "Cluster does not exist. Creating ECS cluster: $CLUSTER_NAME"
    aws ecs create-cluster --cluster-name "$CLUSTER_NAME" --region "$AWS_REGION"
}

echo "Cluster '$CLUSTER_NAME' is ready."

echo ""
echo "=== Creating CloudWatch Log Group ==="
LOG_GROUP="/ecs/$PROJECT_NAME"
aws logs create-log-group --log-group-name "$LOG_GROUP" --region "$AWS_REGION" 2>/dev/null || echo "Log group already exists."

echo ""
echo "=== Preparing Task Definition ==="
cp ecs/task-definition.json ecs/task-definition-deployed.json

sed -i "s|{{IMAGE_URI}}|$IMAGE_URI|g" ecs/task-definition-deployed.json
sed -i "s|{{AWS_REGION}}|$AWS_REGION|g" ecs/task-definition-deployed.json
sed -i "s|{{ACCOUNT_ID}}|$ACCOUNT_ID|g" ecs/task-definition-deployed.json
sed -i "s|{{DB_HOST}}|$DB_HOST|g" ecs/task-definition-deployed.json
sed -i "s|{{DB_USER}}|$DB_USER|g" ecs/task-definition-deployed.json
sed -i "s|{{DB_PASSWORD}}|$DB_PASSWORD|g" ecs/task-definition-deployed.json

echo "Registering ECS Task Definition..."
TASK_DEF_ARN=$(aws ecs register-task-definition \
    --cli-input-json file://ecs/task-definition-deployed.json \
    --region "$AWS_REGION" \
    --query 'taskDefinition.taskDefinitionArn' \
    --output text)

if [ -z "$TASK_DEF_ARN" ]; then
    echo "ERROR: Failed to register task definition"
    exit 1
fi

echo "Task Definition registered: $TASK_DEF_ARN"

echo ""
echo "=== Preparing Service Definition ==="
cp ecs/service-definition.json ecs/service-definition-deployed.json

sed -i "s|{{CLUSTER_NAME}}|$CLUSTER_NAME|g" ecs/service-definition-deployed.json
sed -i "s|{{SUBNET_1}}|$SUBNET_1|g" ecs/service-definition-deployed.json
sed -i "s|{{SUBNET_2}}|$SUBNET_2|g" ecs/service-definition-deployed.json
sed -i "s|{{SECURITY_GROUP}}|$SECURITY_GROUP|g" ecs/service-definition-deployed.json

if [[ "$NEED_LB" =~ ^[Yy]$ ]]; then
    sed -i "s|{{TARGET_GROUP_ARN}}|$TARGET_GROUP_ARN|g" ecs/service-definition-deployed.json
else
    # Remove load balancer section if not needed
    jq 'del(.loadBalancers) | del(.healthCheckGracePeriodSeconds)' ecs/service-definition-deployed.json > ecs/service-definition-temp.json
    mv ecs/service-definition-temp.json ecs/service-definition-deployed.json
fi

echo ""
echo "=== Checking if Service Exists ==="
SERVICE_EXISTS=$(aws ecs describe-services \
    --cluster "$CLUSTER_NAME" \
    --services "$SERVICE_NAME" \
    --region "$AWS_REGION" \
    --query 'services[?status==`ACTIVE`].serviceName' \
    --output text)

if [ -z "$SERVICE_EXISTS" ] || [ "$SERVICE_EXISTS" == "None" ]; then
    echo "Service does not exist. Creating new service..."
    aws ecs create-service \
        --cli-input-json file://ecs/service-definition-deployed.json \
        --region "$AWS_REGION"
else
    echo "Service exists. Updating service with new task definition..."
    if [[ "$NEED_LB" =~ ^[Yy]$ ]]; then
        aws ecs update-service \
            --cluster "$CLUSTER_NAME" \
            --service "$SERVICE_NAME" \
            --task-definition "$TASK_DEF_ARN" \
            --force-new-deployment \
            --region "$AWS_REGION"
    else
        aws ecs update-service \
            --cluster "$CLUSTER_NAME" \
            --service "$SERVICE_NAME" \
            --task-definition "$TASK_DEF_ARN" \
            --force-new-deployment \
            --region "$AWS_REGION"
    fi
fi

echo ""
echo "=== Waiting for Service Stability ==="
echo "This may take several minutes..."
aws ecs wait services-stable --cluster "$CLUSTER_NAME" --services "$SERVICE_NAME" --region "$AWS_REGION"

echo ""
echo "====================================="
echo "Deployment Completed Successfully!"
echo "====================================="
echo ""
echo "Cluster: $CLUSTER_NAME"
echo "Service: $SERVICE_NAME"
echo "Task Definition: $TASK_DEF_ARN"
if [[ "$NEED_LB" =~ ^[Yy]$ ]]; then
    echo "Load Balancer DNS: $ALB_DNS"
    echo "Application URL: http://$ALB_DNS"
fi
echo "CloudWatch Logs: $LOG_GROUP"
echo ""
echo "To view service details:"
echo "  aws ecs describe-services --cluster $CLUSTER_NAME --services $SERVICE_NAME --region $AWS_REGION"
echo ""
echo "To view running tasks:"
echo "  aws ecs list-tasks --cluster $CLUSTER_NAME --service-name $SERVICE_NAME --region $AWS_REGION"
echo ""