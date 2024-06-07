Feature: Personas
  As a developer, I would like to encapsulate reusable attributes related
  to actors within a single class.  This includes their name and their
  standard abilities.
  This will make test logic more reusable.
  
Scenario: A persona may be used to create an actor who has the correct abilities
  Given Sarah is an actor created from a persona
    And she has the number 50
   When she adds 70
   Then she should see the total 120
