#!/bin/bash
set -e
set -o pipefail

echo "============================================"
echo "  AWS EKS Deployment Script"
echo "============================================"
echo ""

# Prompt for AWS and EKS configuration
read -p "Enter AWS Region (e.g., us-east-1): " AWS_REGION
read -p "Enter EKS Cluster Name: " CLUSTER_NAME
read -p "Enter Docker Image URI (full path with tag): " IMAGE_URI

if [ -z "$AWS_REGION" ] || [ -z "$CLUSTER_NAME" ] || [ -z "$IMAGE_URI" ]; then
    echo "ERROR: All fields are required"
    exit 1
fi

echo ""
echo "--- Application Configuration ---"
read -p "Enter Database Host (or press Enter to skip): " DB_HOST
read -p "Enter Database Name (or press Enter to skip): " DB_NAME
read -p "Enter Database User (or press Enter to skip): " DB_USER

if [ -n "$DB_HOST" ]; then
    DB_HOST=${DB_HOST:-"localhost"}
fi

if [ -n "$DB_NAME" ]; then
    DB_NAME=${DB_NAME:-"DBProject"}
fi

if [ -n "$DB_USER" ]; then
    DB_USER=${DB_USER:-"sa"}
fi

echo ""
echo "============================================"
echo "Configuring kubectl for EKS..."
echo "============================================"

aws eks update-kubeconfig --region "$AWS_REGION" --name "$CLUSTER_NAME"

if [ $? -ne 0 ]; then
    echo "ERROR: Failed to configure kubectl"
    exit 1
fi

echo ""
echo "Verifying cluster connectivity..."
kubectl cluster-info || exit 1

echo ""
echo "============================================"
echo "Updating Kubernetes manifests..."
echo "============================================"

# Create kubernetes directory if it doesn't exist
mkdir -p kubernetes

# Update manifests with actual values
for file in kubernetes/*.yaml; do
    if [ -f "$file" ]; then
        echo "Updating $file..."
        sed -i 's|{{IMAGE_URI}}|'"$IMAGE_URI"'|g' "$file"
        
        if [ -n "$DB_HOST" ]; then
            sed -i 's|{{DB_HOST}}|'"$DB_HOST"'|g' "$file"
        fi
        
        if [ -n "$DB_NAME" ]; then
            sed -i 's|{{DB_NAME}}|'"$DB_NAME"'|g' "$file"
        fi
        
        if [ -n "$DB_USER" ]; then
            sed -i 's|{{DB_USER}}|'"$DB_USER"'|g' "$file"
        fi
    fi
done

echo ""
echo "============================================"
echo "Deploying to EKS..."
echo "============================================"

# Apply manifests in order
echo "Creating namespace..."
kubectl apply -f kubernetes/namespace.yaml

echo "Deploying application..."
kubectl apply -f kubernetes/deployment.yaml

echo "Creating service..."
kubectl apply -f kubernetes/service.yaml

echo "Creating ingress..."
kubectl apply -f kubernetes/ingress.yaml

echo ""
echo "============================================"
echo "Waiting for deployment to complete..."
echo "============================================"

kubectl rollout status deployment/clinic-management-system -n clinic-management-system --timeout=5m

if [ $? -ne 0 ]; then
    echo "WARNING: Deployment rollout did not complete in time"
    echo "Check deployment status with: kubectl get pods -n clinic-management-system"
fi

echo ""
echo "============================================"
echo "Deployment Status"
echo "============================================"

kubectl get pods,svc,ingress -n clinic-management-system

echo ""
echo "============================================"
echo "SUCCESS!"
echo "============================================"
echo "Application deployed to EKS cluster: $CLUSTER_NAME"
echo ""
echo "To get the application URL, run:"
echo "kubectl get ingress -n clinic-management-system"
echo ""
echo "To view logs:"
echo "kubectl logs -f deployment/clinic-management-system -n clinic-management-system"
echo ""
echo "To check pod status:"
echo "kubectl get pods -n clinic-management-system"
echo ""