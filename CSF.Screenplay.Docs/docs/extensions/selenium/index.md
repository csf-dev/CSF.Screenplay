---
uid: SeleniumArticle
---

# CSF.Screenplay.Selenium Extension

The Selenium extension allows [Actors] to control web browsers using [Selenium WebDriver] technology.
The control of web browsers in this manner is the origin of [the Screenplay pattern], in which it was an improvement and thorough refactoring of the older [Page Object pattern].

[Actors]: xref:CSF.Screenplay.Actor
[Selenium WebDriver]: https://www.selenium.dev/
[the Screenplay pattern]: https://www.browserstack.com/guide/screenplay-pattern-approach-in-selenium
[Page Object pattern]: https://martinfowler.com/bliki/PageObject.html

## Core contents of this extension

Like many extensions, the Selenium extension provides:

* The [Ability] [`BrowseTheWeb`], which provides access to the WebDriver API
* Several [Actions, which allow an actor to interact with the web page]
* Several [Questions, which allow an actor to read or observe the web page]
* [Tasks, for commonly-performed combinations of actions & questions]
* Models representing the UI, URLs and areas of the page
  * The [`NamedUri`] class is used to navigate to specific pages
  * Types which provide [references to HTML elements on the page], deriving from [`ITarget`]

Many of the [Performables] provided by this extension operate in the format _"Actor performs X action upon Y element"_.
A core benefit of Screenplay (over Page Object pattern) is the separation of _the functionality_ (the logic of the interaction) and _the UI elements_ (the target of the interaction).
This improves reusability and composability.

[Ability]: ../../../glossary/Ability.md
[`BrowseTheWeb`]: xref:CSF.Screenplay.Selenium.BrowseTheWeb
[Actions, which allow an actor to interact with the web page]: Actions.md
[Questions, which allow an actor to read or observe the web page]: Questions.md
[Tasks, for commonly-performed combinations of actions & questions]: Tasks.md
[`NamedUri`]: xref:CSF.Screenplay.Selenium.NamedUri
[`ITarget`]: xref:CSF.Screenplay.Selenium.Elements.ITarget
[references to HTML elements on the page]: Targets.md
[Performables]: ../../../glossary/Performable.md

## Additional contents

The types listed above are the core of the Selenium plugin, but there's more available.

* This extension integrates with [the Browser Quirks feature of CSF.Extensions.WebDriver].  That allows it to provide integrated workarounds for the quirks of some browsers. These workarounds are amongst the Tasks noted above.
* This extension provides a [`Color`] struct which provides a cross-browser way of describing sRGB color and testing for equality.
* For occasions on which JavaScript must be sent to the browser, this extension provides [Named Script] classes which offer a type-safe mechanism to refer to those scripts from .NET code.

[the Browser Quirks feature of CSF.Extensions.WebDriver]: https://csf-dev.github.io/CSF.Extensions.WebDriver/docs/Quirks.html
[`Color`]: xref:CSF.Screenplay.Selenium.Color
[Named Script]: NamedScripts.md
