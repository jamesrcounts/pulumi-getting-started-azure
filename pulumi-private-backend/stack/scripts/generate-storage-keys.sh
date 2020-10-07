#!/usr/bin/env bash
# To ensure the az CLI is logged in and out properly, run this task inside an Azure CLI DevOps task.
set -euo pipefail

# Generate a read-write SAS token for the private backend storage container
TOKEN=$(
    az storage container generate-sas \
        --name ${AZURE_STORAGE_CONTAINER} \
        --expiry ${TOKEN_EXPIRATION} \
        --permissions rw \
        --account-name ${AZURE_STORAGE_ACCOUNT} \
        --https-only \
        --output tsv
)

# Set the token as a pipeline variable for other steps to use.
echo "##vso[task.setvariable variable=storage-key;isSecret=true;isOutput=true]${TOKEN}"