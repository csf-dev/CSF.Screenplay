# Screenplay for testing

The Screenplay pattern is a recommended tool for writing automated tests for application software.
Unlike [NUnit] or [SpecFlow] or many others, Screenplay is not a complete testing framework. 
Rather, Screenplay integrates with your chosen testing framework to assist in the writing of test logic.

As such, *it is not a silver bullet*.
Some tests will benefit from using Screenplay and others will not.
Three testing scenarios are explored below, with an analysis of whether Screenplay is likely to be relevant. 
Since terminology can differ between developers, each type of test includes a short definition.

[NUnit]: TODO
[SpecFlow]: TODO


## Ideal: Acceptance tests
Automated acceptance tests test *the full deployed application* with no test fakes involved. The tests interact with the app in the same way that *a real user* would, by controlling the app's user interface. These are also known as "end to end" or "system" tests.

Acceptance tests are **an ideal candidate** for Screenplay test logic.

## Unsuitable: Unit tests
Unit tests test a single small unit of code, such as a class, in isolation. Anything external to the tested unit is replaced with [a test fake] or mock.

[a test fake]: https://en.wikipedia.org/wiki/Mock_object

Because the test & expected outcomes are specific only to that unit's responsibility, Screenplay is going to offer very little value.

* Test steps will be short and simple, without need for code reuse
* Test steps can be difficult to describe from the overall 'behaviour domain' because the unit under test might only represent a small aspect of a more complex behaviour

It is *not recommended* to use Screenplay for unit testing.

## Perhaps useful: Integration tests
Integration tests live at a point in 'the testing spectrum' between unit & acceptance testing. Like unit tests, they execute code directly instead of testing the deployed application. Unlike unit tests, each integration test executes code across many units of code/classes. It's common for integration tests to test high-level APIs which represent real user use-cases.

Integration tests *may* include some test fakes, but usually these are limited to things which are difficult or expensive to control. For example asynchronous web services & databases.

It is recommended to *evaluate for yourself* as to whether you can benefit from using Screenplay with integration tests. Some teams & projects will find great value from it and others will not.