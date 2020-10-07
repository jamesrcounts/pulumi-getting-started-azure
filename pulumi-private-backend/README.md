# How to Run Pulumi with an Azure Storage Backend

Use an Azure Storage account to store the stack file instead of app.pulumi.com.

<!-- TOC -->
- [How to Run Pulumi with an Azure Storage Backend](#how-to-run-pulumi-with-an-azure-storage-backend)
  - [Prerequisites](#prerequisites)
  - [Steps](#steps)
  - [Interesting Take-aways](#interesting-take-aways)

<!-- /TOC -->

## Prerequisites

* Pulumi CLI - provided by devcontainer in this project
* dotnet SDK - provided by devcontainer in this project
* Azure CLI - provided by devcontainer in this project

## Steps

1. Bootstrap the Pulumi project.
2. Add an azure-pipelines.yml template
3. Configure a pipeline to run in Azure DevOps
   1. Create a DevOps project
   2. Create a pipeline
      1. Choose GitHub (YAML)
      2. Grant OAuth access if needed
      3. Choose repository
      4. Choose "Existing Azure Pipelines YAML file"
      5. Pick the branch your yaml file is on (if not main)
      6. Choose or enter the path to your yaml file
      7. Choose Run (or Save) to save the pipeline
4. Configure a branch protection rule in GitHub that requires all status checks to pass.
5. Install Pulumi devops extension
6. Create a service connection for Azure
7. Grant pipeline permission to use the service connection
8. Define a variable group to hold the pulumi access token
   1. Define a secret variable called `pulumi.access.token`
   2. Reference this group from the pipeline

## Interesting Take-aways
