#!/bin/bash
set -e
set -o pipefail

echo "====================================="
echo "CareTracker Application - Docker Build & Push"
echo "====================================="
echo ""

# Project configuration
PROJECT_NAME="CareTracker Application"
IMAGE_NAME=$(echo "$PROJECT_NAME" | tr '[:upper:]' '[:lower:]' | tr -cs 'a-z0-9' '-' | sed 's/^-*//;s/-*$//')

echo "Project: $PROJECT_NAME"
echo "Image Name: $IMAGE_NAME"
echo ""

# Prompt for image tag
read -p "Enter image tag (default: latest): " IMAGE_TAG
IMAGE_TAG=${IMAGE_TAG:-latest}
IMAGE_TAG=$(echo "$IMAGE_TAG" | tr '[:upper:]' '[:lower:]' | tr -cs 'a-z0-9.-' '-' | sed 's/^-*//;s/-*$//')

if [ -z "$IMAGE_TAG" ]; then
    IMAGE_TAG="latest"
fi

echo "Using tag: $IMAGE_TAG"
echo ""

# Registry selection
echo "Select container registry:"
echo "1. AWS ECR (Elastic Container Registry)"
echo "2. Docker Hub"
read -p "Enter choice (1 or 2): " REGISTRY_CHOICE

if [ "$REGISTRY_CHOICE" == "1" ]; then
    # AWS ECR
    echo ""
    echo "=== AWS ECR Configuration ==="
    read -p "Enter AWS Region (e.g., us-east-1): " AWS_REGION
    read -p "Enter AWS Account ID: " AWS_ACCOUNT_ID
    read -p "Enter ECR Repository Name (default: $IMAGE_NAME): " ECR_REPO
    ECR_REPO=${ECR_REPO:-$IMAGE_NAME}
    
    REGISTRY_URL="$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com"
    FULL_IMAGE_NAME="$REGISTRY_URL/$ECR_REPO:$IMAGE_TAG"
    
    echo ""
    echo "Logging into AWS ECR..."
    aws ecr get-login-password --region "$AWS_REGION" | docker login --username AWS --password-stdin "$REGISTRY_URL"
    
    if [ $? -ne 0 ]; then
        echo "ERROR: ECR login failed. Please check your AWS credentials and region."
        exit 1
    fi
    
    echo "Checking if ECR repository exists..."
    aws ecr describe-repositories --repository-names "$ECR_REPO" --region "$AWS_REGION" >/dev/null 2>&1 || {
        echo "Repository does not exist. Creating ECR repository: $ECR_REPO"
        aws ecr create-repository --repository-name "$ECR_REPO" --region "$AWS_REGION"
        if [ $? -ne 0 ]; then
            echo "ERROR: Failed to create ECR repository."
            exit 1
        fi
    }
    
elif [ "$REGISTRY_CHOICE" == "2" ]; then
    # Docker Hub
    echo ""
    echo "=== Docker Hub Configuration ==="
    read -p "Enter Docker Hub Username: " DOCKER_USERNAME
    read -sp "Enter Docker Hub Password or Access Token: " DOCKER_PASSWORD
    echo ""
    
    FULL_IMAGE_NAME="$DOCKER_USERNAME/$IMAGE_NAME:$IMAGE_TAG"
    
    echo "Logging into Docker Hub..."
    echo "$DOCKER_PASSWORD" | docker login --username "$DOCKER_USERNAME" --password-stdin
    
    if [ $? -ne 0 ]; then
        echo "ERROR: Docker Hub login failed. Please check your credentials."
        exit 1
    fi
else
    echo "Invalid choice. Exiting."
    exit 1
fi

echo ""
echo "====================================="
echo "Building Docker Image"
echo "====================================="
echo "Image: $FULL_IMAGE_NAME"
echo ""

# Build the Docker image
docker build -f Dockerfile -t "$FULL_IMAGE_NAME" .

if [ $? -ne 0 ]; then
    echo "ERROR: Docker build failed."
    exit 1
fi

echo ""
echo "====================================="
echo "Pushing Docker Image"
echo "====================================="
echo ""

# Push the image
docker push "$FULL_IMAGE_NAME"

if [ $? -ne 0 ]; then
    echo "ERROR: Docker push failed."
    exit 1
fi

echo ""
echo "====================================="
echo "Build and Push Completed Successfully!"
echo "====================================="
echo "Image: $FULL_IMAGE_NAME"
echo ""
echo "To deploy this image to AWS ECS, run:"
echo "  ./scripts/deploy-image.sh"
echo ""