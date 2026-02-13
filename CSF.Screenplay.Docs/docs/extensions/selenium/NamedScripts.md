---
uid: NamedScriptsArticle
---
# Named JavaScripts

Sometimes it is neccesary to execute JavaScript in the web browser.
This might be to work around a limitation in a WebDriver implementation, such as [web browsers which cannot enter values into date fields].
At other times, JavaScript might be used to perform large-scale operations which would perform very poorly if conducted through many WebDriver interactions.
This is particularly relevant where a Remote WebDriver is being used, in which each individual interaction suffers the latency and overhead of a remote network request/response.

Named scripts provide a mechanism by which developers may build a library of reusable pre-defined scripts and give them human-readable names.
This way, when those scripts are used, they produce clear and concise entries in [the Screenplay report].
Additionally, named scripts provide .NET type safety via generic type parameters.

[web browsers which cannot enter values into date fields]: xref:CSF.Screenplay.Selenium.BrowserQuirks.CannotSetInputTypeDateWithSendKeys
[the Screenplay report]: ../../GettingReports.md

## The classes

There are many script classes available, differing primarily in the number of paramaters which they accept.
Here is a table listing them all.
Pick the one which returns/does not return a result and which has the correct number of parameters for your script.

| Params    | No result                                 | With result                                               |
| ------    | ---------                                 | -----------                                               |
| 0         | [`NamedScript`]                           | [`NamedScriptWithResult<TResult>`]                        |
| 1         | [`NamedScript<T1>`]                       | [`NamedScriptWithResult<T1,TResult>`]                     |
| 2         | [`NamedScript<T1,T2>`]                    | [`NamedScriptWithResult<T1,T2,TResult>`]                  |
| 3         | [`NamedScript<T1,T2,T3>`]                 | [`NamedScriptWithResult<T1,T2,T3,TResult>`]               |
| 4         | [`NamedScript<T1,T2,T3,T4>`]              | [`NamedScriptWithResult<T1,T2,T3,T4,TResult>`]            |
| 5         | [`NamedScript<T1,T2,T3,T4,T5>`]           | [`NamedScriptWithResult<T1,T2,T3,T4,T5,TResult>`]         |
| 6         | [`NamedScript<T1,T2,T3,T4,T5,T6>`]        | [`NamedScriptWithResult<T1,T2,T3,T4,T5,T6,TResult>`]      |
| 7         | [`NamedScript<T1,T2,T3,T4,T5,T6,T7>`]     | [`NamedScriptWithResult<T1,T2,T3,T4,T5,T6,T7,TResult>`]   |

[`NamedScript`]: xref:CSF.Screenplay.Selenium.NamedScript
[`NamedScriptWithResult<TResult>`]: xref:CSF.Screenplay.Selenium.NamedScriptWithResult`1
[`NamedScript<T1>`]: xref:CSF.Screenplay.Selenium.NamedScript`1
[`NamedScriptWithResult<T1,TResult>`]: xref:CSF.Screenplay.Selenium.NamedScriptWithResult`2
[`NamedScript<T1,T2>`]: xref:CSF.Screenplay.Selenium.NamedScript`2
[`NamedScriptWithResult<T1,T2,TResult>`]: xref:CSF.Screenplay.Selenium.NamedScriptWithResult`3
[`NamedScript<T1,T2,T3>`]: xref:CSF.Screenplay.Selenium.NamedScript`3
[`NamedScriptWithResult<T1,T2,T3,TResult>`]: xref:CSF.Screenplay.Selenium.NamedScriptWithResult`4
[`NamedScript<T1,T2,T3,T4>`]: xref:CSF.Screenplay.Selenium.NamedScript`4
[`NamedScriptWithResult<T1,T2,T3,T4,TResult>`]: xref:CSF.Screenplay.Selenium.NamedScriptWithResult`5
[`NamedScript<T1,T2,T3,T4,T5>`]: xref:CSF.Screenplay.Selenium.NamedScript`5
[`NamedScriptWithResult<T1,T2,T3,T4,T5,TResult>`]: xref:CSF.Screenplay.Selenium.NamedScriptWithResult`6
[`NamedScript<T1,T2,T3,T4,T5,T6>`]: xref:CSF.Screenplay.Selenium.NamedScript`6
[`NamedScriptWithResult<T1,T2,T3,T4,T5,T6,TResult>`]: xref:CSF.Screenplay.Selenium.NamedScriptWithResult`7
[`NamedScript<T1,T2,T3,T4,T5,T6,T7>`]: xref:CSF.Screenplay.Selenium.NamedScript`7
[`NamedScriptWithResult<T1,T2,T3,T4,T5,T6,T7,TResult>`]: xref:CSF.Screenplay.Selenium.NamedScriptWithResult`8

## Using a script

Execute scripts within your performables using the builder method [`PerformableBuilder.ExecuteAScript`] or one of its many same-named overloads.
Generic type inference will offer the appropriate number and types of parameters and will expose a strongly-typed result if applicable.

Within the JavaScript body, the parameters are accessible via an `arguments` object.
Each parameter is assigned to this object using a zero-based index.
So, the first parameter value is `arguments[0]`, the second `arguments[1]` and so on.

[`PerformableBuilder.ExecuteAScript`]: xref:CSF.Screenplay.Selenium.PerformableBuilder.ExecuteAScript(CSF.Screenplay.Selenium.NamedScript)
