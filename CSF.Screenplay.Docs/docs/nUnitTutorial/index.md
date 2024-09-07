# Screenplay & NUnit tutorial

Begin writing NUnit tests using Screenplay by following these steps.
Further detail is provided below.

1. Ensure that your test project uses [NUnit version 3.6.0] or higher
1. Install the NuGet package **[CSF.Screenplay.NUnit]** to your test project
1. Write a class which implements [`IGetsScreenplay`]
1. Decorate your test assembly with [`ScreenplayAssemblyAttribute`], referencing your implementation of `IGetsScreenplay`
1. Write your tests, decorating each test method with [`ScreenplayAttribute`]
1. Add parameters to your test methods to access the Screenplay architecture

[NUnit version 3.6.0]: https://www.nuget.org/packages/NUnit/3.6.0
[CSF.Screenplay.NUnit]: https://www.nuget.org/packages/CSF.Screenplay.NUnit
[`IGetsScreenplay`]: xref:CSF.Screenplay.IGetsScreenplay
[`ScreenplayAssemblyAttribute`]: xref:CSF.Screenplay.ScreenplayAssemblyAttribute
[`ScreenplayAttribute`]:xref:CSF.Screenplay.ScreenplayAttribute

## Decorating your test assembly with `[ScreenplayAssembly]`

TODO: Write this docco

## Writing test methods

TODO: Write this docco
