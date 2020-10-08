#!/usr/bin/env bash
# To ensure the az CLI is logged in and out properly, run this task inside an 
# Azure CLI DevOps task.
set -euo pipefail

# Caluclate the token expiration time
TOKEN_EXPIRATION=$(date -u -d "1 hour" '+%Y-%m-%dT%H:%MZ')

# Generate a read-write SAS token for the private backend storage container
TOKEN=$(
    az storage account generate-sas \
        --account-name ${AZURE_STORAGE_ACCOUNT} \
        --expiry ${TOKEN_EXPIRATION} \
        --permissions acdlpruw \
        --resource-types co \
        --services b \
        --https-only \
        --output tsv
)

# Set the token as a pipeline variable for other steps to use.
echo "##vso[task.setvariable variable=AZURE_STORAGE_TOKEN;issecret=true]${TOKEN}"