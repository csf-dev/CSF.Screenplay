# CSF.Screenplay.Selenium
This is an add-in to [CSF.Screenplay] for controlling a [Selenium WebDriver] from within Screenplay-based [BDD] tests.

[CSF.Screenplay]: https://github.com/csf-dev/CSF.Screenplay
[Selenium WebDriver]: https://www.seleniumhq.org/
[BDD]: https://en.wikipedia.org/wiki/Behavior-driven_development

## Why Screenplay?
The [Screenplay pattern], previously known as **Journey**, originally came about to address some of the limitations of the well-known [Page Object pattern]. In many ways, it is the result of aggressively refactoring Page Objects following the SOLID principles.

[Screenplay pattern]: https://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay
[Page Object pattern]: https://martinfowler.com/bliki/PageObject.html

Screenplay, applied to a Selenium WebDriver, describes individual user interactions as **Action** classes, but through the Screenplay architecture, makes it simple to compose those actions into reusable **Task** classes, representing higher-level interactions. In turn, there are no limits on the composition of task classes.

In order to perform test assertions, create **Question** types, which represent reading information from the browser.  Like tasks, questions may be composed of other tasks and questions, making both high and low-level questions available with code reuse between them.

## This package

### Pre-created actions
The CSF.Screenplay.Selenium library comes with a wide number of action classes built-in, which permit a wide range of low-level interactions between a 'testing user' and a web application.

### JavaScript workarounds
Browser support for Selenium WebDriver isn't perfect.  A number of web browsers have less-than-perfect support; for example, Apple **Safari** version 11.0 cannot make selections from HTML `<select>` elements, due a bug in their WebDriver implementation.  In order to work around known issues, this library ships with JavaScript workarounds built-in.

Using the action classes included, if it is detected that you are requesting an action which is unsupported in the current browser, a JavaScript-based workaround is automatically used instead.  This will be visible in the Screenplay Report generated at the end of the test run (assuming that you are using reporting).

## Continuous integration status
CI builds are configured via both **Travis** (for build & test on Linux/Mono) and **AppVeyor** (Windows/.NET).
Below are links to the most recent build statuses for these two CI platforms.

The Travis build also includes browser-based testing on the "big 5" browsers; AppVeyor tests only on the built-in installation of Firefox.

* Edge
* Internet Explorer
* Chrome
* Firefox
* Safari

Platform | Status
-------- | ------
Linux/Mono (Travis) | [![Travis Status](https://travis-ci.org/csf-dev/CSF.Screenplay.Selenium.svg?branch=master)](https://travis-ci.org/csf-dev/CSF.Screenplay.Selenium)
Windows/.NET (AppVeyor) | [![AppVeyor status](https://ci.appveyor.com/api/projects/status/ffp7d1jpa4gtkf5n/branch/master?svg=true)](https://ci.appveyor.com/project/craigfowler/csf-screenplay-selenium)
