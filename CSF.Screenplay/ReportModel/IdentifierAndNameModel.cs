namespace CSF.Screenplay.ReportModel
{
    /// <summary>
    /// Model represents an <see cref="Performances.IdentifierAndName"/> within a Screenplay report.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Like many models in this namespace, this type mimicks a first-class part of the Screenplay architecture.
    /// This model type is intended for use with the serialization process to JSON.
    /// Many of the properties of these types will correspond directly with the same-named properties on the original
    /// Screenplay architecture types.
    /// </para>
    /// </remarks>
    public class IdentifierAndNameModel
    {
        /// <summary>
        /// Corresponds to the value <see cref="Performances.IdentifierAndName.Identifier"/>.
        /// </summary>
        public string Identifier { get; set; }
        
        /// <summary>
        /// Corresponds to the value <see cref="Performances.IdentifierAndName.Name"/>.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Corresponds to the value <see cref="Performances.IdentifierAndName.WasIdentifierAutoGenerated"/>.
        /// </summary>
        public bool WasIdentifierAutoGenerated { get; set; }
    }
}