using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Tasks;

public class ToggleAnItemsCompletedState(int position) : IPerformable, ICanReport
{
    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} toggles the completion state of the item at position {Position}", actor, position);

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        => actor.PerformAsync(ClickOn(TodoApp.CompletedToggle(position)));
}