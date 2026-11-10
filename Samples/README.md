# Screenplay sample

This directory contains samples which are essentially _as part of the documentation_ rather than production logic or tests for Screenplay:

* **CSF.Screenplay.Samples.SimpleToDoApp**<br>
    This is a small browser-based todo application, runnable with **npm**.
    It is actually the [TodoMVC web app].
* **CSF.Screenplay.Samples.SimpleToDoAppTests**<br>
    This is a BDD test suite written using [Reqnroll], [Selenium] and Screenplay to test the todo app (above).

[TodoMVC web app]: https://github.com/tastejs/todomvc
[Reqnroll]: https://reqnroll.net/
[Selenium]: https://www.selenium.dev/

## The todo app

The TodoMVC project is imported into this repository as a [git submodule].
_If the `Samples/CSF.Screenplay.Samples.SimpleToDoApp/todomvc` directory is empty in your working copy_ then most likely the submodules are not initialised.
You can fix this by running the command:

```txt
git submodule update --init --recursive
```

To run the todo app you will first need to restore node modules.
Ensure you have node/npm installed, then use the command `npm ci` from within the `Samples/CSF.Screenplay.Samples.SimpleToDoApp` directory.

[git submodule]: https://git-scm.com/docs/gitsubmodules

### Running it manually

When performing a test run (see below), the app is started/stopped automatically.
Should you want to see it yourself, after restoring node modules (above) use the following command from the `Samples/CSF.Screenplay.Samples.SimpleToDoApp` directory:

```txt
npm run serve
```

The app will be available at `http://localhost:5173`.

## The test suite

The test suite here is runnable via the following command, or whatever other .NET compatible test-runner you care to use.
_Ensure that the git submodule has loaded and that you have restored node modules first_.

```txt
dotnet test Samples/CSF.Screenplay.Samples.SimpleToDoAppTests
```

Feel free to look around and use this worked example to discover how Screenplay assists in writing tests.
The code is annotated with documentation comments.

* The sample tests project is written using Gherkin syntax (the `.feature` files)
* The `TestRunSetup.cs` file contains DI setup as well as logic to start up the todo app via npm
* `Webster.cs` is a [Persona]
* `TodoApp.cs` provides [URLs and Targets] for interacting with the app
* `PerformableBuilder.cs` is [a builder for performables]
* The `Tasks` directory contains [performable tasks]
* The `StepDefinitions` directory contains Reqnroll step definitions (which bind the Gherkin syntax to C# code)

[Persona]: https://csf-dev.github.io/CSF.Screenplay/glossary/Persona.html
[URLs and Targets]: https://csf-dev.github.io/CSF.Screenplay/docs/extensions/selenium/Targets.html
[a builder for performables]: https://csf-dev.github.io/CSF.Screenplay/docs/builderPattern/index.html
[performable tasks]: https://csf-dev.github.io/CSF.Screenplay/glossary/Task.html
