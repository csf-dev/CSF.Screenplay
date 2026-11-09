using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Tasks;

public class CreateATodoItem(string text) : IPerformable, ICanReport
{
    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} creates a todo item reading \"{text}\"", actor, text);

    public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        => actor.PerformAsync(EnterTheText(text, Keys.Enter).Into(TodoApp.NewItem), cancellationToken);
}
