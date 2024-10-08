# Documentation source files

This directory contains a [DocFx documentation project].
These are the source files which generate the documentation website in the `docs/` directory.

Files in the `api/` subdirectory are auto-generated by the DocFx build process; they are not for hand-editing.
API documentation is auto-generated from C# types and members and their XML documentation comments.

Markdown files (except this README file) in this directory and subdirectories are converted to HTML pages by DocFx.
The `CSF.Screenplay.Docs.proj` file ensures that the latest documentation is built just by executing `dotnet build`.
If you do not yet have the `docfx` .NET tool installed then you will receive a build error; use the following command to install (or update) it.

```txt
dotnet tool update -g docfx
```

[DocFx documentation project]: https://dotnet.github.io/docfx/index.html
