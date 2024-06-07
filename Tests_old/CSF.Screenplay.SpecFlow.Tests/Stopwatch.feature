Feature: The stopwatch
  In order to record how long certain test-related actions
  take.
  As a developer, I want to be able to include a stopwatch
  in my tests, and record the results of the timing in Screenplay
  reports.

Scenario: Read the time
  Given Joe has a stopwatch
    And he has started timing
   When he waits for 1550 milliseconds
    And he stops the stopwatch
   Then the stopwatch should read at least 1550 milliseconds
