@echo off
setlocal enabledelayedexpansion

echo ============================================
echo   AWS EKS Deployment Script
echo ============================================
echo.

set /p AWS_REGION="Enter AWS Region (e.g., us-east-1): "
set /p CLUSTER_NAME="Enter EKS Cluster Name: "
set /p IMAGE_URI="Enter Docker Image URI (full path with tag): "

if "!AWS_REGION!"=="" (
    echo ERROR: AWS Region is required
    exit /b 1
)

if "!CLUSTER_NAME!"=="" (
    echo ERROR: EKS Cluster Name is required
    exit /b 1
)

if "!IMAGE_URI!"=="" (
    echo ERROR: Docker Image URI is required
    exit /b 1
)

echo.
echo --- Application Configuration ---
set /p DB_HOST="Enter Database Host (or press Enter to skip): "
set /p DB_NAME="Enter Database Name (or press Enter to skip): "
set /p DB_USER="Enter Database User (or press Enter to skip): "

if "!DB_HOST!"=="" set DB_HOST=localhost
if "!DB_NAME!"=="" set DB_NAME=DBProject
if "!DB_USER!"=="" set DB_USER=sa

echo.
echo ============================================
echo Configuring kubectl for EKS...
echo ============================================

aws eks update-kubeconfig --region !AWS_REGION! --name !CLUSTER_NAME!

if !ERRORLEVEL! neq 0 (
    echo ERROR: Failed to configure kubectl
    exit /b 1
)

echo.
echo Verifying cluster connectivity...
kubectl cluster-info

if !ERRORLEVEL! neq 0 (
    echo ERROR: Cannot connect to cluster
    exit /b 1
)

echo.
echo ============================================
echo Updating Kubernetes manifests...
echo ============================================

if not exist kubernetes mkdir kubernetes

for %%f in (kubernetes\*.yaml) do (
    echo Updating %%f...
    powershell -Command "(Get-Content '%%f') -replace '{{IMAGE_URI}}', '!IMAGE_URI!' | Set-Content '%%f'"
    powershell -Command "(Get-Content '%%f') -replace '{{DB_HOST}}', '!DB_HOST!' | Set-Content '%%f'"
    powershell -Command "(Get-Content '%%f') -replace '{{DB_NAME}}', '!DB_NAME!' | Set-Content '%%f'"
    powershell -Command "(Get-Content '%%f') -replace '{{DB_USER}}', '!DB_USER!' | Set-Content '%%f'"
)

echo.
echo ============================================
echo Deploying to EKS...
echo ============================================

echo Creating namespace...
kubectl apply -f kubernetes\namespace.yaml

echo Deploying application...
kubectl apply -f kubernetes\deployment.yaml

echo Creating service...
kubectl apply -f kubernetes\service.yaml

echo Creating ingress...
kubectl apply -f kubernetes\ingress.yaml

echo.
echo ============================================
echo Waiting for deployment to complete...
echo ============================================

kubectl rollout status deployment/clinic-management-system -n clinic-management-system --timeout=5m

if !ERRORLEVEL! neq 0 (
    echo WARNING: Deployment rollout did not complete in time
    echo Check deployment status with: kubectl get pods -n clinic-management-system
)

echo.
echo ============================================
echo Deployment Status
echo ============================================

kubectl get pods,svc,ingress -n clinic-management-system

echo.
echo ============================================
echo SUCCESS!
echo ============================================
echo Application deployed to EKS cluster: !CLUSTER_NAME!
echo.
echo To get the application URL run:
echo kubectl get ingress -n clinic-management-system
echo.
echo To view logs:
echo kubectl logs -f deployment/clinic-management-system -n clinic-management-system
echo.
echo To check pod status:
echo kubectl get pods -n clinic-management-system
echo.

endlocal