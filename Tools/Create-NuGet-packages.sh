#!/bin/bash

OUTPUT_DIR="nuget-packages"

msbuild /p:Configuration=Release

if [ "$?" -ne "0" ]
then
  exit 1
fi

rm -rf "$OUTPUT_DIR"
mkdir -p "$OUTPUT_DIR"

find . \
  -type f \
  -name "*.nuspec" \
  \! -path "./.git/*" \
  -exec nuget pack '{}' -OutputDirectory "$OUTPUT_DIR" \;