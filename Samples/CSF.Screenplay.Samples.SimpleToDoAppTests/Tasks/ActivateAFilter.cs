using System;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Tasks;
public class ActivateAFilter(string filterName) : IPerformable, ICanReport
{
    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} activates the filter {Name}", actor, filterName);

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        var filter = filterName.ToLowerInvariant() switch
        {
            "all" => TodoApp.ShowAllFilter,
            "active" => TodoApp.ActiveOnlyFilter,
            "completed" => TodoApp.CompletedOnlyFilter,
            _ => throw new InvalidOperationException("The filter name must be one of 'all', 'active' or 'completed' (case-invariant)")
        };
        return actor.PerformAsync(ClickOn(filter));
    }
}