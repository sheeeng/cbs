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

export TestsFolder=../Source/Alert/Tests
export SlnFile=../Source/Alert/Alert.sln
export WebBinFolder=../Source/Alert/Web/bin
export AngularFolder=../Source/Alert/Web.Angular

sed -i 's/<path to the test folder>/..\/Source\/Alert\/Tests/g' Build/appveyor.yml
sed -i 's/<path to the solution file>/..\/Source\/Alert\/Alert.sln/g' Build/appveyor.yml
sed -i 's/<path to the bin folder of the web project>/..\/Source\/Alert\/Web\/bin/g' Build/appveyor.yml
sed -i 's/<path to the Angular folder of the web project>/..\/Source\/Alert\/Web.Angular/g' Build/appveyor.yml

cat Build/appveyor.yml

cd ${BUILD_DIR} && ./build.sh