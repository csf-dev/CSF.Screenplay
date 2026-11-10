Feature: Opening the app

Scenario: Webster should be able to open the app in a web browser
    When Webster opens the todo app
    Then the page heading should read "todos"