# Selenium tasks

As noted [in their glossary definition], Tasks are a kind of performable which composes other performables.
The Selenium extension for Screenplay provides a small number of Tasks, which simplify some common WebDriver processes.
The table below serves as a list, along with a brief summary of the purpose of each.

[in their glossary definition]: ../../../glossary/Task.md

| Task                              | Usage                                                                                 |
| ----                              | -----                                                                                 |
| [`ClickAndWaitForDocumentReady`]  | Clicks a link which navigates to a new page, waiting until it is ready                |
| [`EnterTheDate`]                  | Enters a value into an `<input type="date">` in a cross-browser manner                |
| [`OpenUrlRespectingBase`]         | Navigates to a URL, using a base URL provided by the [`UseABaseUri`] ability          |
| [`SetTheElementValue`]            | Sets the `value` of an element with JavaScript, emulating updating it interactively   |
| [`TakeAndSaveScreenshot`]         | Convenience task to combine the taking & saving of a browser screenshot               |

[`ClickAndWaitForDocumentReady`]: xref:CSF.Screenplay.Selenium.Tasks.ClickAndWaitForDocumentReady
[`EnterTheDate`]: xref:CSF.Screenplay.Selenium.Tasks.EnterTheDate
[`OpenUrlRespectingBase`]: xref:CSF.Screenplay.Selenium.Tasks.OpenUrlRespectingBase
[`UseABaseUri`]: xref:CSF.Screenplay.Selenium.UseABaseUri
[`SetTheElementValue`]: xref:CSF.Screenplay.Selenium.Tasks.SetTheElementValue
[`TakeAndSaveScreenshot`]: xref:CSF.Screenplay.Selenium.Tasks.TakeAndSaveScreenshot
