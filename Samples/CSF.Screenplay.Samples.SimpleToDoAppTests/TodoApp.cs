using CSF.Screenplay.Selenium.Elements;

public static class TodoApp
{
    public static NamedUri RootUri = new NamedUri("/", "The root of the todo app");

    public static Locator PageHeading = new CssSelector(".header h1", "The main page heading");

    public static Locator NewItem = new CssSelector(".header .new-todo", "The new-item entry control");

    public static Locator DisplayedTodos = new CssSelector(".main .todo-list li", "The displayed todo items");

    public static Locator TodoAtPosition(int position) => new CssSelector($".main .todo-list li:nth-child({position}) .view label", $"The text of the displayed todo item at position {position}");

    public static Locator CompletedToggle(int position) => new CssSelector($".main .todo-list li:nth-child({position}) .view input", $"The 'completed' toggle for the todo item at position {position}");

    public static Locator ShowAllFilter => new CssSelector(".footer .filters [href=\"#/\"]", "The clear-filter (show all) button");

    public static Locator ActiveOnlyFilter => new CssSelector(".footer .filters [href=\"#/active\"]", "The active-only filter button");

    public static Locator CompletedOnlyFilter => new CssSelector(".footer .filters [href=\"#/completed\"]", "The completed-only filter button");
}