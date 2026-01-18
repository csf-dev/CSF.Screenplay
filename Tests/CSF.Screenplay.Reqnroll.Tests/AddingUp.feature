Feature: Adding up numbers

    As a math idiot, I want to be able to add numbers to a running total, to test the Screenplay/Reqnroll integration

Scenario: Mathias should be able to add two numbers together and get the correct result
    Given Mathias has the number 50
     When he adds 70
     Then he should have the total 120

Scenario: Mathias should be able to add three numbers together and get the correct result
    Given Mathias has the number 50
     When he adds 70
      And he adds 20
     Then he should have the total 140

Scenario: Mathias should be able to add three numbers in a single step
    Given Mathias has the number 50
     When he adds 40, 30 and 20
     Then he should have the total 140