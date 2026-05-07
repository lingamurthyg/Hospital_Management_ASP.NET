# CareTracker Application - AWS ECS Fargate Deployment Guide

## Table of Contents
1. [Overview](#overview)
2. [Prerequisites](#prerequisites)
3. [Technology Stack](#technology-stack)
4. [Local Development Setup](#local-development-setup)
5. [Building Docker Images](#building-docker-images)
6. [AWS ECS Fargate Prerequisites](#aws-ecs-fargate-prerequisites)
7. [ECS Fargate Setup](#ecs-fargate-setup)
8. [ECS Task Definition Explained](#ecs-task-definition-explained)
9. [ECS Service Configuration](#ecs-service-configuration)
10. [Database Setup](#database-setup)
11. [Deployment Walkthrough](#deployment-walkthrough)
12. [Post-Deployment Verification](#post-deployment-verification)
13. [Troubleshooting](#troubleshooting)
14. [Scaling and Management](#scaling-and-management)
15. [Security Considerations](#security-considerations)
16. [Monitoring and Logging](#monitoring-and-logging)
17. [Cost Optimization](#cost-optimization)

---

## Overview

CareTracker is a comprehensive Hospital/Clinic Management System built on ASP.NET Framework 4.5.2. This guide provides step-by-step instructions for containerizing and deploying the application to AWS ECS Fargate using Windows containers.

**Application Details:**
- **Framework**: ASP.NET Web Forms (.NET Framework 4.5.2)
- **Runtime**: IIS (Internet Information Services)
- **Database**: SQL Server
- **Port**: 80 (HTTP)
- **Container Type**: Windows Server 2019 Core

---

## Prerequisites

### Required Software
1. **Docker Desktop for Windows** (with Windows containers support)
   - Download: https://www.docker.com/products/docker-desktop
   - Ensure Windows containers mode is enabled
   - Minimum 8GB RAM, 20GB disk space

2. **AWS CLI v2**
   - Download: https://aws.amazon.com/cli/
   - Configure with credentials: `aws configure`

3. **Git** (for version control)
   - Download: https://git-scm.com/

### Required AWS Resources
1. **AWS Account** with appropriate permissions
2. **IAM User** with programmatic access
3. **VPC** with public/private subnets
4. **Security Groups** configured for ECS tasks
5. **RDS SQL Server Instance** (or external SQL Server)

### Required IAM Permissions
Your IAM user/role must have:
- `AmazonECS_FullAccess`
- `AmazonEC2ContainerRegistryFullAccess`
- `IAMFullAccess` (for creating ECS roles)
- `CloudWatchLogsFullAccess`
- `ElasticLoadBalancingFullAccess` (if using ALB)

---

## Technology Stack

### Application Stack
- **Framework**: ASP.NET Framework 4.5.2
- **Application Type**: Web Forms
- **Runtime**: IIS 10.0+
- **Language**: C# 6.0
- **Database**: SQL Server 2016+

### Container Stack
- **Base Image**: `mcr.microsoft.com/dotnet/framework/aspnet:4.8`
- **Build Image**: `mcr.microsoft.com/dotnet/framework/sdk:4.8`
- **OS**: Windows Server 2019 Core
- **Container Runtime**: Docker

### AWS Infrastructure
- **Compute**: AWS ECS Fargate (Windows containers)
- **Registry**: Amazon ECR
- **Load Balancer**: Application Load Balancer (ALB)
- **Database**: Amazon RDS for SQL Server
- **Logging**: CloudWatch Logs
- **Networking**: VPC with public/private subnets

---

## Local Development Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd "CareTracker Application 17"
```

### 2. Configure Database Connection
Update the connection string in `Code/DBProject/Web.config`:

```xml
<connectionStrings>
  <add name="sqlCon1" 
       connectionString="Data Source=localhost;Initial Catalog=DBProject;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 3. Set Up Database Schema
1. Install SQL Server Express or SQL Server Developer Edition
2. Run database scripts in order:
   ```sql
   -- Run in SQL Server Management Studio
   -- 1. Create database and tables
   :r Database Files\Schema.sql
   
   -- 2. Create stored procedures
   :r Database Files\Admin.sql
   :r Database Files\Doctor.sql
   :r Database Files\Patient.sql
   :r Database Files\SignUp.sql
   
   -- 3. Insert sample data
   :r Database Files\Insertions.sql
   ```

### 4. Build and Test Locally
1. Open `Code/Clinic Management System.sln` in Visual Studio
2. Restore NuGet packages: `Tools > NuGet Package Manager > Restore`
3. Build solution: `Build > Build Solution` (Ctrl+Shift+B)
4. Run application: Press F5
5. Navigate to: `http://localhost:1972/SignUp.aspx`

---

## Building Docker Images

### Understanding the Dockerfile

The Dockerfile uses a multi-stage build:

**Stage 1: Builder** (SDK Image)
- Installs NuGet for package restoration
- Restores dependencies
- Builds the application using MSBuild
- Publishes to output directory

**Stage 2: Runtime** (ASP.NET Image)
- Copies built application from builder
- Configures IIS and Application Pool
- Sets up environment variables
- Exposes port 80

### Building the Image

**Option 1: Using build-push.sh (Linux/macOS)**
```bash
chmod +x scripts/build-push.sh
./scripts/build-push.sh
```

**Option 2: Using build-push.bat (Windows)**
```cmd
scripts\build-push.bat
```

**Option 3: Manual Docker Build**
```bash
# Build the image
docker build -f Dockerfile -t caretracker-application:latest .

# Verify the image
docker images | grep caretracker
```

### Testing the Image Locally

```bash
# Run container with environment variables
docker run -d -p 8080:80 \
  -e DB_HOST=host.docker.internal \
  -e DB_USER=sa \
  -e DB_PASSWORD=YourPassword123 \
  --name caretracker-test \
  caretracker-application:latest

# Check logs
docker logs -f caretracker-test

# Test the application
curl http://localhost:8080/SignUp.aspx

# Stop and remove
docker stop caretracker-test
docker rm caretracker-test
```

---

## AWS ECS Fargate Prerequisites

### 1. Create VPC and Subnets

If you don't have a VPC, create one:

```bash
# Create VPC
VPC_ID=$(aws ec2 create-vpc \
  --cidr-block 10.0.0.0/16 \
  --query 'Vpc.VpcId' \
  --output text)

echo "VPC ID: $VPC_ID"

# Enable DNS hostnames
aws ec2 modify-vpc-attribute \
  --vpc-id $VPC_ID \
  --enable-dns-hostnames

# Create Internet Gateway
IGW_ID=$(aws ec2 create-internet-gateway \
  --query 'InternetGateway.InternetGatewayId' \
  --output text)

aws ec2 attach-internet-gateway \
  --vpc-id $VPC_ID \
  --internet-gateway-id $IGW_ID

# Create public subnets (two for high availability)
SUBNET_1=$(aws ec2 create-subnet \
  --vpc-id $VPC_ID \
  --cidr-block 10.0.1.0/24 \
  --availability-zone us-east-1a \
  --query 'Subnet.SubnetId' \
  --output text)

SUBNET_2=$(aws ec2 create-subnet \
  --vpc-id $VPC_ID \
  --cidr-block 10.0.2.0/24 \
  --availability-zone us-east-1b \
  --query 'Subnet.SubnetId' \
  --output text)

echo "Subnet 1: $SUBNET_1"
echo "Subnet 2: $SUBNET_2"

# Create route table and associate with subnets
RTB_ID=$(aws ec2 create-route-table \
  --vpc-id $VPC_ID \
  --query 'RouteTable.RouteTableId' \
  --output text)

aws ec2 create-route \
  --route-table-id $RTB_ID \
  --destination-cidr-block 0.0.0.0/0 \
  --gateway-id $IGW_ID

aws ec2 associate-route-table \
  --subnet-id $SUBNET_1 \
  --route-table-id $RTB_ID

aws ec2 associate-route-table \
  --subnet-id $SUBNET_2 \
  --route-table-id $RTB_ID
```

### 2. Create Security Group

```bash
# Create security group
SG_ID=$(aws ec2 create-security-group \
  --group-name caretracker-sg \
  --description "Security group for CareTracker ECS tasks" \
  --vpc-id $VPC_ID \
  --query 'GroupId' \
  --output text)

echo "Security Group ID: $SG_ID"

# Allow inbound HTTP (port 80)
aws ec2 authorize-security-group-ingress \
  --group-id $SG_ID \
  --protocol tcp \
  --port 80 \
  --cidr 0.0.0.0/0

# Allow inbound HTTPS (port 443) - optional
aws ec2 authorize-security-group-ingress \
  --group-id $SG_ID \
  --protocol tcp \
  --port 443 \
  --cidr 0.0.0.0/0

# Allow outbound to SQL Server (port 1433)
aws ec2 authorize-security-group-egress \
  --group-id $SG_ID \
  --protocol tcp \
  --port 1433 \
  --cidr 10.0.0.0/16
```

### 3. Create IAM Roles

**ECS Task Execution Role** (required for Fargate):

```bash
# Create trust policy
cat > ecs-task-execution-trust-policy.json <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": "ecs-tasks.amazonaws.com"
      },
      "Action": "sts:AssumeRole"
    }
  ]
}
EOF

# Create role
aws iam create-role \
  --role-name ecsTaskExecutionRole \
  --assume-role-policy-document file://ecs-task-execution-trust-policy.json

# Attach managed policy
aws iam attach-role-policy \
  --role-name ecsTaskExecutionRole \
  --policy-arn arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy
```

**ECS Task Role** (for application permissions):

```bash
# Create task role
aws iam create-role \
  --role-name ecsTaskRole \
  --assume-role-policy-document file://ecs-task-execution-trust-policy.json

# Attach policies as needed (e.g., S3 access, Secrets Manager)
aws iam attach-role-policy \
  --role-name ecsTaskRole \
  --policy-arn arn:aws:iam::aws:policy/AmazonS3ReadOnlyAccess
```

### 4. Create ECR Repository

```bash
# Create ECR repository
aws ecr create-repository \
  --repository-name caretracker-application \
  --region us-east-1

# Get repository URI
REPO_URI=$(aws ecr describe-repositories \
  --repository-names caretracker-application \
  --query 'repositories[0].repositoryUri' \
  --output text)

echo "ECR Repository URI: $REPO_URI"
```

### 5. Create RDS SQL Server Instance

```bash
# Create DB subnet group
aws rds create-db-subnet-group \
  --db-subnet-group-name caretracker-db-subnet \
  --db-subnet-group-description "Subnet group for CareTracker database" \
  --subnet-ids $SUBNET_1 $SUBNET_2

# Create RDS instance
aws rds create-db-instance \
  --db-instance-identifier caretracker-db \
  --db-instance-class db.t3.small \
  --engine sqlserver-ex \
  --engine-version 15.00 \
  --master-username admin \
  --master-user-password YourSecurePassword123 \
  --allocated-storage 20 \
  --vpc-security-group-ids $SG_ID \
  --db-subnet-group-name caretracker-db-subnet \
  --backup-retention-period 7 \
  --no-publicly-accessible

# Wait for instance to be available (takes 10-15 minutes)
aws rds wait db-instance-available \
  --db-instance-identifier caretracker-db

# Get endpoint
DB_ENDPOINT=$(aws rds describe-db-instances \
  --db-instance-identifier caretracker-db \
  --query 'DBInstances[0].Endpoint.Address' \
  --output text)

echo "Database Endpoint: $DB_ENDPOINT"
```

---

## ECS Fargate Setup

### 1. Create ECS Cluster

```bash
aws ecs create-cluster \
  --cluster-name caretracker-cluster \
  --region us-east-1
```

### 2. Create CloudWatch Log Group

```bash
aws logs create-log-group \
  --log-group-name /ecs/caretracker-application \
  --region us-east-1
```

---

## ECS Task Definition Explained

### Key Components

**1. Launch Type Configuration**
```json
"requiresCompatibilities": ["FARGATE"],
"networkMode": "awsvpc"
```
- `FARGATE`: Serverless compute engine
- `awsvpc`: Each task gets its own ENI (Elastic Network Interface)

**2. CPU and Memory (Critical)**

Fargate requires specific CPU/memory combinations:

| CPU (vCPU) | Valid Memory (GB) |
|------------|-------------------|
| 0.25       | 0.5, 1, 2         |
| 0.5        | 1, 2, 3, 4        |
| 1          | 2, 3, 4, 5, 6, 7, 8 |
| 2          | 4-16 (increments of 1) |
| 4          | 8-30 (increments of 1) |

For Windows containers, we use:
```json
"cpu": "1024",  // 1 vCPU
"memory": "2048" // 2 GB
```

**3. Runtime Platform (Windows Containers)**
```json
"runtimePlatform": {
  "operatingSystemFamily": "WINDOWS_SERVER_2019_CORE",
  "cpuArchitecture": "X86_64"
}
```

**4. Container Definition**
```json
"containerDefinitions": [{
  "name": "caretracker-application",
  "image": "{{IMAGE_URI}}",
  "essential": true,
  "portMappings": [{"containerPort": 80, "protocol": "tcp"}],
  "environment": [...],
  "logConfiguration": {...},
  "healthCheck": {...}
}]
```

**5. Health Check**
```json
"healthCheck": {
  "command": ["CMD-SHELL", "powershell -Command ..."],
  "interval": 30,
  "timeout": 10,
  "retries": 3,
  "startPeriod": 120
}
```
- Uses PowerShell to check if the application responds on port 80
- `startPeriod`: 120 seconds for IIS warmup

---

## ECS Service Configuration

### Key Service Parameters

**1. Desired Count**
```json
"desiredCount": 2
```
- Runs 2 tasks for high availability
- Adjust based on load requirements

**2. Deployment Configuration**
```json
"deploymentConfiguration": {
  "maximumPercent": 200,
  "minimumHealthyPercent": 50,
  "deploymentCircuitBreaker": {
    "enable": true,
    "rollback": true
  }
}
```
- `maximumPercent`: Can run up to 4 tasks during deployment (2 x 200%)
- `minimumHealthyPercent`: Must keep at least 1 task healthy (2 x 50%)
- `deploymentCircuitBreaker`: Auto-rollback on failure

**3. Network Configuration**
```json
"networkConfiguration": {
  "awsvpcConfiguration": {
    "subnets": ["subnet-xxx", "subnet-yyy"],
    "securityGroups": ["sg-xxx"],
    "assignPublicIp": "ENABLED"
  }
}
```
- Uses `awsvpc` mode (required for Fargate)
- Public IP needed if tasks are in public subnets

**4. Load Balancer Integration**
```json
"loadBalancers": [{
  "targetGroupArn": "arn:aws:elasticloadbalancing:...",
  "containerName": "caretracker-application",
  "containerPort": 80
}],
"healthCheckGracePeriodSeconds": 300
```
- Registers tasks with ALB target group
- Grace period allows IIS to start before health checks

---

## Database Setup

### 1. Connect to RDS Instance

Use SQL Server Management Studio (SSMS) or Azure Data Studio:

```
Server: <RDS_ENDPOINT>
Port: 1433
Authentication: SQL Server Authentication
Username: admin
Password: <YOUR_PASSWORD>
```

### 2. Create Database and Schema

Run the database setup scripts:

```sql
-- Execute in order
:r Schema.sql
:r Admin.sql
:r Doctor.sql
:r Patient.sql
:r SignUp.sql
:r Insertions.sql
```

### 3. Update Connection String

The Dockerfile handles connection string replacement using environment variables:

```
DB_HOST=caretracker-db.xxx.us-east-1.rds.amazonaws.com
DB_USER=admin
DB_PASSWORD=YourSecurePassword123
```

---

## Deployment Walkthrough

### Step 1: Build and Push Image

```bash
# Run the build script
./scripts/build-push.sh

# Follow prompts:
# 1. Enter image tag: v1.0.0
# 2. Select registry: 1 (AWS ECR)
# 3. Enter AWS region: us-east-1
# 4. Enter account ID: 123456789012
# 5. Enter repo name: caretracker-application
```

### Step 2: Deploy to ECS

```bash
# Run the deployment script
./scripts/deploy-image.sh

# Follow prompts:
# 1. AWS Region: us-east-1
# 2. Cluster name: caretracker-cluster
# 3. VPC ID: vpc-xxx
# 4. Subnets: subnet-xxx,subnet-yyy
# 5. Security group: sg-xxx
# 6. Image URI: 123456789012.dkr.ecr.us-east-1.amazonaws.com/caretracker-application:v1.0.0
# 7. DB Host: caretracker-db.xxx.us-east-1.rds.amazonaws.com
# 8. DB User: admin
# 9. DB Password: <password>
# 10. Need load balancer: y
```

The script will:
1. Create ALB and target group (if requested)
2. Register task definition
3. Create or update ECS service
4. Wait for service to stabilize
5. Display deployment results

### Step 3: Verify Deployment

```bash
# Check service status
aws ecs describe-services \
  --cluster caretracker-cluster \
  --services caretracker-application-service \
  --region us-east-1

# List running tasks
aws ecs list-tasks \
  --cluster caretracker-cluster \
  --service-name caretracker-application-service \
  --region us-east-1

# Get task details
TASK_ARN=$(aws ecs list-tasks \
  --cluster caretracker-cluster \
  --service-name caretracker-application-service \
  --query 'taskArns[0]' \
  --output text)

aws ecs describe-tasks \
  --cluster caretracker-cluster \
  --tasks $TASK_ARN \
  --region us-east-1
```

---

## Post-Deployment Verification

### 1. Access the Application

If using Application Load Balancer:

```bash
# Get ALB DNS name
ALB_DNS=$(aws elbv2 describe-load-balancers \
  --names caretracker-application-alb \
  --query 'LoadBalancers[0].DNSName' \
  --output text)

echo "Application URL: http://$ALB_DNS"
```

Open in browser: `http://<ALB_DNS>/SignUp.aspx`

### 2. Test Application Features

1. **Login Page**: Verify SignUp.aspx loads
2. **Admin Login**: Use sample credentials from Insertions.sql
3. **Patient Portal**: Test appointment booking
4. **Doctor Portal**: Verify patient history access

### 3. Check Application Logs

```bash
# View CloudWatch logs
aws logs tail /ecs/caretracker-application \
  --follow \
  --region us-east-1
```

### 4. Test Health Checks

```bash
# Check target group health
TARGET_GROUP_ARN=$(aws elbv2 describe-target-groups \
  --names caretracker-application-tg \
  --query 'TargetGroups[0].TargetGroupArn' \
  --output text)

aws elbv2 describe-target-health \
  --target-group-arn $TARGET_GROUP_ARN \
  --region us-east-1
```

Healthy status: `State: healthy`

---

## Troubleshooting

### Common Issues

#### 1. Task Fails to Start

**Error**: "CannotPullContainerError"

**Solution**:
- Verify ECR repository exists and image is pushed
- Check task execution role has ECR permissions
- Verify image URI is correct in task definition

```bash
# Verify image exists
aws ecr describe-images \
  --repository-name caretracker-application \
  --region us-east-1
```

#### 2. Task Starts but Unhealthy

**Error**: "Target.FailedHealthChecks"

**Solution**:
- Check CloudWatch logs for application errors
- Verify database connection string is correct
- Ensure security group allows inbound traffic on port 80
- Check health check path is accessible

```bash
# Get task logs
aws logs tail /ecs/caretracker-application --follow
```

#### 3. Database Connection Failures

**Error**: "Cannot open database 'DBProject'"

**Solution**:
- Verify database exists on RDS instance
- Check security group allows traffic from ECS tasks to RDS (port 1433)
- Verify RDS endpoint is correct
- Test connection from task:

```bash
# Exec into running task
aws ecs execute-command \
  --cluster caretracker-cluster \
  --task $TASK_ARN \
  --container caretracker-application \
  --interactive \
  --command "powershell"

# Test database connection
Test-NetConnection -ComputerName $env:DB_HOST -Port 1433
```

#### 4. High Memory/CPU Usage

**Solution**:
- Increase task CPU/memory in task definition
- Use Application Insights to identify bottlenecks
- Optimize database queries
- Enable connection pooling

```bash
# Update task definition with more resources
# Edit ecs/task-definition.json
"cpu": "2048",  // 2 vCPU
"memory": "4096" // 4 GB

# Register new revision
aws ecs register-task-definition \
  --cli-input-json file://ecs/task-definition.json

# Update service
aws ecs update-service \
  --cluster caretracker-cluster \
  --service caretracker-application-service \
  --task-definition caretracker-application-task
```

#### 5. Deployment Failures

**Error**: "Service was unable to place a task"

**Solution**:
- Verify subnets have available IP addresses
- Check Fargate capacity in region
- Ensure valid CPU/memory combination
- Review IAM role permissions

---

## Scaling and Management

### Manual Scaling

```bash
# Scale to 5 tasks
aws ecs update-service \
  --cluster caretracker-cluster \
  --service caretracker-application-service \
  --desired-count 5
```

### Auto Scaling

Configure Service Auto Scaling:

```bash
# Register scalable target
aws application-autoscaling register-scalable-target \
  --service-namespace ecs \
  --scalable-dimension ecs:service:DesiredCount \
  --resource-id service/caretracker-cluster/caretracker-application-service \
  --min-capacity 2 \
  --max-capacity 10

# Create scaling policy (target tracking)
aws application-autoscaling put-scaling-policy \
  --service-namespace ecs \
  --scalable-dimension ecs:service:DesiredCount \
  --resource-id service/caretracker-cluster/caretracker-application-service \
  --policy-name caretracker-cpu-scaling \
  --policy-type TargetTrackingScaling \
  --target-tracking-scaling-policy-configuration file://scaling-policy.json
```

scaling-policy.json:
```json
{
  "TargetValue": 70.0,
  "PredefinedMetricSpecification": {
    "PredefinedMetricType": "ECSServiceAverageCPUUtilization"
  },
  "ScaleInCooldown": 300,
  "ScaleOutCooldown": 60
}
```

### Blue/Green Deployments

For zero-downtime deployments, use CodeDeploy:

```bash
# Update service to use CODE_DEPLOY
aws ecs update-service \
  --cluster caretracker-cluster \
  --service caretracker-application-service \
  --deployment-controller type=CODE_DEPLOY
```

---

## Security Considerations

### 1. Use Secrets Manager for Sensitive Data

```bash
# Store database password in Secrets Manager
aws secretsmanager create-secret \
  --name caretracker/db-password \
  --secret-string "YourSecurePassword123"

# Reference in task definition
"secrets": [
  {
    "name": "DB_PASSWORD",
    "valueFrom": "arn:aws:secretsmanager:us-east-1:123456789012:secret:caretracker/db-password"
  }
]
```

### 2. Enable AWS WAF (Web Application Firewall)

```bash
# Create WAF web ACL
aws wafv2 create-web-acl \
  --name caretracker-waf \
  --scope REGIONAL \
  --default-action Allow={} \
  --rules file://waf-rules.json

# Associate with ALB
aws wafv2 associate-web-acl \
  --web-acl-arn <WAF_ACL_ARN> \
  --resource-arn <ALB_ARN>
```

### 3. Enable Container Insights

```bash
aws ecs put-account-setting-default \
  --name containerInsights \
  --value enabled
```

### 4. Use Private Subnets with NAT Gateway

For production, place ECS tasks in private subnets:
- Create NAT Gateway in public subnet
- Route private subnet traffic through NAT
- Set `assignPublicIp: DISABLED`

---

## Monitoring and Logging

### CloudWatch Metrics

Monitor key metrics:
- `CPUUtilization`
- `MemoryUtilization`
- `TargetResponseTime`
- `HealthyHostCount`
- `UnhealthyHostCount`

```bash
# Create CloudWatch dashboard
aws cloudwatch put-dashboard \
  --dashboard-name CareTracker \
  --dashboard-body file://dashboard.json
```

### CloudWatch Alarms

```bash
# High CPU alarm
aws cloudwatch put-metric-alarm \
  --alarm-name caretracker-high-cpu \
  --alarm-description "Alert when CPU exceeds 80%" \
  --metric-name CPUUtilization \
  --namespace AWS/ECS \
  --statistic Average \
  --period 300 \
  --threshold 80 \
  --comparison-operator GreaterThanThreshold \
  --evaluation-periods 2 \
  --dimensions Name=ServiceName,Value=caretracker-application-service Name=ClusterName,Value=caretracker-cluster
```

### Application Insights Integration

The application includes Microsoft Application Insights. Configure connection string:

```xml
<!-- In ApplicationInsights.config -->
<InstrumentationKey>your-instrumentation-key</InstrumentationKey>
```

Or use environment variable:
```
APPINSIGHTS_INSTRUMENTATIONKEY=your-key
```

---

## Cost Optimization

### 1. Right-Size Tasks

- Start with minimal resources (1 vCPU, 2 GB)
- Monitor utilization and adjust
- Avoid over-provisioning

### 2. Use Fargate Spot (for non-critical workloads)

```json
"capacityProviderStrategy": [
  {
    "capacityProvider": "FARGATE_SPOT",
    "weight": 1
  },
  {
    "capacityProvider": "FARGATE",
    "weight": 1,
    "base": 1
  }
]
```

Savings: Up to 70% compared to Fargate on-demand

### 3. Schedule Non-Production Environments

```bash
# Stop development environment at night
aws ecs update-service \
  --cluster caretracker-dev-cluster \
  --service caretracker-dev-service \
  --desired-count 0

# Start in the morning
aws ecs update-service \
  --cluster caretracker-dev-cluster \
  --service caretracker-dev-service \
  --desired-count 2
```

### 4. Optimize Docker Image Size

- Use multi-stage builds
- Minimize layers
- Remove unnecessary files
- Use Windows Server Core instead of Full

---

## Conclusion

You now have a production-ready CareTracker application running on AWS ECS Fargate with:

✓ Containerized ASP.NET Framework application
✓ Automated build and deployment scripts
✓ High availability with multiple tasks
✓ Load balancing with Application Load Balancer
✓ Centralized logging with CloudWatch
✓ Auto-scaling capabilities
✓ Security best practices

### Next Steps

1. **Configure HTTPS**: Add SSL/TLS certificate to ALB
2. **Set up CI/CD**: Integrate with AWS CodePipeline
3. **Implement monitoring**: Configure CloudWatch dashboards and alarms
4. **Enable backups**: Configure RDS automated backups
5. **Disaster recovery**: Set up multi-region deployment

### Support and Resources

- AWS ECS Documentation: https://docs.aws.amazon.com/ecs/
- AWS Fargate Pricing: https://aws.amazon.com/fargate/pricing/
- ASP.NET Framework Containers: https://hub.docker.com/_/microsoft-dotnet-framework-aspnet
- Application Insights: https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net

---

**Document Version**: 1.0.0  
**Last Updated**: 2026-05-07  
**Maintained By**: DevOps Team