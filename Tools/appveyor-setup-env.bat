IF NOT DEFINED APPVEYOR_PULL_REQUEST_HEAD_REPO_BRANCH (
    SET BranchName=%APPVEYOR_REPO_BRANCH%
    SET BranchParam=sonar.branch.name
    SET PRParam=
    echo Not building a PR
) ELSE (
    SET BranchName=%APPVEYOR_PULL_REQUEST_HEAD_REPO_BRANCH%
    SET BranchParam=sonar.pullrequest.branch
    SET PRParam=/d:sonar.pullrequest.key=%APPVEYOR_PULL_REQUEST_NUMBER%
    echo Building a PR
)