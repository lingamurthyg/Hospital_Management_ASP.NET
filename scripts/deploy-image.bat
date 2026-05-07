@echo off
setlocal enabledelayedexpansion

echo =====================================
echo CareTracker Application - AWS ECS Fargate Deployment
echo =====================================
echo.

set PROJECT_NAME=caretracker-application
set TASK_FAMILY=!PROJECT_NAME!-task
set SERVICE_NAME=!PROJECT_NAME!-service

echo === AWS Configuration ===
set /p AWS_REGION="Enter AWS Region (e.g., us-east-1): "
set /p CLUSTER_NAME="Enter ECS Cluster Name (e.g., caretracker-cluster): "

echo.
echo === Network Configuration ===
set /p VPC_ID="Enter VPC ID (e.g., vpc-0abc123def456): "
set /p SUBNETS_INPUT="Enter Subnet IDs comma-separated (e.g., subnet-0abc123,subnet-0def456): "
set /p SECURITY_GROUP="Enter Security Group ID (e.g., sg-0abc123def): "

for /f "tokens=1,2 delims=," %%a in ("!SUBNETS_INPUT!") do (
    set SUBNET_1=%%a
    set SUBNET_2=%%b
)
if "!SUBNET_2!"=="" set SUBNET_2=!SUBNET_1!

echo.
echo === Container Configuration ===
set /p IMAGE_URI="Enter Docker Image URI (e.g., 123456789.dkr.ecr.us-east-1.amazonaws.com/caretracker-application:latest): "

echo.
echo === Database Configuration ===
set /p DB_HOST="Enter Database Host (e.g., rds-instance.region.rds.amazonaws.com): "
set /p DB_USER="Enter Database Username: "
set /p DB_PASSWORD="Enter Database Password: "

echo.
echo === Load Balancer Configuration ===
set /p NEED_LB="Do you need a load balancer for this service? (y/n): "

if /i "!NEED_LB!"=="y" (
    echo Creating Application Load Balancer and Target Group...
    
    set ALB_NAME=!PROJECT_NAME!-alb
    echo Creating ALB: !ALB_NAME!
    
    for /f "delims=" %%i in ('aws elbv2 create-load-balancer --name !ALB_NAME! --subnets !SUBNET_1! !SUBNET_2! --security-groups !SECURITY_GROUP! --scheme internet-facing --type application --ip-address-type ipv4 --region !AWS_REGION! --query "LoadBalancers[0].LoadBalancerArn" --output text') do set ALB_ARN=%%i
    
    if "!ALB_ARN!"=="" (
        echo ERROR: Failed to create Application Load Balancer
        exit /b 1
    )
    
    echo ALB created: !ALB_ARN!
    
    for /f "delims=" %%i in ('aws elbv2 describe-load-balancers --load-balancer-arns !ALB_ARN! --region !AWS_REGION! --query "LoadBalancers[0].DNSName" --output text') do set ALB_DNS=%%i
    
    set TG_NAME=!PROJECT_NAME!-tg
    echo Creating Target Group: !TG_NAME!
    
    for /f "delims=" %%i in ('aws elbv2 create-target-group --name !TG_NAME! --protocol HTTP --port 80 --vpc-id !VPC_ID! --target-type ip --health-check-enabled --health-check-protocol HTTP --health-check-path "/SignUp.aspx" --health-check-interval-seconds 30 --health-check-timeout-seconds 10 --healthy-threshold-count 2 --unhealthy-threshold-count 3 --region !AWS_REGION! --query "TargetGroups[0].TargetGroupArn" --output text') do set TARGET_GROUP_ARN=%%i
    
    if "!TARGET_GROUP_ARN!"=="" (
        echo ERROR: Failed to create Target Group
        exit /b 1
    )
    
    echo Target Group created: !TARGET_GROUP_ARN!
    
    echo Creating ALB Listener...
    aws elbv2 create-listener --load-balancer-arn !ALB_ARN! --protocol HTTP --port 80 --default-actions Type=forward,TargetGroupArn=!TARGET_GROUP_ARN! --region !AWS_REGION! >nul
    
    echo ALB Listener created successfully
) else (
    echo Skipping load balancer configuration.
)

echo.
echo === Getting AWS Account ID ===
for /f "delims=" %%i in ('aws sts get-caller-identity --query Account --output text') do set ACCOUNT_ID=%%i
echo Account ID: !ACCOUNT_ID!

echo.
echo === Checking ECS Cluster ===
aws ecs describe-clusters --clusters !CLUSTER_NAME! --region !AWS_REGION! >nul 2>&1
if !ERRORLEVEL! neq 0 (
    echo Cluster does not exist. Creating ECS cluster: !CLUSTER_NAME!
    aws ecs create-cluster --cluster-name !CLUSTER_NAME! --region !AWS_REGION!
)

echo Cluster '!CLUSTER_NAME!' is ready.

echo.
echo === Creating CloudWatch Log Group ===
set LOG_GROUP=/ecs/!PROJECT_NAME!
aws logs create-log-group --log-group-name !LOG_GROUP! --region !AWS_REGION! 2>nul || echo Log group already exists.

echo.
echo === Preparing Task Definition ===
copy ecs\task-definition.json ecs\task-definition-deployed.json >nul

powershell -Command "(Get-Content ecs\task-definition-deployed.json) -replace '{{IMAGE_URI}}', '!IMAGE_URI!' | Set-Content ecs\task-definition-deployed.json"
powershell -Command "(Get-Content ecs\task-definition-deployed.json) -replace '{{AWS_REGION}}', '!AWS_REGION!' | Set-Content ecs\task-definition-deployed.json"
powershell -Command "(Get-Content ecs\task-definition-deployed.json) -replace '{{ACCOUNT_ID}}', '!ACCOUNT_ID!' | Set-Content ecs\task-definition-deployed.json"
powershell -Command "(Get-Content ecs\task-definition-deployed.json) -replace '{{DB_HOST}}', '!DB_HOST!' | Set-Content ecs\task-definition-deployed.json"
powershell -Command "(Get-Content ecs\task-definition-deployed.json) -replace '{{DB_USER}}', '!DB_USER!' | Set-Content ecs\task-definition-deployed.json"
powershell -Command "(Get-Content ecs\task-definition-deployed.json) -replace '{{DB_PASSWORD}}', '!DB_PASSWORD!' | Set-Content ecs\task-definition-deployed.json"

echo Registering ECS Task Definition...
for /f "delims=" %%i in ('aws ecs register-task-definition --cli-input-json file://ecs/task-definition-deployed.json --region !AWS_REGION! --query "taskDefinition.taskDefinitionArn" --output text') do set TASK_DEF_ARN=%%i

if "!TASK_DEF_ARN!"=="" (
    echo ERROR: Failed to register task definition
    exit /b 1
)

echo Task Definition registered: !TASK_DEF_ARN!

echo.
echo === Preparing Service Definition ===
copy ecs\service-definition.json ecs\service-definition-deployed.json >nul

powershell -Command "(Get-Content ecs\service-definition-deployed.json) -replace '{{CLUSTER_NAME}}', '!CLUSTER_NAME!' | Set-Content ecs\service-definition-deployed.json"
powershell -Command "(Get-Content ecs\service-definition-deployed.json) -replace '{{SUBNET_1}}', '!SUBNET_1!' | Set-Content ecs\service-definition-deployed.json"
powershell -Command "(Get-Content ecs\service-definition-deployed.json) -replace '{{SUBNET_2}}', '!SUBNET_2!' | Set-Content ecs\service-definition-deployed.json"
powershell -Command "(Get-Content ecs\service-definition-deployed.json) -replace '{{SECURITY_GROUP}}', '!SECURITY_GROUP!' | Set-Content ecs\service-definition-deployed.json"

if /i "!NEED_LB!"=="y" (
    powershell -Command "(Get-Content ecs\service-definition-deployed.json) -replace '{{TARGET_GROUP_ARN}}', '!TARGET_GROUP_ARN!' | Set-Content ecs\service-definition-deployed.json"
) else (
    powershell -Command "$json = Get-Content ecs\service-definition-deployed.json | ConvertFrom-Json; $json.PSObject.Properties.Remove('loadBalancers'); $json.PSObject.Properties.Remove('healthCheckGracePeriodSeconds'); $json | ConvertTo-Json -Depth 10 | Set-Content ecs\service-definition-deployed.json"
)

echo.
echo === Checking if Service Exists ===
for /f "delims=" %%i in ('aws ecs describe-services --cluster !CLUSTER_NAME! --services !SERVICE_NAME! --region !AWS_REGION! --query "services[?status=='ACTIVE'].serviceName" --output text') do set SERVICE_EXISTS=%%i

if "!SERVICE_EXISTS!"=="" (
    echo Service does not exist. Creating new service...
    aws ecs create-service --cli-input-json file://ecs/service-definition-deployed.json --region !AWS_REGION!
) else (
    echo Service exists. Updating service with new task definition...
    aws ecs update-service --cluster !CLUSTER_NAME! --service !SERVICE_NAME! --task-definition !TASK_DEF_ARN! --force-new-deployment --region !AWS_REGION!
)

echo.
echo === Waiting for Service Stability ===
echo This may take several minutes...
aws ecs wait services-stable --cluster !CLUSTER_NAME! --services !SERVICE_NAME! --region !AWS_REGION!

echo.
echo =====================================
echo Deployment Completed Successfully!
echo =====================================
echo.
echo Cluster: !CLUSTER_NAME!
echo Service: !SERVICE_NAME!
echo Task Definition: !TASK_DEF_ARN!
if /i "!NEED_LB!"=="y" (
    echo Load Balancer DNS: !ALB_DNS!
    echo Application URL: http://!ALB_DNS!
)
echo CloudWatch Logs: !LOG_GROUP!
echo.
echo To view service details:
echo   aws ecs describe-services --cluster !CLUSTER_NAME! --services !SERVICE_NAME! --region !AWS_REGION!
echo.
echo To view running tasks:
echo   aws ecs list-tasks --cluster !CLUSTER_NAME! --service-name !SERVICE_NAME! --region !AWS_REGION!
echo.

endlocal