using System;
using System.IO;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Tests
{
  public class ScreenplayAssemblyAttribute : NUnit.ScreenplayAssemblyAttribute
  {
    protected override IUriTransformer GetUriTransformer()
      => new RootUriPrependingTransformer("http://localhost:8080/");

    protected override void ConfigureActorsInCast(ICast cast)
    {
      cast.ActorCreated += Cast_ActorCreated;
    }

    protected override void WriteReport(Report report)
    {
      using(var writer = new StreamWriter("screenplay-report.txt"))
      {
        var reportWriter = new TextReportWriter(writer);
        reportWriter.Write(report);
        writer.Flush();
      }
    }

    void Cast_ActorCreated(object sender, ActorEventArgs e)
    {
      GrantWebBrowsingAbility(e.Actor);
    }

    void GrantWebBrowsingAbility(IActor actor)
    {
      var browseTheWeb = Context.GetWebBrowsingAbility();
      if(browseTheWeb == null)
        return;

      actor.IsAbleTo(browseTheWeb);
    }
  }
}
