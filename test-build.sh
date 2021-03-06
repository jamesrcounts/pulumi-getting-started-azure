#!/usr/bin/env bash
set -euo pipefail

docker pull mcr.microsoft.com/vscode/devcontainers/base:buster
docker build \
    --build-arg VARIANT=3.1 \
    --build-arg INSTALL_AZURE_CLI=true \
    --build-arg INSTALL_DOCKER=true \
    --build-arg INSTALL_NODE=false \
    --build-arg INSTALL_ZSH=false \
    --build-arg UPGRADE_PACKAGES=true \
    --tag devcontainer-pulumi \
    --file ./.devcontainer/Dockerfile \
    ./.devcontainer