using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Performables.TimeSpanBuilderTests.EatLunchPerformableBuilder;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Performables;

[TestFixture,Parallelizable]
public class TimeSpanBuilderTests
{
    [Test,AutoMoqData]
    public async Task GetTimeSpanShouldReturnACorrectTimeSpanInTheContextOfALargerBuilderIntegrationTest(Actor actor, string foodName)
    {
        object? performable = null;
        void OnBeginPerformable(object? sender, PerformableEventArgs ev) => performable = ev.Performable;

        actor.BeginPerformable += OnBeginPerformable;
        await When(actor).AttemptsTo(Eat(foodName).For(10).Minutes());
        actor.BeginPerformable -= OnBeginPerformable;

        Assert.Multiple(() =>
        {
            Assert.That(performable, Is.Not.Null, "Performable must not be null");
            Assert.That(performable, Is.InstanceOf<EatLunch>(), $"Performable must not be an instance of {nameof(EatLunch)}");
            Assert.That(performable, Has.Property(nameof(EatLunch.FoodName)).EqualTo(foodName), $"The performable must have the correct {nameof(EatLunch.FoodName)}");
            Assert.That(performable, Has.Property(nameof(EatLunch.HowLong)).EqualTo(TimeSpan.FromMinutes(10)), $"The performable must have the correct {nameof(EatLunch.HowLong)}");
        });
    }

    [Test,AutoMoqData]
    public void MillisecondsShouldCreateAnAmountInMilliseconds(object otherBuilder)
    {
        var sut = TimeSpanBuilder.Create(otherBuilder, 20);
        sut.Milliseconds();
        Assert.That(((IProvidesTimeSpan)sut).GetTimeSpan(), Is.EqualTo(TimeSpan.FromMilliseconds(20)));
    }

    [Test,AutoMoqData]
    public void SecondsShouldCreateAnAmountInSeconds(object otherBuilder)
    {
        var sut = TimeSpanBuilder.Create(otherBuilder, 20);
        sut.Seconds();
        Assert.That(((IProvidesTimeSpan)sut).GetTimeSpan(), Is.EqualTo(TimeSpan.FromSeconds(20)));
    }

    [Test,AutoMoqData]
    public void MinutesShouldCreateAnAmountInMinutes(object otherBuilder)
    {
        var sut = TimeSpanBuilder.Create(otherBuilder, 20);
        sut.Minutes();
        Assert.That(((IProvidesTimeSpan)sut).GetTimeSpan(), Is.EqualTo(TimeSpan.FromMinutes(20)));
    }

    [Test,AutoMoqData]
    public void HoursShouldCreateAnAmountInHours(object otherBuilder)
    {
        var sut = TimeSpanBuilder.Create(otherBuilder, 20);
        sut.Hours();
        Assert.That(((IProvidesTimeSpan)sut).GetTimeSpan(), Is.EqualTo(TimeSpan.FromHours(20)));
    }

    [Test,AutoMoqData]
    public void DaysShouldCreateAnAmountInDays(object otherBuilder)
    {
        var sut = TimeSpanBuilder.Create(otherBuilder, 20);
        sut.Days();
        Assert.That(((IProvidesTimeSpan)sut).GetTimeSpan(), Is.EqualTo(TimeSpan.FromDays(20)));
    }

    // Note that this class is identical to the example in the docco comments for TimeSpanBuilder<TOtherBuilder>
    public class EatLunchPerformableBuilder : IGetsPerformable
    {
        IProvidesTimeSpan? timeSpanBuilder;

        protected string? FoodName { get; init; }

        IPerformable IGetsPerformable.GetPerformable()
            => new EatLunch(FoodName, timeSpanBuilder?.GetTimeSpan() ?? TimeSpan.Zero);

        public TimeSpanBuilder<EatLunchPerformableBuilder> For(int howMany)
        {
            var builder = TimeSpanBuilder.Create(this, howMany);
            timeSpanBuilder = builder;
            return builder;
        }

        public static EatLunchPerformableBuilder Eat(string foodName) => new EatLunchPerformableBuilder() { FoodName = foodName };
    }

    public class EatLunch(string? foodName, TimeSpan howLong) : IPerformable
    {
        public string? FoodName { get; } = foodName;
        public TimeSpan HowLong { get; } = howLong;

        // Intentional no-op, this is just a testing class.
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
            => ValueTask.CompletedTask;
    }
}