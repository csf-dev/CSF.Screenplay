# Where Screenplay is suitable for testing

The Screenplay pattern is a recommended tool for writing automated tests for application software.
Unlike [NUnit] or [SpecFlow] (or many others) Screenplay is not a complete testing framework.
Rather, Screenplay integrates with your chosen testing framework to assist in the writing of test logic.

_Screenplay is not a silver bullet_; some kinds of tests could benefit from Screenplay and others will not.
Some testing scenarios are listed below, along with a brief consideration as to whether Screenplay is likely to be relevant.
Terminology can differ between developers, so each type of test begins with a short definition.

[NUnit]: https://nunit.org/
[SpecFlow]: https://specflow.org/

## Ideal: System tests

Also known as "end-to-end tests"; this kind of test exercises and tests _the full deployed application_ without the use of any [test fakes].
The application might be set up/configured in a controlled environment but it runs and is exercised in the same way it would in production.

System tests are **an ideal candidate** for the use of Screenplay.
System tests are often written from a user's perspective and test meaningful, observable behaviour of the application, such as a complete [user story].
Screenplay excels in this area of test design.

[test fakes]: https://en.wikipedia.org/wiki/Test_double
[user story]: https://en.wikipedia.org/wiki/User_story

## Unsuitable: Unit tests

Unit tests test the functionality of a small unit of code in isolation.
Typically this is a single class or function.
Anything external to the tested unit is replaced with [a test fake] or [a mock].

[a test fake]: https://en.wikipedia.org/wiki/Test_double
[a mock]: https://en.wikipedia.org/wiki/Mock_object

Screenplay is **not recommended** for use in unit testing.
Unit tests are rarely written from an end-user's perspective and often do not test _complete_ application behaviour, such as a [user story].
A unit test will only test only a small aspect of some functionality.
When this aspect is considered in isolation it may be meaningless, or too abstract to comprehend for an end-user.
Additionally:

* The steps involved in unit tests should be short & simple, with minimal need for code reuse or composition
* The steps are typically difficult to describe in the application's ['behaviour domain'], only with language that a software developer would understand

['behaviour domain']: https://en.wikipedia.org/wiki/Domain_(software_engineering)

## Perhaps useful: Integration tests

Integration tests live at a point in _the spectrum of testing_ between unit & system tests.
Like unit tests, they execute logic via its programming API instead of testing the deployed application.
Unlike unit tests, each integration test exercises logic across many units of code/classes.
Thus, it is common for integration tests to test high-level APIs which represent complete [user stories].
Integration tests _might include_ some [test fakes].
Usually these are limited to things which are difficult or expensive to control.
Asynchronous web services & databases are typically replaced with test fakes in integration tests.

For integration tests, we recommend that you **evaluate for yourself** whether Screenplay is useful.
In some projects/applications, Screenplay will be excellent for integration tests.
In others it might not.

You will find Screenplay most useful if your integration tests test complete user stories.
This is particularly true if the test/sample scenarios would be recognisable to end users, even if described in an abstract manner, for example without reference to a user interface.

[user stories]: https://en.wikipedia.org/wiki/User_story

## Recommended: Use BDD-style tests

Screenplay is a great tool when used alongside [Behaviour Driven Development] (BDD).
Whilst the use of a BDD framework such as [SpecFlow] is not at all mandatory, those familiar with BDD will quickly see the synergies with Screenplay.

[Behaviour Driven Development]: https://en.wikipedia.org/wiki/Behavior-driven_development
