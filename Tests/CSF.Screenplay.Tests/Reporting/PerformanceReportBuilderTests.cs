using System.Collections.Generic;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Performances;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting;

[TestFixture, Parallelizable]
public class PerformanceReportBuilderTest
{
    [Test, AutoMoqData]
    public void GetReportShouldReturnReportWithSuccessOutcomeWhenSuccessIsTrue(PerformanceReportBuilder sut)
    {
        Assert.That(() => sut.GetReport(true)?.Outcome, Is.EqualTo(nameof(PerformanceState.Success)));
    }

    [Test, AutoMoqData]
    public void GetReportShouldReturnReportWithFailedOutcomeWhenSuccessIsFalse(PerformanceReportBuilder sut)
    {
        Assert.That(() => sut.GetReport(false)?.Outcome, Is.EqualTo(nameof(PerformanceState.Failed)));
    }

    [Test, AutoMoqData]
    public void GetReportShouldReturnReportWithCompletedOutcomeWhenSuccessIsNull(PerformanceReportBuilder sut)
    {
        Assert.That(() => sut.GetReport(null)?.Outcome, Is.EqualTo(nameof(PerformanceState.Completed)));
    }

    [Test, AutoMoqData]
    public void ActorCreatedShouldAddAnActorCreatedReportableWithTheCorrectNameAndReportText(PerformanceReportBuilder sut,
                                                                                             bool? outcome,
                                                                                             [NamedActor("Joe")] Actor actor)
    {
        sut.ActorCreated(actor);
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables, Has.One.Matches<ActorCreatedReport>(x => x.ActorName == "Joe" && x.Report == "Joe joined the performance"));
    }

    [Test, AutoMoqData]
    public void ActorGainedAbilityShouldAddAGainedAbilityReportableWithAnAbilityGeneratedReportIfItIsIReportable([Frozen] IFormatsReportFragment formatter,
                                                                                                                 PerformanceReportBuilder sut,
                                                                                                                 bool? outcome,
                                                                                                                 ICanReport ability,
                                                                                                                 [NamedActor("Joe")] Actor actor)
    {
        Mock.Get(ability).Setup(x => x.GetReportFragment(actor, formatter)).Returns(new ReportFragment("Original", "Formatted", Array.Empty<NameAndValue>()));
        sut.ActorGainedAbility(actor, ability);
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables, Has.One.Matches<ActorGainedAbilityReport>(x => x.ActorName == "Joe" && x.Report == "Formatted"));
    }

    [Test, AutoMoqData]
    public void ActorGainedAbilityShouldAddAGainedAbilityReportableWithAGeneratedReportIfItIsNotIReportable([Frozen] IGetsValueFormatter valueFormatterProvider,
                                                                                                            PerformanceReportBuilder sut,
                                                                                                            IValueFormatter valueFormatter,
                                                                                                            bool? outcome,
                                                                                                            object ability,
                                                                                                            [NamedActor("Joe")] Actor actor)
    {
        Mock.Get(valueFormatterProvider).Setup(x => x.GetValueFormatter(ability)).Returns(valueFormatter);
        Mock.Get(valueFormatter).Setup(x => x.Format(ability)).Returns("Ability Name");
        sut.ActorGainedAbility(actor, ability);
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables, Has.One.Matches<ActorGainedAbilityReport>(x => x.ActorName == "Joe" && x.Report == "Joe is able to Ability Name"));
    }

    [Test, AutoMoqData]
    public void ActorSpotlitShouldAddASpotlitReportableWithTheCorrectReportText(PerformanceReportBuilder sut,
                                                                                [NamedActor("Joe")] Actor actor,
                                                                                bool? outcome)
    {
        sut.ActorSpotlit(actor);
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables, Has.One.Matches<ActorSpotlitReport>(x => x.ActorName == "Joe" && x.Report == "Joe was put into the spotlight"));
    }

    [Test,AutoMoqData]
    public void SpotlightTurnedOffShouldAddASpotlightOffReportableWithTheCorrectReportText(PerformanceReportBuilder sut,
                                                                                           bool? outcome)
    {
        sut.SpotlightTurnedOff();
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables, Has.One.Matches<SpotlightTurnedOffReport>(x => x.ActorName == null && x.Report == "The spotlight was turned off"));
    }

    [Test, AutoMoqData]
    public void BeginAndEndPerformableShouldAddAPerformableReportableWithCorrectValuesIfPerformableIsReportable([Frozen] IFormatsReportFragment formatter,
                                                                                                                PerformanceReportBuilder sut,
                                                                                                                [NamedActor("Joe")] Actor actor,
                                                                                                                bool? outcome,
                                                                                                                StartTheStopwatch performable,
                                                                                                                string performancePhase)
    {
        Mock.Get(formatter).Setup(x => x.Format(It.IsAny<string>(), actor)).Returns(new ReportFragment("Original", "Formatted", Array.Empty<NameAndValue>()));
        sut.BeginPerformable(performable, actor, performancePhase);
        sut.EndPerformable(performable, actor);
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables,
                    Has.One.Matches<PerformableReport>(x => x.ActorName == "Joe"
                                                            && x.Report == "Formatted"
                                                            && x.PerformancePhase == performancePhase
                                                            && x.PerformableType == "CSF.Screenplay.Performables.StartTheStopwatch"));
    }

    [Test, AutoMoqData]
    public void BeginAndEndPerformableShouldAddAPerformableReportableWithFallbackReportIfPerformableIsNotReportable(PerformanceReportBuilder sut,
                                                                                                                [NamedActor("Joe")] Actor actor,
                                                                                                                bool? outcome,
                                                                                                                string performable,
                                                                                                                string performancePhase)
    {
        sut.BeginPerformable(performable, actor, performancePhase);
        sut.EndPerformable(performable, actor);
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables,
                    Has.One.Matches<PerformableReport>(x => x.Report == "Joe performed System.String"));
    }
}