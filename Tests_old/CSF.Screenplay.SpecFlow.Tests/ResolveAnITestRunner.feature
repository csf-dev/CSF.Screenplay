Feature: Resolving an ITestRunner
  As a developer who writes acceptance tests,
  I should be able to write SpecFlow bindings which resolve instances
  of standard SpecFlow services.
  
Scenario: A binding should be able to resolve an ITestRunner
  Given I have an instance of a SpecFlow binding class which includes a Lazy ITestRunner
   When I resolve that test runner instance
   Then the test runner instance should not be null

Scenario: A binding should be able to subclass the SpecFlow Steps class
  Given I have an instance of a SpecFlow binding class which derives from the Steps class
   When I make use of a sample method from that binding class which returns the string 'Hello'
   Then the returned value should be 'Hello'

Scenario: It should be possible to execute composite, high-level binding steps
  Given I have an instance of a SpecFlow binding class which derives from the Steps class
   When I execute a composite binding from the steps binding
   Then if nothing crashed then the test passes
