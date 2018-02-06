#!/bin/bash

msbuild /p:Configuration=Release

if [ "$?" -ne "0" ]
then
  exit 1
fi

find . \
  -type f \
  -name "*.nuspec" \
  \! -path "./.git/*" \
  -exec nuget pack '{}' \;