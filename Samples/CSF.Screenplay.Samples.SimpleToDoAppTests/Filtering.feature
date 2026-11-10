Feature: Filtering the todo list

    Users should be able to filter their todo items.

Background:
    Given Webster has opened the todo app
    And he has created several todo items
        | Item          |
        | feed the cat  |
        | feed the dog  |
        | buy milk      |

Scenario: The Active filter should not show completed items
    When he marks the item at position 1 as completed
    And he marks the item at position 2 as completed
    And he clicks the filter named Active
    Then the count of displayed todos should be 1

Scenario: The Completed filter should not show completed items
    When he marks the item at position 1 as completed
    And he marks the item at position 2 as completed
    And he clicks the filter named Completed
    Then the count of displayed todos should be 2

Scenario: The All filter should show all items
    When he marks the item at position 1 as completed
    And he marks the item at position 2 as completed
    And he clicks the filter named Completed
    And he clicks the filter named All
    Then the count of displayed todos should be 3