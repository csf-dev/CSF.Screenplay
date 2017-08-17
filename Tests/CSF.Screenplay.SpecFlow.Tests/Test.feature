Feature: Addition
    In order to avoid silly mistakes
    As a math idiot
    I want to keep a running count of numbers
    
Scenario: Add two numbers
  Given Joe has the number 50
   When Joe adds 70
   Then Joe should see the total 120

Scenario: Add three numbers
  Given Joe has the number 50
    And Joe adds 20
   When Joe adds 70
   Then Joe should see the total 140
