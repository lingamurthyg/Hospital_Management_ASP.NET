# Clinic Management System - Deployment Guide

## Overview

This guide provides comprehensive instructions for containerizing and deploying the Clinic Management System (.NET Framework 4.5.2 ASP.NET Web Forms application) to AWS EKS (Elastic Kubernetes Service).

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Project Analysis](#project-analysis)
3. [Local Development Setup](#local-development-setup)
4. [Docker Build and Push](#docker-build-and-push)
5. [AWS EKS Deployment](#aws-eks-deployment)
6. [Configuration Management](#configuration-management)
7. [Troubleshooting](#troubleshooting)
8. [Security Considerations](#security-considerations)
9. [Scaling and Management](#scaling-and-management)

## Prerequisites

### Required Tools

- **Docker Desktop for Windows** (20.10 or later) - Required for Windows containers
- **AWS CLI** (v2.x) - [Installation Guide](https://aws.amazon.com/cli/)
- **kubectl** (v1.24+) - [Installation Guide](https://kubernetes.io/docs/tasks/tools/)
- **eksctl** (optional) - For EKS cluster creation
- **Git** - For source code management

### AWS Requirements

- AWS Account with appropriate permissions
- IAM permissions for:
  - EKS cluster management
  - ECR repository access
  - VPC and networking resources
  - IAM role creation
- AWS Windows node groups in EKS cluster (required for .NET Framework)

### System Requirements

- Windows Server 2019 or later (for building Windows containers)
- 8GB RAM minimum
- 50GB available disk space
- Internet connectivity

## Project Analysis

### Technology Stack

- **Framework**: .NET Framework 4.5.2
- **Application Type**: ASP.NET Web Forms
- **Build Tool**: MSBuild
- **Database**: SQL Server (connection string in Web.config)
- **Dependencies**: 
  - Microsoft.ApplicationInsights
  - System.Web
  - System.Data.SqlClient

### Application Details

- **Port**: 80 (HTTP)
- **Health Endpoint**: /SignUp.aspx
- **Configuration Files**: Web.config, ApplicationInsights.config
- **Database Connection**: Configured in Web.config connectionStrings section

## Local Development Setup

### 1. Clone Repository

```bash
git clone <repository-url>
cd FullApp
```

### 2. Review Configuration

Check `Code/DBProject/Web.config` for database connection settings:

```xml
<connectionStrings>
  <add name="sqlCon1" 
       connectionString="Data Source=.\SQLEXPRESS; Initial Catalog=DBProject; Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 3. Build with Docker Compose

```bash
docker-compose up -d
```

Access the application at: http://localhost:8080

### 4. Stop Local Environment

```bash
docker-compose down
```

## Docker Build and Push

### Using Build Scripts

#### Linux/macOS:

```bash
chmod +x scripts/build-push.sh
./scripts/build-push.sh
```

#### Windows:

```cmd
scripts\build-push.bat
```

### Script Features

1. **Registry Selection**: Choose between AWS ECR or Docker Hub
2. **Automatic Authentication**: Handles AWS ECR and Docker Hub login
3. **Repository Creation**: Auto-creates ECR repositories if they don't exist
4. **Tag Management**: Prompts for image tags with validation
5. **Error Handling**: Validates each step with clear error messages

### Manual Docker Build

```bash
# Build image
docker build -t clinic-management-system:latest -f Dockerfile .

# Tag for registry
docker tag clinic-management-system:latest <registry-url>/clinic-management-system:v1.0

# Push to registry
docker push <registry-url>/clinic-management-system:v1.0
```

### AWS ECR Manual Setup

```bash
# Authenticate with ECR
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin <account-id>.dkr.ecr.us-east-1.amazonaws.com

# Create repository
aws ecr create-repository --repository-name clinic-management-system --region us-east-1

# Build and push
docker build -t <account-id>.dkr.ecr.us-east-1.amazonaws.com/clinic-management-system:latest .
docker push <account-id>.dkr.ecr.us-east-1.amazonaws.com/clinic-management-system:latest
```

## AWS EKS Deployment

### Prerequisites

1. **EKS Cluster with Windows Node Groups**

```bash
# Create EKS cluster with Windows support
exksctl create cluster \
  --name clinic-cluster \
  --region us-east-1 \
  --node-type t3.large \
  --nodes 2 \
  --nodes-min 2 \
  --nodes-max 4 \
  --with-oidc \
  --managed

# Add Windows node group
exksctl create nodegroup \
  --cluster clinic-cluster \
  --region us-east-1 \
  --name windows-ng \
  --node-type t3.large \
  --nodes 2 \
  --nodes-min 2 \
  --nodes-max 4 \
  --node-ami-family WindowsServer2019FullContainer
```

2. **Install AWS Load Balancer Controller**

```bash
# Add IAM policy
curl -o iam_policy.json https://raw.githubusercontent.com/kubernetes-sigs/aws-load-balancer-controller/v2.4.4/docs/install/iam_policy.json
aws iam create-policy --policy-name AWSLoadBalancerControllerIAMPolicy --policy-document file://iam_policy.json

# Install controller
kubectl apply -k "github.com/aws/eks-charts/stable/aws-load-balancer-controller//crds?ref=master"
helm repo add eks https://aws.github.io/eks-charts
helm install aws-load-balancer-controller eks/aws-load-balancer-controller \
  -n kube-system \
  --set clusterName=clinic-cluster \
  --set serviceAccount.create=false \
  --set serviceAccount.name=aws-load-balancer-controller
```

### Deployment Steps

#### Using Deployment Scripts

**Linux/macOS:**

```bash
chmod +x scripts/deploy-image.sh
./scripts/deploy-image.sh
```

**Windows:**

```cmd
scripts\deploy-image.bat
```

#### Manual Deployment

1. **Configure kubectl**

```bash
aws eks update-kubeconfig --region us-east-1 --name clinic-cluster
kubectl cluster-info
```

2. **Update Kubernetes Manifests**

Edit `kubernetes/deployment.yaml` and replace placeholders:

- `{{IMAGE_URI}}`: Your Docker image URI
- `{{DB_HOST}}`: Database host address
- `{{DB_NAME}}`: Database name
- `{{DB_USER}}`: Database username

3. **Create Database Secret** (if using password authentication)

```bash
kubectl create secret generic clinic-db-secret \
  --from-literal=password='YourDatabasePassword' \
  -n clinic-management-system
```

4. **Apply Manifests**

```bash
# Create namespace
kubectl apply -f kubernetes/namespace.yaml

# Deploy application
kubectl apply -f kubernetes/deployment.yaml

# Create service
kubectl apply -f kubernetes/service.yaml

# Create ingress
kubectl apply -f kubernetes/ingress.yaml
```

5. **Verify Deployment**

```bash
# Check deployment status
kubectl rollout status deployment/clinic-management-system -n clinic-management-system

# View resources
kubectl get pods,svc,ingress -n clinic-management-system

# View logs
kubectl logs -f deployment/clinic-management-system -n clinic-management-system
```

6. **Get Application URL**

```bash
kubectl get ingress -n clinic-management-system
```

The ALB DNS name will be displayed in the ADDRESS column.

## Configuration Management

### Environment Variables

The application supports the following environment variables:

- `ASPNET_ENVIRONMENT`: Application environment (Production, Development)
- `DB_HOST`: Database server host
- `DB_NAME`: Database name
- `DB_USER`: Database username
- `DB_PASSWORD`: Database password (stored in Kubernetes secret)

### Database Connection

Update the connection string in Web.config or use environment variables:

```yaml
env:
- name: DB_CONNECTION_STRING
  value: "Data Source=$(DB_HOST);Initial Catalog=$(DB_NAME);User ID=$(DB_USER);Password=$(DB_PASSWORD)"
```

### ConfigMaps

For additional configuration:

```bash
kubectl create configmap clinic-config \
  --from-file=Web.config=Web.config \
  -n clinic-management-system
```

## Troubleshooting

### Common Issues

#### 1. Pods Not Starting

**Symptoms**: Pods stuck in Pending or CrashLoopBackOff state

**Solutions**:

```bash
# Check pod events
kubectl describe pod <pod-name> -n clinic-management-system

# Check logs
kubectl logs <pod-name> -n clinic-management-system

# Verify Windows node selector
kubectl get nodes -l kubernetes.io/os=windows
```

#### 2. Database Connection Failures

**Symptoms**: Application crashes with database connection errors

**Solutions**:

- Verify database host is accessible from EKS cluster
- Check database credentials in secrets
- Ensure security groups allow SQL Server traffic (port 1433)
- Test connectivity from a pod:

```bash
kubectl run -it --rm debug --image=mcr.microsoft.com/windows/servercore:ltsc2019 --restart=Never -- powershell
# Inside pod:
Test-NetConnection -ComputerName <db-host> -Port 1433
```

#### 3. Ingress Not Working

**Symptoms**: Cannot access application via ingress URL

**Solutions**:

```bash
# Check ingress status
kubectl describe ingress clinic-management-system-ingress -n clinic-management-system

# Verify ALB controller logs
kubectl logs -n kube-system deployment/aws-load-balancer-controller

# Check ALB in AWS Console
aws elbv2 describe-load-balancers --region us-east-1
```

#### 4. High Memory Usage

**Symptoms**: Pods being OOMKilled

**Solutions**:

- Increase memory limits in deployment.yaml
- Analyze memory usage:

```bash
kubectl top pod -n clinic-management-system
```

### Debug Commands

```bash
# Get detailed pod information
kubectl get pods -n clinic-management-system -o wide

# Execute commands in pod
kubectl exec -it <pod-name> -n clinic-management-system -- powershell

# View events
kubectl get events -n clinic-management-system --sort-by='.lastTimestamp'

# Check resource usage
kubectl top nodes
kubectl top pods -n clinic-management-system
```

## Security Considerations

### 1. Container Security

- Use official Microsoft base images
- Regularly update base images for security patches
- Scan images for vulnerabilities:

```bash
aws ecr start-image-scan --repository-name clinic-management-system --image-id imageTag=latest --region us-east-1
```

### 2. Network Security

- Use AWS Security Groups to restrict traffic
- Implement Network Policies:

```yaml
apiVersion: networking.k8s.io/v1
kind: NetworkPolicy
metadata:
  name: clinic-network-policy
  namespace: clinic-management-system
spec:
  podSelector:
    matchLabels:
      app: clinic-management-system
  policyTypes:
  - Ingress
  - Egress
  ingress:
  - from:
    - namespaceSelector:
        matchLabels:
          name: clinic-management-system
    ports:
    - protocol: TCP
      port: 80
```

### 3. Secrets Management

- Use AWS Secrets Manager or AWS Systems Manager Parameter Store
- Install External Secrets Operator:

```bash
helm repo add external-secrets https://charts.external-secrets.io
helm install external-secrets external-secrets/external-secrets -n external-secrets --create-namespace
```

### 4. IAM and RBAC

- Use IAM Roles for Service Accounts (IRSA)
- Implement least-privilege RBAC policies
- Create service account:

```yaml
apiVersion: v1
kind: ServiceAccount
metadata:
  name: clinic-app-sa
  namespace: clinic-management-system
  annotations:
    eks.amazonaws.com/role-arn: arn:aws:iam::<account-id>:role/clinic-app-role
```

## Scaling and Management

### Horizontal Pod Autoscaling

```yaml
apiVersion: autoscaling/v2
kind: HorizontalPodAutoscaler
metadata:
  name: clinic-hpa
  namespace: clinic-management-system
spec:
  scaleTargetRef:
    apiVersion: apps/v1
    kind: Deployment
    name: clinic-management-system
  minReplicas: 2
  maxReplicas: 10
  metrics:
  - type: Resource
    resource:
      name: cpu
      target:
        type: Utilization
        averageUtilization: 70
  - type: Resource
    resource:
      name: memory
      target:
        type: Utilization
        averageUtilization: 80
```

Apply HPA:

```bash
kubectl apply -f hpa.yaml
kubectl get hpa -n clinic-management-system
```

### Rolling Updates

```bash
# Update image
kubectl set image deployment/clinic-management-system \
  clinic-management-system=<new-image-uri> \
  -n clinic-management-system

# Monitor rollout
kubectl rollout status deployment/clinic-management-system -n clinic-management-system

# Rollback if needed
kubectl rollout undo deployment/clinic-management-system -n clinic-management-system
```

### Monitoring and Logging

#### Application Insights

The application includes Application Insights. Configure instrumentation key:

```yaml
env:
- name: APPINSIGHTS_INSTRUMENTATIONKEY
  valueFrom:
    secretKeyRef:
      name: appinsights-secret
      key: instrumentation-key
```

#### CloudWatch Logs

Install Fluent Bit for log forwarding:

```bash
kubectl apply -f https://raw.githubusercontent.com/aws-samples/amazon-cloudwatch-container-insights/latest/k8s-deployment-manifest-templates/deployment-mode/daemonset/container-insights-monitoring/fluent-bit/fluent-bit.yaml
```

### Backup and Disaster Recovery

1. **Backup Kubernetes Resources**

```bash
kubectl get all -n clinic-management-system -o yaml > backup.yaml
```

2. **Use Velero for Cluster Backups**

```bash
helm install velero vmware-tanzu/velero \
  --namespace velero --create-namespace \
  --set configuration.provider=aws \
  --set configuration.backupStorageLocation.bucket=<s3-bucket> \
  --set configuration.backupStorageLocation.config.region=us-east-1

# Create backup
velero backup create clinic-backup --include-namespaces clinic-management-system
```

## Technology-Specific Notes

### .NET Framework Considerations

1. **Windows Containers Required**: .NET Framework 4.5.2 requires Windows Server containers
2. **IIS Configuration**: Application runs on IIS within the container
3. **Memory Requirements**: Windows containers require more memory (minimum 1GB)
4. **Startup Time**: Windows containers have longer startup times (60-90 seconds)

### Performance Tuning

1. **Application Pool Settings**

```powershell
# Inside container
Import-Module WebAdministration
Set-ItemProperty IIS:\AppPools\.NET v4.5 -name recycling.periodicRestart.time -value 0
Set-ItemProperty IIS:\AppPools\.NET v4.5 -name processModel.idleTimeout -value 0
```

2. **Resource Limits**

Adjust based on load testing:

```yaml
resources:
  requests:
    cpu: "1000m"
    memory: "2Gi"
  limits:
    cpu: "2000m"
    memory: "4Gi"
```

### Database Initialization

Initialize database schema:

```bash
# Apply schema from SQL file
kubectl cp Hospital_mgmt_MSSQL.sql <pod-name>:/tmp/schema.sql -n clinic-management-system
kubectl exec <pod-name> -n clinic-management-system -- sqlcmd -S <db-host> -U <user> -P <password> -i /tmp/schema.sql
```

## Additional Resources

- [AWS EKS Documentation](https://docs.aws.amazon.com/eks/)
- [Kubernetes Documentation](https://kubernetes.io/docs/)
- [.NET Framework Container Images](https://hub.docker.com/_/microsoft-dotnet-framework)
- [Windows Containers on EKS](https://docs.aws.amazon.com/eks/latest/userguide/windows-support.html)

## Support and Maintenance

For issues or questions:

1. Check application logs: `kubectl logs -f deployment/clinic-management-system -n clinic-management-system`
2. Review AWS CloudWatch for infrastructure metrics
3. Consult Application Insights for application telemetry

---

**Note**: This deployment guide assumes a production-ready setup. Always test in a staging environment before deploying to production.