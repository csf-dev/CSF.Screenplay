using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Tasks;

public class GetTheCountOfDisplayedItems : IPerformableWithResult<int>, ICanReport
{
    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} gets the count of displayed todo items");

    public async ValueTask<int> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        var displayedTodos = await actor.PerformAsync(FindElementsOnThePage().WhichMatch(TodoApp.DisplayedTodos), cancellationToken);
        return displayedTodos.Count;
    }
}
