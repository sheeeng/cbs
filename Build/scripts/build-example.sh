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

export TestsFolder=../Source/Example/Tests
export SlnFile=../Source/Example/Example.sln
export WebBinFolder=../Source/Example/Web/bin
export AngularFolder=../Source/Example/Web.Angular

sed -i 's/<path to the test folder>/..\/Source\/Example\/Catalog\/Tests/g' Build/appveyor.yml
sed -i 's/<path to the solution file>/..\/Source\/Example\/Catalog\/Catalog.sln/g' Build/appveyor.yml
sed -i 's/<path to the bin folder of the web project>/..\/Source\/Example\/Catalog\/Web\/bin/g' Build/appveyor.yml
sed -i 's/<path to the Angular folder of the web project>/..\/Source\/Example\/Catalog\/Web.Angular/g' Build/appveyor.yml

cat Build/appveyor.yml

cd ${BUILD_DIR} && ./build.sh