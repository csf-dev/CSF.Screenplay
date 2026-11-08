using CSF.Screenplay.Selenium.Elements;

public static class TodoApp
{
    public static NamedUri RootUri = new NamedUri("/", "The root of the todo app");

    public static Locator PageHeading = new CssSelector(".header h1", "The main page heading");

    public static Locator NewItem = new CssSelector(".header .new-todo", "The new-item entry control");

    public static Locator DisplayedTodos = new CssSelector(".main .todo-list li", "The displayed todo items");

    public static Locator TodoAtPosition(int position) => new CssSelector($".main .todo-list li:nth-child({position}) .view label", $"The text of the displayed todo item at position {position}");
}