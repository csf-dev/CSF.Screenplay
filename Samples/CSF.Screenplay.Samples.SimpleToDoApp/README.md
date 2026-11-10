# Simple TODO app

This project contains a [git submodule] which points to [the TodoMVC project].
TodoMVC is a small, self-contained web application for which we may write tests using Screenplay.
The sample app uses **version 2.0.0** of TodoMVC.

[git submodule]: https://git-scm.com/docs/gitsubmodules
[the TodoMVC project]: https://github.com/tastejs/todomvc

## If the `todomvc` directory is empty

If the directory is empty in your working copy then most likely you did not initialise the submodules, as directed [in the instructions for cloning the repo].
You can fix this by running the command:

```txt
git submodule update --init --recursive
```

[in the instructions for cloning the repo]: ../../README.md

## Running the sample app

To run the sample app, use the following commands, from this directory.

```txt
npm ci
npm run serve
```

The app will be available at `http://localhost:5173`.
You may change the port if desired from `package.json`.

Note: The test project `CSF.Screenplay.Samples.SimpleToDoAppTests` performs these steps automatically when it runs.
You only need run it manually if you'd like to see it for yourself.
