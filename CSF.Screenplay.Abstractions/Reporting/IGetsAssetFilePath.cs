namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// A service which gets a filesystem path to which Screenplay asset files should be written, if they are to be written at all.
    /// </summary>
    public interface IGetsAssetFilePath
    {
        /// <summary>
        /// Gets the filesystem path to which an asset file should be written.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If reporting is disabled, for the same reasons as <see cref="IGetsReportPath.GetReportPath"/> would return <see langword="null" />,
        /// then this method will also return <see langword="null" />.
        /// In that case, reporting is disabled and no asset files should be written to the file system.
        /// </para>
        /// <para>
        /// If reporting is enabled, then this method should return an absolute file system path to which an asset file should be written,
        /// where the asset has the specified 'base name'.  That base name should be a short filename fragment which describes the asset.
        /// This file name will be embellished with other information by this method, such as to ensure that the file name is unique within
        /// the current Screenplay run.
        /// </para>
        /// </remarks>
        /// <param name="baseName">A short &amp; descriptive filename fragment, which includes the file extension but no path information</param>
        /// <returns>An absolute file system path at which the asset file should be saved, or a <see langword="null" /> reference indicating that
        /// the asset file should not be saved.</returns>
        string GetAssetFilePath(string baseName);
    }
}