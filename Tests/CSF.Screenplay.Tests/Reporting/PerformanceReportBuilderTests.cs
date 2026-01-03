using System.Collections.Generic;
using System.Linq;
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
        Mock.Get(valueFormatter).Setup(x => x.FormatForReport(ability)).Returns("Ability Name");
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
    public void BeginAndEndPerformableShouldBeAbleToCreateAHierarchyOfPerformableReports([Frozen] IFormatsReportFragment formatter,
                                                                                         PerformanceReportBuilder sut,
                                                                                         [NamedActor("Joe")] Actor actor,
                                                                                         bool? outcome,
                                                                                         TaskPerformable performable1,
                                                                                         StartTheStopwatch performable2,
                                                                                         StopTheStopwatch performable3,
                                                                                         string performancePhase)
    {
        Mock.Get(formatter).Setup(x => x.Format(It.IsAny<string>(), actor)).Returns(new ReportFragment("Original", "Formatted", Array.Empty<NameAndValue>()));
        sut.BeginPerformable(performable1, actor, performancePhase);
        sut.BeginPerformable(performable2, actor, performancePhase);
        sut.EndPerformable(performable2, actor);
        sut.BeginPerformable(performable3, actor, performancePhase);
        sut.EndPerformable(performable3, actor);
        sut.EndPerformable(performable1, actor);
        var report = sut.GetReport(outcome);

        Assert.Multiple(() =>
        {
            Assert.That(report.Reportables, Has.Count.EqualTo(1), "Only one reportable should be present at root level");

            var taskReport = report.Reportables.OfType<PerformableReport>().Single();
            Assert.That(taskReport.Reportables, Has.Count.EqualTo(2), "Two child reports should be present");
            Assert.That(taskReport.Reportables.OfType<PerformableReport>().Select(x => x.PerformableType),
                        Is.EqualTo(new[] { "CSF.Screenplay.Performables.StartTheStopwatch", "CSF.Screenplay.Performables.StopTheStopwatch" }),
                        "The child reports should be of the correct performable types");
        });
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

    [Test, AutoMoqData]
    public void RecordAssetForCurrentPerformableShouldAddAnAssetToTheReportable(PerformanceReportBuilder sut,
                                                                                [NamedActor("Joe")] Actor actor,
                                                                                bool? outcome,
                                                                                object performable,
                                                                                string performancePhase,
                                                                                string assetFilename,
                                                                                string assetSummary)
    {
        sut.BeginPerformable(performable, actor, performancePhase);
        sut.RecordAssetForCurrentPerformable(assetFilename, assetSummary);
        sut.EndPerformable(performable, actor);
        var report = sut.GetReport(outcome);

        Assert.That(report.Reportables.OfType<PerformableReport>().Single().Assets,
                    Has.One.Matches<PerformableAsset>(x => x.FilePath == assetFilename && x.FileSummary == assetSummary));
    }

    [Test, AutoMoqData]
    public void RecordResultForCurrentPerformableShouldAddTheFormattedResultToTheReportable([Frozen] IGetsValueFormatter valueFormatterProvider,
                                                                                            PerformanceReportBuilder sut,
                                                                                            [NamedActor("Joe")] Actor actor,
                                                                                            bool? outcome,
                                                                                            object performable,
                                                                                            string performancePhase,
                                                                                            IValueFormatter valueFormatter,
                                                                                            object result,
                                                                                            string resultText)
    {
        Mock.Get(valueFormatterProvider).Setup(x => x.GetValueFormatter(It.IsAny<object>())).Returns(valueFormatter);
        Mock.Get(valueFormatter).Setup(x => x.FormatForReport(result)).Returns(resultText);
        sut.BeginPerformable(performable, actor, performancePhase);
        sut.RecordResultForCurrentPerformable(result);
        sut.EndPerformable(performable, actor);
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables,
                    Has.One.Matches<PerformableReport>(x => x.Result == resultText));
    }

    [Test, AutoMoqData]
    public void RecordFailureForCurrentPerformableShouldAddTheExceptionStringToTheReportable(PerformanceReportBuilder sut,
                                                                                             [NamedActor("Joe")] Actor actor,
                                                                                             bool? outcome,
                                                                                             string performable,
                                                                                             string performancePhase)
    {
        sut.BeginPerformable(performable, actor, performancePhase);
        sut.RecordFailureForCurrentPerformable(new Exception("An error occurred"));
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables,
                    Has.One.Matches<PerformableReport>(x => x.Exception.Contains("An error occurred") && x.ExceptionIsFromConsumedPerformable == false));
    }

    [Test, AutoMoqData]
    public void RecordFailureForCurrentPerformableShouldSetExceptionIsFromConsumedPerformableToTrueIfTheExceptionIsPerformableException(PerformanceReportBuilder sut,
                                                                                                                                        [NamedActor("Joe")] Actor actor,
                                                                                                                                        bool? outcome,
                                                                                                                                        string performable,
                                                                                                                                        string performancePhase)
    {
        sut.BeginPerformable(performable, actor, performancePhase);
        sut.RecordFailureForCurrentPerformable(new PerformableException("An error occurred"));
        var report = sut.GetReport(outcome);
        Assert.That(report.Reportables,
                    Has.One.Matches<PerformableReport>(x => x.Exception.Contains("An error occurred") && x.ExceptionIsFromConsumedPerformable == true));
    }

    public class TaskPerformable : IPerformable, ICanReport
    {
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} starts then stops their stopwatch after a short pause", actor);

        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            await actor.PerformAsync(new StartTheStopwatch(), cancellationToken);
            await Task.Delay(50, cancellationToken);
            await actor.PerformAsync(new StopTheStopwatch(), cancellationToken);
        }
    }
}