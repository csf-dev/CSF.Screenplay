using System;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A supplementary builder type which enables the collection of <see cref="TimeSpan"/> instances.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When consuming <xref href="PerformableGlossaryItem?text=Performable+objects"/> it is recommended to use
    /// <xref href="BuilderPatternMainArticle?text=the+builder+pattern"/> to create them.
    /// A commonly-used 'parameter' which may be specified in builders is 'an amount of time', IE a <see cref="TimeSpan"/>.
    /// </para>
    /// <para>
    /// This builder is intended to supplement another builder, for the purpose of specifying an amount of time.
    /// The 'other' builder is passed as a constructor parameter to this builder, along with an absolute amount.
    /// The consumer should then execute one of the methods of this type, which selects the unit of time and thus
    /// determines the <see cref="TimeSpan"/> value.
    /// The method which determines the units then returns that other builder instance, allowing the building process
    /// to continue with that other builder.
    /// </para>
    /// <para>
    /// Whilst it is possible to create instances of this type via its public constructor, it is often easier to create
    /// instances using the <c>static</c> <see cref="TimeSpanBuilder"/> class.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// The example below shows how the time span builder is intended to be used.  It is consumed from within another builder,
    /// which needs to include a developer-configurable time span.
    /// See <xref href="BuilderPatternMainArticle?text=the+documentation+for+writing+performable+builders"/> for more information
    /// about the makeup of the <c>EatLunchPerformableBuilder</c>.
    /// </para>
    /// <code>
    /// public class EatLunchPerformableBuilder : IGetsPerformable
    /// {
    ///     IProvidesTimeSpan? timeSpanBuilder;
    /// 
    ///     protected string? FoodName { get; init; }
    /// 
    ///     IPerformable IGetsPerformable.GetPerformable()
    ///         =&gt; new EatLunch(FoodName, timeSpanBuilder?.GetTimeSpan() ?? TimeSpan.Zero);
    /// 
    ///     public TimeSpanBuilder&lt;EatLunchPerformableBuilder&gt; For(int howMany)
    ///     {
    ///         var builder = TimeSpanBuilder.Create(this, howMany);
    ///         timeSpanBuilder = builder;
    ///         return builder;
    ///     }
    /// 
    ///     public static EatLunchPerformableBuilder Eat(string foodName) =&gt; new EatLunchPerformableBuilder() { FoodName = foodName };
    /// }
    /// </code>
    /// <para>
    /// The sample builder above would be used to build an instance of a (fictitious) <c>EachLunch</c> performable, which derives from
    /// <see cref="IPerformable"/>.  The fictitious performable requires two parameters; the name of the food being eaten for lunch
    /// and how long the lunch break lasts. The time span builder is used for that second parameter.
    /// A consumer which uses this builder in an <see cref="IPerformance"/>, or another <xref href="PerformableGlossaryItem?text=performable"/>,
    /// might consume it as follows.
    /// </para>
    /// <code>
    /// using static EatLunchPerformableBuilder;
    /// 
    /// // ...
    /// 
    /// actor.PerformAsync(Eat("Sandwiches").For(30).Minutes(), cancellationToken);
    /// </code>
    /// <para>
    /// A note for developers with access to the source code for this library.
    /// There is a small integration test which sets up and exercises the example above; it is named <c>TimeSpanBuilderTests</c>.
    /// </para>
    /// </example>
    /// <typeparam name="TOtherBuilder">The builder type for which this builder will supplement</typeparam>
    /// <seealso cref="TimeSpanBuilder.Create{TOtherBuilder}(TOtherBuilder, int)"/>
    public class TimeSpanBuilder<TOtherBuilder> : IProvidesTimeSpan where TOtherBuilder : class
    {
        readonly TOtherBuilder otherBuilder;
        readonly int value;
        TimeSpan timespan;

        TimeSpan IProvidesTimeSpan.GetTimeSpan() => timespan;

        /// <summary>
        /// Configures the contained time span to be measured in milliseconds, then returns the contained builder.
        /// </summary>
        public TOtherBuilder Milliseconds()
        {
            timespan = TimeSpan.FromMilliseconds(value);
            return otherBuilder;
        }

        /// <summary>
        /// Configures the contained time span to be measured in seconds, then returns the contained builder.
        /// </summary>
        public TOtherBuilder Seconds()
        {
            timespan = TimeSpan.FromSeconds(value);
            return otherBuilder;
        }

        /// <summary>
        /// Configures the contained time span to be measured in minutes, then returns the contained builder.
        /// </summary>
        public TOtherBuilder Minutes()
        {
            timespan = TimeSpan.FromMinutes(value);
            return otherBuilder;
        }

        /// <summary>
        /// Configures the contained time span to be measured in hours, then returns the contained builder.
        /// </summary>
        public TOtherBuilder Hours()
        {
            timespan = TimeSpan.FromHours(value);
            return otherBuilder;
        }

        /// <summary>
        /// Configures the contained time span to be measured in days, then returns the contained builder.
        /// </summary>
        public TOtherBuilder Days()
        {
            timespan = TimeSpan.FromDays(value);
            return otherBuilder;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TimeSpanBuilder{TBuilder}"/>.
        /// </summary>
        /// <param name="value">The absolute value of time, but without units</param>
        /// <param name="otherBuilder">The other builder which shall be supplemented by this</param>
        /// <exception cref="ArgumentNullException">If <paramref name="otherBuilder"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is less than zero.</exception>
        public TimeSpanBuilder(TOtherBuilder otherBuilder, int value)
        {
            if (otherBuilder == null)
                throw new ArgumentNullException(nameof(otherBuilder));
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must not be negative");

            this.value = value;
            this.otherBuilder = otherBuilder;
        }
    }
}
