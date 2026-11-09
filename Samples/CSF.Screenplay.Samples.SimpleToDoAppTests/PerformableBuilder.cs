using CSF.Screenplay.Tasks;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay;

/// <summary>
/// Builder class which runs tasks.
/// </summary>
/// <remarks>
/// <para>
/// This class is an example of <xref href="BuilderPatternMainArticle?text=a+performable+builder"/> which is used to construct performable instances.
/// </para>
/// </remarks>
public static class TodoPerformableBuilder
{
    public static IPerformable OpenTheApp() => NavigateTo(TodoApp.RootUri);

    public static IPerformable CreateATodoReading(string text) => new CreateATodoItem(text);

    public static IPerformableWithResult<int> GetTheCountOfDisplayedTodos() => new GetTheCountOfDisplayedItems();

    public static IPerformableWithResult<string> GetTheTodoItemAtPosition(int position) => new GetTheItemDisplayedAtAPosition(position);

    public static IPerformable ActivateAFilter(string name) => new ActivateAFilter(name);

    public static IPerformable ToggleTheCompletedStateOfItem(int position) => new ToggleAnItemsCompletedState(position);
}