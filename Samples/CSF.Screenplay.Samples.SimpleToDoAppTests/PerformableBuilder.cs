namespace CSF.Screenplay;

using System.Net;
using CSF.Screenplay.Tasks;
using static CSF.Screenplay.Selenium.PerformableBuilder;

public static class PerformableBuilder
{
    public static IPerformable OpenTheApp() => NavigateTo(TodoApp.RootUri);

    public static IPerformable CreateATodoReading(string text) => new CreateATodoItem(text);

    public static IPerformableWithResult<int> GetTheCountOfDisplayedTodos() => new GetTheCountOfDisplayedItems();

    public static IPerformableWithResult<string> GetTheTodoItemAtPosition(int position) => new GetTheItemDisplayedAtAPosition(position);
}