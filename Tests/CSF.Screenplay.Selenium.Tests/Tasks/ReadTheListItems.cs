using System.Collections.Generic;
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks;

public class ReadTheListItems : IPerformableWithResult<IReadOnlyList<string>>, ICanReport
{
    static readonly Locator listItems = new CssSelector("li", "the list items");

    readonly ITarget target;

    public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        => formatter.Format("{Actor} reads the text from list items within {Target}", actor, target);

    public async ValueTask<IReadOnlyList<string>> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    {
        var items = await actor.PerformAsync(FindElementsWithin(target).WhichMatch(listItems), cancellationToken);
        return await actor.PerformAsync(ReadFromTheCollectionOfElements(items).Text(), cancellationToken);
    }

    public ReadTheListItems(ITarget target)
    {
        this.target = target ?? throw new System.ArgumentNullException(nameof(target));
    }
}

public static class ReadTheListItemsBuilder
{
    public static IPerformableWithResult<IReadOnlyList<string>> ReadTheListItemsIn(ITarget target)
        => new ReadTheListItems(target);
}