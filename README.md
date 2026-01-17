# CSF.Screenplay

Screenplay is a *software design pattern* to assist in the automation of complex processes.
It is commonly recommended for use in writing tests using the [Behaviour Driven Development] (BDD) style.
**CSF.Screenplay** is a library and framework for using this design pattern.

[Behaviour Driven Development]: https://en.wikipedia.org/wiki/Behavior-driven_development

## An extensible framework

Screenplay itself is a framework which is intended to be used alongside extensions.
It has *two primary extension points*; [Test Framework integrations] and [Screenplay Extensions].
One such extension is [CSF.Screenplay.Selenium], which allows Screenplay to control Web Browsers, via [a Selenium WebDriver].

[Test Framework integrations]: https://csf-dev.github.io/CSF.Screenplay/glossary/Integration.html
[Screenplay Extensions]: https://csf-dev.github.io/CSF.Screenplay/glossary/Extension.html
[CSF.Screenplay.Selenium]: https://www.nuget.org/packages/CSF.Screenplay.Selenium
[a Selenium WebDriver]: https://www.selenium.dev/

**Learn Screenplay's concepts and how to use it [on the documentation website].**

[on the documentation website]: https://csf-dev.github.io/CSF.Screenplay/

## Continuous integration status

CI builds are configured via **GitHub Actions**, with static code analysis & reporting in **SonarCloud**.

[![.NET CI](https://github.com/csf-dev/CSF.Screenplay/actions/workflows/dotnetCi.yml/badge.svg)](https://github.com/csf-dev/CSF.Screenplay/actions/workflows/dotnetCi.yml)
[![Test coverage](https://sonarcloud.io/api/project_badges/measure?project=csf-dev_CSF.Screenplay&metric=coverage)](https://sonarcloud.io/summary/new_code?id=csf-dev_CSF.Screenplay)
