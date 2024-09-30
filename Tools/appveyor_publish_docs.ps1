$ErrorActionPreference = "Stop"

$BaseDir = "docs"

if($Env:APPVEYOR -eq "True" -and ($Env:APPVEYOR_PULL_REQUEST_NUMBER -or $Env:APPVEYOR_REPO_BRANCH -ne "master")) {
    Write-Host "Skipping publishing docs; we are not building on master"
    Exit 0;
}

Write-Host "Publishing the docs site to $BaseDir"

if($Env:APPVEYOR -eq "True") {
    Write-Host "Setting up git to make a commit from Appveyor"
    git config --global user.name "Appveyor (on behalf of Craig Fowler)"
    git config --global user.email "craig+appveyor@csf-dev.com"
    git config --global credential.helper store
    Set-Content -Path "$HOME\.git-credentials" -Value "https://$($Env:GITHUB_SECRET_KEY):x-oauth-basic@github.com`n" -NoNewline
}

# The git commands below could report warnings which cause the script to
# stop unless I change the error preference first
$ErrorActionPreference = "silentlycontinue"

git checkout -b temp/publish-docs
git add --all $BaseDir/
git commit -m "Auto: Updates to docs website via CI [skip ci]"
git checkout $Env:APPVEYOR_REPO_BRANCH
git pull
git merge temp/publish-docs --no-ff -m "Auto: Merge docs website via CI [skip ci]"
git push origin $Env:APPVEYOR_REPO_BRANCH
BRANCH
