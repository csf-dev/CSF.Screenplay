#!/bin/bash

OUTPUT_DIR="nuget-packages"

msbuild /p:Configuration=Release

if [ "$?" -ne "0" ]
then
  exit 1
fi

sn -R CSF.Screenplay/bin/Release/CSF.Screenplay.dll CSF-Software-OSS.snk
sn -R CSF.Screenplay.NUnit/bin/Release/CSF.Screenplay.NUnit.dll CSF-Software-OSS.snk
sn -R CSF.Screenplay.Reporting/bin/Release/CSF.Screenplay.Reporting.dll CSF-Software-OSS.snk
sn -R CSF.Screenplay.Reporting.Html/bin/Release/CSF.Screenplay.Reporting.Html.dll CSF-Software-OSS.snk
sn -R CSF.Screenplay.SpecFlow/bin/Release/CSF.Screenplay.SpecFlowPlugin.dll CSF-Software-OSS.snk
sn -R CSF.Screenplay.WebApis/bin/Release/CSF.Screenplay.WebApis.dll CSF-Software-OSS.snk

rm -rf "$OUTPUT_DIR"
mkdir -p "$OUTPUT_DIR"

find . \
  -type f \
  -name "*.nuspec" \
  \! -path "./.git/*" \
  -exec nuget pack '{}' -OutputDirectory "$OUTPUT_DIR" \;