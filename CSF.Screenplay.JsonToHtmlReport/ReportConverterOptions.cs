using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Options for converting a JSON report to HTML.
    /// </summary>
    public class ReportConverterOptions
    {
        /// <summary>
        /// Gets or sets the file system path to the JSON report which is to be converted to HTML.
        /// </summary>
        public string ReportPath { get; set; }

        /// <summary>
        /// Gets or sets the file system path where the HTML report will be saved.
        /// </summary>
        public string OutputPath { get; set; } = "ScreenplayReport.html";

        /// <summary>
        /// Gets or sets a threshold (in Kilobytes) for files which should be embedded into the report.
        /// </summary>
        /// <remarks>
        /// <para>
        /// By default, the report converter will attempt to embed file data into the HTML report file.
        /// This is desirable because it means that the report file is likely to be portable as a single file, even when
        /// <xref href="AssetsArticle?text=it+contains+linked+assets"/>.  Any asset files of supported file extensions are
        /// embedded into the HTML file if their file size (in kilobytes) is less than or equal to this value.
        /// </para>
        /// <para>
        /// The default value for this property is 500 (500 kilobytes, half a megabyte). Setting this value to zero
        /// or a negative number will disable embedding of files into the report.
        /// Setting this value to an arbitrarily high number (such as 1000000, meaning a gigabyte) will cause all files to be
        /// embedded.
        /// </para>
        /// <para>
        /// The supported file extensions are listed in the option property <see cref="EmbeddedFileExtensions"/>.
        /// </para>
        /// </remarks>
        public int EmbeddedFileSizeThresholdKb { get; set; } = 500;

        /// <summary>
        /// Gets or sets a comma-separated list of file extensions which are supported for embedding into report.
        /// </summary>
        /// <remarks>
        /// <para>
        /// By default, the report converter will attempt to embed file data into the HTML report file.
        /// This is desirable because it means that the report file is likely to be portable as a single file, even when
        /// <xref href="AssetsArticle?text=it+contains+linked+assets"/>.  Any asset files with a size less than or equal to the threshold
        /// are embedded into the HTML file if their file extension is amongst those listed in this property.
        /// </para>
        /// <para>
        /// The default value for this property is <c>jpg,jpeg,png,gif,webp,svg,mp4,mov,avi,wmv,mkv,webm</c>.
        /// These are common image and video file types, seen on the web.
        /// Note that the wildcard <c>*</c>, if included anywhere this property value, denotes that files of all (any) extension
        /// should be embedded into the report.
        /// Setting this value to an empty string will disable embedding of files into the report.
        /// </para>
        /// <para>
        /// The file-size threshold for files which may be embedded into the report is controlled by the option property
        /// <see cref="EmbeddedFileSizeThresholdKb"/>.
        /// </para>
        /// </remarks>
        public string EmbeddedFileExtensions { get; set; } = "jpg,jpeg,png,gif,webp,svg,mp4,mov,avi,wmv,mkv,webm";

        /// <summary>
        /// Gets a collection of file extensions (including the leading period) which should be embedded into HTML reports.
        /// </summary>
        /// <returns>A collection of the extensions to embed</returns>
        public IReadOnlyCollection<string> GetEmbeddedFileExtensions()
        {
            if(string.IsNullOrWhiteSpace(EmbeddedFileExtensions)) return Array.Empty<string>();
            return EmbeddedFileExtensions.Split(',').Select(x => string.Concat(".", x.Trim())).ToArray();
        }

        /// <summary>
        /// Gets a value indicating whether all file types (regardless of extension) should be embedded.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method returns <see langword="true"/> if <see cref="EmbeddedFileExtensions"/> contains the character <c>*</c>.
        /// Note that a file must still have a size equal to or less than <see cref="EmbeddedFileSizeThresholdKb"/> to be embedded.
        /// </para>
        /// </remarks>
        /// <returns><see langword="true"/> if all file types are to be embedded; <see langword="false"/> if not.</returns>
        public bool ShouldEmbedAllFileTypes() => EmbeddedFileExtensions.Contains('*');
    }
}