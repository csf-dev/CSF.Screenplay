# Selenium tasks

As noted [in their glossary definition], Tasks are a kind of performable which composes other performables.
The Selenium extension for Screenplay provides a small number of Tasks, which simplify some common WebDriver processes.
The table below serves as a list, along with a brief summary of the purpose of each.

[in their glossary definition]: ../../../glossary/Task.md

| Task                              | Usage                                                                                 |
| ----                              | -----                                                                                 |
| [`BeginCollectingLogsWithJavaScriptIfApplicable`] | Conditionally executes [`BeginCollectingLogsWithJavaScript`] |
| [`ClickAndWaitForDocumentReady`]  | Clicks a link which navigates to a new page, waiting until it is ready                |
| [`EnterTheDate`]                  | Enters a value into an `<input type="date">` in a cross-browser manner                |
| [`GetShadowRoot`]                 | Uses the best technique available to get [the root element] of a [Shadow DOM] |
| [`GetTheBrowserLogs`]             | Uses the best technique available to get the browser console logs |
| [`NavigateToUrl`]                 | Navigates to a specified URL, which may be relative to a base specified by the [`UseABaseUri`] ability |
| [`SetTheElementValue`]            | Sets the `value` of an element with JavaScript, emulating updating it interactively   |
| [`TakeAndSaveScreenshot`]         | Convenience task to combine the taking & saving of a browser screenshot               |

[`BeginCollectingLogsWithJavaScriptIfApplicable`]: xref:CSF.Screenplay.Selenium.Tasks.BeginCollectingLogsWithJavaScriptIfApplicable
[`BeginCollectingLogsWithJavaScript`]: xref:CSF.Screenplay.Selenium.Actions.BeginCollectingLogsWithJavaScript
[`GetShadowRoot`]: xref:CSF.Screenplay.Selenium.Tasks.GetShadowRoot
[Shadow DOM]: https://developer.mozilla.org/en-US/docs/Web/API/Web_components/Using_shadow_DOM
[the root element]: https://developer.mozilla.org/en-US/docs/Web/API/ShadowRoot
[`GetTheBrowserLogs`]: xref:CSF.Screenplay.Selenium.Tasks.GetTheBrowserLogs
[`ClickAndWaitForDocumentReady`]: xref:CSF.Screenplay.Selenium.Tasks.ClickAndWaitForDocumentReady
[`EnterTheDate`]: xref:CSF.Screenplay.Selenium.Tasks.EnterTheDate
[`NavigateToUrl`]: xref:CSF.Screenplay.Selenium.Tasks.NavigateToUrl
[`UseABaseUri`]: xref:CSF.Screenplay.Selenium.UseABaseUri
[`SetTheElementValue`]: xref:CSF.Screenplay.Selenium.Tasks.SetTheElementValue
[`TakeAndSaveScreenshot`]: xref:CSF.Screenplay.Selenium.Tasks.TakeAndSaveScreenshot
