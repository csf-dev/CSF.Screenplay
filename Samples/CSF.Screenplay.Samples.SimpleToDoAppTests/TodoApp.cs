using CSF.Screenplay.Selenium.Elements;

public static class TodoApp
{
    public static NamedUri RootUri = new NamedUri("/", "The root of the todo app");

    public static Locator PageHeading = new CssSelector("body h1", "The main page heading");
}