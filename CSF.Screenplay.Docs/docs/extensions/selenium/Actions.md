# Selenium actions

The Selenium extension for Screenplay provides numerous [Actions], each of which represents one of the smallest possible interactions with a web browser/page.
Broadly, these actions fall into the following categories:

* Actions which work with HTML elements (targets), such as _"click on a specified element"_ or _"type some keys into an input box"_.  These actions will always require [a target] as a parameter.
* Actions which interact directly with the web browser, such as _"take a screenshot"_ or _"clear the cookies for the current domain"_.
* Actions which represent waiting for something, such as _"wait until the progress bar reaches 100%"_ or _"wait until the shopping cart items are visible"_.

Below is a summary of each of these actions' usage, with a link to their detailed documentation page.

[Actions]: ../../../glossary/Action.md
[a target]: Targets.md

## Actions that work with targets

| Action                    | Usage                                                                             |
| ------                    | -----                                                                             |
| [`ClearTheContents`]      | Remove/clear all text from the specified element, such as an `<input>`            |
| [`Click`]                 | Click on the specified element                                                    |
| [`DeselectByIndex`]       | Deselect an option from the specified `<select>` element by its index             |
| [`DeselectByText`]        | Deselect an option from the specified `<select>` element by its displayed text    |
| [`DeselectByValue`]       | Deselect an option from the specified `<select>` element by its underlying value  |
| [`DeselectAll`]           | Deselects every option from the specified `<select>` element                      |
| [`SelectByIndex`]         | Select an option from the specified `<select>` element by its index               |
| [`SelectByText`]          | Select an option from the specified `<select>` element by its displayed text      |
| [`SelectByValue`]         | Select an option from the specified `<select>` element by its underlying value    |
| [`SendKeys`]              | Type text (send keystrokes) whilst a specified element has focus                  |

[`ClearTheContents`]: xref:CSF.Screenplay.Selenium.Actions.ClearTheContents
[`Click`]: xref:CSF.Screenplay.Selenium.Actions.Click
[`DeselectByIndex`]: xref:CSF.Screenplay.Selenium.Actions.DeselectByIndex
[`DeselectByText`]: xref:CSF.Screenplay.Selenium.Actions.DeselectByText
[`DeselectByValue`]: xref:CSF.Screenplay.Selenium.Actions.DeselectByValue
[`DeselectAll`]: xref:CSF.Screenplay.Selenium.Actions.DeselectAll
[`SelectByIndex`]: xref:CSF.Screenplay.Selenium.Actions.SelectByIndex
[`SelectByText`]: xref:CSF.Screenplay.Selenium.Actions.SelectByText
[`SelectByValue`]: xref:CSF.Screenplay.Selenium.Actions.SelectByValue
[`SendKeys`]: xref:CSF.Screenplay.Selenium.Actions.SendKeys

## Actions which work with the browser

| Action                    | Usage                                                                             |
| ------                    | -----                                                                             |
| [`ClearCookies`]          | Clear the web browser cookies for the current site (domain)                       |
| [`ClearLocalStorage`]     | Clear the web browser [local storage] for the current site (domain)               |
| [`DeleteTheCookie`]       | Delete a single named cookie                                                      |
| [`ExecuteJavaScript`]     | Executes a JavaScript directly in the browser and reads the result, if applicable |
| [`OpenUrl`]               | Directs the browser to open a specified URL                                       |
| [`SaveScreenshot`]        | Saves a screenshot to a file                                                      |

[`OpenUrl`]: xref:CSF.Screenplay.Selenium.Actions.OpenUrl
[`SaveScreenshot`]: xref:CSF.Screenplay.Selenium.Actions.SaveScreenshot
[`ClearCookies`]: xref:CSF.Screenplay.Selenium.Actions.ClearCookies
[`ClearLocalStorage`]: xref:CSF.Screenplay.Selenium.Actions.ClearLocalStorage
[`DeleteTheCookie`]: xref:CSF.Screenplay.Selenium.Actions.DeleteTheCookie
[`ExecuteJavaScript`]: xref:CSF.Screenplay.Selenium.Actions.ExecuteJavaScript
[local storage]: https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage

## Actions which wait

| Action                    | Usage                                                                             |
| ------                    | -----                                                                             |
| [`Wait`]                  | Pauses the [Performance] until a condition is true                                |
| [`WaitForSomeTime`]       | Pauses the [Performance] for an arbitrary amount of time                          |

[Performance]: xref:CSF.Screenplay.IPerformance
[`Wait`]: xref:CSF.Screenplay.Selenium.Actions.Wait
[`WaitForSomeTime`]: xref:CSF.Screenplay.Selenium.Actions.WaitForSomeTime
