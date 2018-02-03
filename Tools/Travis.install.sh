#!/bin/bash

NUGET_LATEST_DIST="https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
NUNIT_CONSOLE_VERSION="3.7.0"
NUGET_PATH=".nuget/nuget.exe"

stop_if_failure()
{
  code="$1"
  process="$2"
  if [ "$code" -ne "0" ]
  then
    echo "The process '${process}' failed with exit code $code"
    exit "$code"
  fi
}

install_latest_nuget()
{
  echo "Downloading the latest version of NuGet ..."

  # Travis uses Xamarin's apt repo which has an ancient nuget version
  mkdir -p .nuget
  wget -O "$NUGET_PATH" "$NUGET_LATEST_DIST"
  stop_if_failure $? "Download NuGet"
}

echo_nuget_version_to_console()
{
  mono "$NUGET_PATH"
}

install_latest_nuget
echo_nuget_version_to_console

export NUGET_PATH
export NUNIT_CONSOLE_VERSION
Tools/Install.sh

exit $?