Feature: Creating some Todo items

    Users should be able to create a number of Todo items.
    Each item is independent and adds to the list of displayed items.

Scenario: A user can create a single todo and it appears in the list
    Given Webster has opened the todo app
    When he creates a todo item reading feed the cat
    Then the count of displayed todos should be 1

Scenario: A user can create three todos and they all appear in the list
    Given Webster has opened the todo app
    When he creates several todo items
        | Item          |
        | feed the cat  |
        | feed the dog  |
        | buy milk      |
    Then the count of displayed todos should be 3

Scenario: Todo items should be listed in inverse order in which they are created
    Given Webster has opened the todo app
    When he creates several todo items
        | Item          |
        | feed the cat  |
        | feed the dog  |
        | buy milk      |
    Then the item displayed at position 3 should read feed the cat
    And the item displayed at position 1 should read buy milk