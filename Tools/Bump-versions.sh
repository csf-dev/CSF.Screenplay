#!/bin/bash

SOLUTION_ROOT="$(dirname $0)/.."
NEW_VERSION="$1"
ASSEMBLY_VERSION="$(echo "$NEW_VERSION" | egrep -o "([0-9]+\.){2}[0-9]+").0"

function bump_assembly_info()
{
  find "$SOLUTION_ROOT" \
    \! -path "*/.git/*" \
    -name "AssemblyInfo.cs" \
    -exec sed -ri "s/AssemblyVersion\\(\"([0-9]+\\.){0,3}[0-9]+\"\\)/AssemblyVersion(\"${ASSEMBLY_VERSION}\")/g" '{}' \;
}

function bump_nuspec_version()
{
  find "$SOLUTION_ROOT" \
    \! -path "*/.git/*" \
    -name "*.nuspec" \
    -exec sed -ri "s/<version>[^<]+<\\/version>/<version>${NEW_VERSION}<\\/version>/g" '{}' \;
}

function bump_solution_version()
{
  sed -ri "s/version *= *.+/version = ${NEW_VERSION}/g" "$SOLUTION_ROOT/CSF.Screenplay.Selenium.sln"
  
  find "$SOLUTION_ROOT" \
    \! -path "*/.git/*" \
    -name "*.csproj" \
    -exec sed -ri "s/<ReleaseVersion>[^<]+<\\/ReleaseVersion>/<ReleaseVersion>${NEW_VERSION}<\\/ReleaseVersion>/g" '{}' \;
}

bump_assembly_info
bump_nuspec_version
bump_solution_version
