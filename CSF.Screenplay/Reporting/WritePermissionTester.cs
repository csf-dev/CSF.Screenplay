using System;
using System.IO;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Helper class to determine whether or not we have write permission to a specified file path.
    /// </summary>
    public class WritePermissionTester : ITestsPathForWritePermissions
    {
        /// <summary>
        /// Gets a value indicating whether or not the current process has write permission to the specified path.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The path may be relative or absolute; if relative then it is treated as relative to the current working directory.
        /// </para>
        /// <para>
        /// This method will recurse in order to test the permissions for parent directories, if it finds a file or directory
        /// which does not exist.
        /// When testing for the writability of directories, this is performed by attempting to create a temporary file with
        /// a random file name; see <see cref="Path.GetRandomFileName()"/>. If we are able to create such a file then it is assumed
        /// that we have full write permissions to the directory.
        /// In this scenario, the temporary file is deleted immediately after.
        /// </para>
        /// <para>
        /// This method will return false if we reach the root directory of the filesystem.
        /// </para>
        /// </remarks>
        /// <param name="path">An absolute or relative file path.</param>
        /// <returns><see langword="true" /> if the current process is able to write to the specified path; <see langword="false" /> if not.</returns>
        public bool HasWritePermission(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            if (!Path.IsPathRooted(path))
                path = Path.Combine(Environment.CurrentDirectory, path);
            
            try
            {
                if (File.Exists(path))
                {
                    using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                        return true;
                }
                else if (Directory.Exists(path))
                {
                    var tempFilePath = Path.Combine(path, Path.GetRandomFileName());
                    try
                    {
                        using (FileStream fs = File.Create(tempFilePath))
                        {
                            // Intentional no-op, if the line above does not throw then we have created a file in that directory.
                        }
                        File.Delete(tempFilePath);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    string parentDir = Path.GetDirectoryName(path);
                    if (string.IsNullOrEmpty(parentDir))
                        return false;
                    return HasWritePermission(parentDir);
                }
            }
            catch
            {
                // The two most likely exceptions here are:
                // * UnauthorizedAccessException
                // * IOException
                // But in practice, any exception at all means we can't write a file at that path.
                return false;
            }
        }
    }
}

