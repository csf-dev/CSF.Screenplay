# Selenium questions

The Selenium extension for Screenplay provides [Questions], each of which observes the web browser and its current contents in some manner, returning a result.
There are three broad categories of questions in the Selenium extension:

* Questions which read something from a specified element or elements, such as _"get the text of the username label"_ or _"get the color of each of the status indicators"_.
* Questions which find or query-for HTML elements, such as _find all checkboxes in the to-do list which are checked_ or _get the first status label which has a red background_.
* Questions which interact directly with the web browser.

What is common to all Screenplay Questions is that they get information, or a result object.
They do not make changes to the state of the application/web browser.
To interact with the web page in a way which could change its state, use [one of the Selenium extension's Actions].
Below is a summary of each of these questions' usage, with a link to their detailed documentation page.

[Questions]: ../../../glossary/Question.md
[one of the Selenium extension's Actions]: Actions.md

## Questions which query the state of elements

Questions which read-from or observe HTML elements operate using a common mechamism, named [Queries].
Queries provide a builder-friendly manner by which to read state from either one or a collection of elements.
Each of the Questions listed below makes use of a Query to identify the state which is to be read from the element.

| Question                                  | Usage                                                                 |
| --------                                  | -----                                                                 |
| [`SingleElementQuery<TResult>`]           | Reads the state from a [target] representing a single element         |
| [`ElementCollectionQuery<TResult>`]       | Reads the state from a [target] representing a collection of elements |

[Queries]: Queries.md
[`SingleElementQuery<TResult>`]: xref:CSF.Screenplay.Selenium.Questions.SingleElementQuery`1
[`ElementCollectionQuery<TResult>`]: xref:CSF.Screenplay.Selenium.Questions.ElementCollectionQuery`1
[target]: Targets.md

## Questions which find or filter elements

There are many ways to use these questions, chosen by the way in which you use the relevant builder(s).
The possibilities are explained in detail on the questions' detailed documentation pages.

| Question                              | Usage                                                                     |
| --------                              | -----                                                                     |
| [`FindElement`]                       | Gets [an element] which matches a [`Locator`]                             |
| [`FindElements`]                      | Gets [a collection of elements] which match a [`Locator`]                 |
| [`FilterElements`]                    | Filters [a collection of elements] for those which match a [query]        |

[`FindElement`]: xref:CSF.Screenplay.Selenium.Questions.FindElement
[`FindElements`]: xref:CSF.Screenplay.Selenium.Questions.FindElements
[`FilterElements`]: xref:CSF.Screenplay.Selenium.Questions.FilterElements
[`Locator`]: xref:CSF.Screenplay.Selenium.Elements.Locator
[query]: Queries.md
[an element]: xref:CSF.Screenplay.Selenium.Elements.SeleniumElement
[a collection of elements]: xref:CSF.Screenplay.Selenium.Elements.SeleniumElementCollection

## Questions for the web browser

These questions don't neccesarily involve elements, instead they query the web browser directly.

| Question                                      | Usage                                                                 |
| --------                                      | -----                                                                 |
| [`ExecuteJavaScriptAndGetResult<TResult>`]    | Executes a JavaScript directly in the browser and reads the result    |
| [`GetWindowTitle`]                            | Reads the text of the Window/Tab title                                |
| [`TakeScreenshot`]                            | Takes a Screenshot of the browser window                              |

[`ExecuteJavaScriptAndGetResult<TResult>`]: xref:CSF.Screenplay.Selenium.Questions.ExecuteJavaScriptAndGetResult`1
[`GetWindowTitle`]: xref:CSF.Screenplay.Selenium.Questions.GetWindowTitle
[`TakeScreenshot`]: xref:CSF.Screenplay.Selenium.Questions.TakeScreenshot

