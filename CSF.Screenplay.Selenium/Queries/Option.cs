using System;

namespace CSF.Screenplay.Selenium.Queries
{
    /// <summary>
    /// Represents a single HTML <c>&lt;option&gt;</c> element within an HTML <c>&lt;select&gt;</c> element.
    /// </summary>
    public sealed class Option : IEquatable<Option>, IHasName
    {
        /// <summary>
        /// Gets the option's displayed text.
        /// </summary>
        public string Text { get; }
        
        /// <summary>
        /// Gets the option's underlying value.
        /// </summary>
        public string Value { get; }

        string IHasName.Name => Text;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Option option && Equals(option);

        /// <inheritdoc/>
        public bool Equals(Option other) => other != null
                                         && string.Equals(Text, other.Text, StringComparison.InvariantCulture)
                                         && string.Equals(Value, other.Value, StringComparison.InvariantCulture);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Text, Value);

        /// <inheritdoc/>
        public override string ToString() => Text;

        /// <summary>
        /// Initializes a new instance of the <see cref="Option"/> class with the specified text and value.
        /// </summary>
        /// <param name="text">The displayed text of the option.</param>
        /// <param name="value">The underlying value of the option.</param>
        public Option(string text, string value)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
