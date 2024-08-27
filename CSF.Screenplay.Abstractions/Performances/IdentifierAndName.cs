using System;

namespace CSF.Screenplay.Performances
{
    /// <summary>A model which indicates a unique identifier and a corresponding human-readable name.</summary>
    public sealed class IdentifierAndName : IEquatable<IdentifierAndName>, IHasName
    {
        /// <summary>Gets the identifier for the current item</summary>
        /// <remarks>
        /// <para>The identifier for an item might not be a human-readable value. It is required to uniquely identify the current item, however.</para>
        /// </remarks>
        public string Identifier { get; }

        /// <summary>Gets a human-readable name for the current item</summary>
        /// <remarks>
        /// <para>The human-readable name in this context is not mandatory, and so this property might return a <see langword="null" />
        /// reference if no name was specified.</para>
        /// </remarks>
        public string Name { get; }

        /// <summary>Gets a value indicating whether or not the <see cref="Identifier"/> is an automatically-generated value or not.</summary>
        /// <remarks>
        /// <para>
        /// Some integrations with Screenplay do not provide a suitable unique identifier for this position.
        /// In that case, because a unique identifier is required, Screenplay generates a unique identifier (the string representation of a
        /// GUID) automatically and uses it as the <see cref="Identifier"/>.
        /// Obviously, if that is the case then the identifier will have no meaning or correspondence to anything in the logic consuming
        /// Screenplay.
        /// </para>
        /// <para>This property is used to indicate when this is the case; if it is <see langword="true" /> then the Identifier was
        /// randomly-generated by Screenplay.</para>
        /// </remarks>
        public bool WasIdentifierAutoGenerated { get; }

        /// <inheritdoc/>
        public bool Equals(IdentifierAndName other)
        {
            if(ReferenceEquals(other, null)) return false;
            if(ReferenceEquals(other, this)) return true;

            return string.Equals(Identifier, other.Identifier, StringComparison.InvariantCulture);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as IdentifierAndName);

        /// <inheritdoc/>
        public override int GetHashCode() => Identifier.GetHashCode();

        /// <summary>Initialises a new instance of <see cref="IdentifierAndName"/></summary>
        /// <param name="identifier">The identifier for this item, which might not be human-readable</param>
        /// <param name="name">A human-readable name for this item</param>
        /// <param name="wasIdentifierAutoGenerated">A value that indicates whether or not
        /// <paramref name="identifier"/> is an auto-generated value</param>
        /// <exception cref="ArgumentNullException">If <paramref name="identifier"/> is <see langword="null" /></exception>
        public IdentifierAndName(string identifier, string name = null, bool wasIdentifierAutoGenerated = false)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            Name = name;
            WasIdentifierAutoGenerated = wasIdentifierAutoGenerated;
        }
    }
}