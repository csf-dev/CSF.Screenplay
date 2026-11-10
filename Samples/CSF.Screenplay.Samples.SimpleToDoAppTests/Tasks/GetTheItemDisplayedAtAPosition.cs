using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Tasks;

public class GetTheItemDisplayedAtAPosition(int position) : IPerformableWithResult<string>, ICanReport
{
    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} gets the item displayed at position {Position}", actor, position);

    public ValueTask<string> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        => actor.PerformAsync(ReadFromTheElement(TodoApp.TodoAtPosition(position)).TheText(), cancellationToken);
}