# Screenplay implementation
This is an implementation of the **[Screenplay]** pattern (which also been known in the past as the **Journey** pattern).
Screenplay is an architectural pattern for writing automated Behaviour-Driven-Development (BDD) acceptance tests.
It is inspired by some of the work performed within the **[Serenity BDD]** library, although sets out with some slightly less ambitious aims.

Screenplay really comes into its own when used to describe *actor-driven test scenarios*, written from the perspective of an end-user of the application software.
It may be used to 'drive' **[Selenium]** or other front-end testing frameworks.
When used with Selenium, Screenplay could be thought of as a specialisation of the [Page Object Pattern], after heavy refactoring to pay closer attention to the [SOLID design principles] - *particularly the Single Responsibility and Open/Closed principles*.

[Screenplay]: https://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay
[Serenity BDD]: https://github.com/serenity-bdd
[Selenium]: http://www.seleniumhq.org/
[Page Object Pattern]: https://martinfowler.com/bliki/PageObject.html
[SOLID design principles]: https://en.wikipedia.org/wiki/SOLID_(object-oriented_design)

## What this library has to offer
**`CSF.Screenplay`** is a lightweight implementation of the Screenplay pattern, for .NET 4.5+ (and Mono).
Its goals are narrower than those of Serenity BDD in that it does not set out to be a complete testing framework.
The ramifications of this are as follows.

### No need to handle assertions
It is assumed that the coder is using a testing library (rather obviously).
Such a library will come with its own assertion methods/functionality in order to match the return values (answers) from questions with expectations.
`CSF.Screenplay` does not attempt to muscle-in on this territory.
You should continue to use your chosen testing framework for matching the responses from questions.
If you wish to use a more human-readable framework for this matching, then you may wish to look into something like [Fluent Assertions].

[Fluent Assertions]: http://fluentassertions.com/

