#!/usr/bin/env bash

set -x

export DEBIAN_FRONTEND=noninteractive
export WORKSPACE_DIR=/workspace
export BUILD_DIR=${WORKSPACE_DIR}/Build
export BUILD_TARGET_DIR=${WORKSPACE_DIR}/Source/Example

if [ "$PWD" != "$WORKSPACE_DIR" ]; then
  echo "Build helper script to be run inside a container."
  echo "Go to $WORKSPACE_DIR directory before running this script."
  exit 1
fi

cp -rfv ${BUILD_DIR}/* ${BUILD_TARGET_DIR}

cd ${BUILD_TARGET_DIR} && build.sh