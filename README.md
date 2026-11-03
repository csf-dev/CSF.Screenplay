# CSF.Screenplay

Screenplay is a *software design pattern* to assist in the automation of complex processes.
Screenplay synergises particularly well with tests which use the [Behaviour Driven Development] (BDD) style.

**CSF.Screenplay** is a library and framework which brings that design pattern to .NET.
The main library itself is intended to be used alongside extensions.
It has *two primary extension points*; [Test Framework integrations] and [Screenplay Extensions].
One such extension is [CSF.Screenplay.Selenium], which allows Screenplay to control Web Browsers, via [a Selenium WebDriver].

**Learn Screenplay's concepts and how to use it [on the documentation website].**

[Behaviour Driven Development]: https://en.wikipedia.org/wiki/Behavior-driven_development
[Test Framework integrations]: https://csf-dev.github.io/CSF.Screenplay/glossary/Integration.html
[Screenplay Extensions]: https://csf-dev.github.io/CSF.Screenplay/glossary/Extension.html
[CSF.Screenplay.Selenium]: https://www.nuget.org/packages/CSF.Screenplay.Selenium
[a Selenium WebDriver]: https://www.selenium.dev/
[on the documentation website]: https://csf-dev.github.io/CSF.Screenplay/

## Developer information

CI builds are configured via **GitHub Actions**, with static code analysis & reporting in **SonarCloud**.
This project is tested with BrowserStack.

The contents of the `docs/` directory of this repository is auto-generated from the `CSF.Screenplay.Docs` project.
There is nothing hand-editable inside.

[![.NET CI](https://github.com/csf-dev/CSF.Screenplay/actions/workflows/dotnetCi.yml/badge.svg)](https://github.com/csf-dev/CSF.Screenplay/actions/workflows/dotnetCi.yml)
[![Test coverage](https://sonarcloud.io/api/project_badges/measure?project=csf-dev_CSF.Screenplay&metric=coverage)](https://sonarcloud.io/summary/new_code?id=csf-dev_CSF.Screenplay)

### This repository uses git submodules

Note that - for the sample application - this repository makes use of [git submodules].
When cloning this repository, you should either use the [`--recurse-submodules`] option, or after cloning use:

```txt
git submodule update --init --recursive
```

Failure to do so will leave you without [the sample web app] files.

[git submodules]: https://git-scm.com/docs/gitsubmodules
[`--recurse-submodules`]: https://git-scm.com/docs/git-clone#Documentation/git-clone.txt---recurse-submodulespathspec
[the sample web app]: ./Samples/CSF.Screenplay.Samples.SimpleToDoApp/README.md
