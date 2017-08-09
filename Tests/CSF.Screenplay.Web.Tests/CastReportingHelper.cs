using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Web.Tests
{
  public class CastReportingHelper : NUnit.CastReportingHelper
  {
    protected override void HandleActorCreatedInCast(object sender, ActorEventArgs e)
    {
      base.HandleActorCreatedInCast(sender, e);
      GrantWebBrowsingAbility(e.Actor);
    }

    void GrantWebBrowsingAbility(IActor actor)
    {
      var browseTheWeb = Context.GetWebBrowsingAbility();
      if(browseTheWeb == null)
        return;

      actor.IsAbleTo(browseTheWeb);
    }

    public CastReportingHelper(ICast cast, IReporter reporter, ScreenplayContext context)
      : base(cast, reporter, context) {}
  }
}
