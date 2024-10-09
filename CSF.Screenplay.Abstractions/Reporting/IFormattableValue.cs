namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which has its own functionality for generating a human-readable representation of itself for a Screenplay report.
    /// </summary>
    public interface IFormattableValue
    {
        /// <summary>
        /// Gets a formatted string which represents the current object instance.
        /// </summary>
        /// <returns>A formatted string which represents the current instance.</returns>
        string Format();
    }
}