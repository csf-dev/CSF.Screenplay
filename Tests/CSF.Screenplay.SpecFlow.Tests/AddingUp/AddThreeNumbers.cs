using static CSF.Screenplay.AddingUp.AddingUpBuilder;

namespace CSF.Screenplay.AddingUp
{
    public class AddThreeNumbers(int number1, int number2, int number3) : IPerformable, ICanReport
    {
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} adds three numbers to the running total: {One}, {Two} & {Three}", actor, number1, number2, number3);

        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            await actor.PerformAsync(Add(number1), cancellationToken);
            await actor.PerformAsync(Add(number2), cancellationToken);
            await actor.PerformAsync(Add(number3), cancellationToken);
        }
    }
}