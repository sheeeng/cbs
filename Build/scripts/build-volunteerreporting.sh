#!/usr/bin/env bash

set -x

export DEBIAN_FRONTEND=noninteractive
export WORKSPACE_DIR=/workspace
export BUILD_DIR=${WORKSPACE_DIR}/Build

if [ "$PWD" != "$WORKSPACE_DIR" ]; then
  echo "Build helper script to be run inside a container."
  echo "Go to $WORKSPACE_DIR directory before running this script."
  exit 1
fi

export TestsFolder=../Source/VolunteerReporting/Tests
export SlnFile=../Source/VolunteerReporting/VolunteerReporting.sln
export WebBinFolder=../Source/VolunteerReporting/Web/bin
export AngularFolder=../Source/VolunteerReporting/Web.Angular

cd ${BUILD_DIR} && ./build.sh