Feature: Calculator

Simple calculator for adding and multiplying two numbers

Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

Scenario: Multiply two numbers
    Given the first number is 5
    And the second number is 7
    When the two numbers are multiplied
    Then the result should be 35