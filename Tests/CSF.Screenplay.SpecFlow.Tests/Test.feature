Feature: Addition
    In order to avoid silly mistakes
    As a math idiot
    I want to keep a running count of numbers
    
Scenario: Add two numbers
  Given Joe has the number 50
   When he adds 70
   Then he should see the total 120

Scenario: Add three numbers
  Given Joe has the number 50
    And he adds 20
   When he adds 70
   Then he should see the total 140
