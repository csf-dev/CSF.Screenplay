using CSF.Screenplay;
using CSF.Screenplay.Selenium.Elements;

/// <summary>
/// Constants which represent URLs and UI elements associated with the Todo app.
/// </summary>
/// <remarks>
/// <para>
/// In the Screenplay pattern (which is a heavy refactoring of the Page Object Pattern), types like this are all that remains of the Page Object Model.
/// This class carries runtime constants & functions which correspond mainly to the interactive UI elements which this test suite interacts with.
/// The only other is a named URI which represents the app's web address (within a base URL, configured in the <see cref="Webster"/> persona).
/// </para>
/// <para>
/// Obviously, the TodoMVC app is very small, so just one static class is ample to contain all these values without confusion.
/// In a more realistic-size test project, there might be many such classes, each representing either a page/screen, or even area/logical section of a screen.
/// Whatever is suitable to organise such constants/functions, appropriate to the design of the app.
/// </para>
/// <para>
/// <see cref="NamedUri"/> and <see cref="Locator"/> instances are given human-readable names, so that they are readable (to devs and non-devs alike)
/// when they show up in reports.
/// </para>
/// </remarks>
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