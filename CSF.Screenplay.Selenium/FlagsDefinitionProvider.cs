using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSF.WebDriverExtras.Flags;

namespace CSF.Screenplay.Selenium
{
  public class FlagsDefinitionProvider : IProvidesFlagsDefinitions
  {
    readonly IReadsFlagsDefinitions definitionReader;
    readonly IReadOnlyCollection<string> extraDefinitionFilePaths;
    readonly Encoding fileEncoding;

    public IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions()
    {
      var baseDefinitions = GetBrowserFlagsDefinitions.FromDefinitionsAssembly();
      var fileDefinitions = extraDefinitionFilePaths.SelectMany(x => ReadDefinitionsFromFile(x));

      return baseDefinitions.Union(fileDefinitions).ToArray();
    }

    IReadOnlyCollection<FlagsDefinition> ReadDefinitionsFromFile(string filePath)
    {
      Stream stream = null;

      try
      {
        stream = File.OpenRead(filePath);
        return ReadDefinitionsFromFileStream(stream, filePath);
      }
      // In any of these exception cases, where we can't open the file, drop the error to the console and skip the file
      catch(PathTooLongException ex) { return GetBadDefinitionsFileResult(ex, filePath); }
      catch(DirectoryNotFoundException ex) { return GetBadDefinitionsFileResult(ex, filePath); }
      catch(UnauthorizedAccessException ex) { return GetBadDefinitionsFileResult(ex, filePath); }
      catch(FileNotFoundException ex) { return GetBadDefinitionsFileResult(ex, filePath); }
      catch(IOException ex) { return GetBadDefinitionsFileResult(ex, filePath); }
      // And finally clean up the stream
      finally
      {
        if(stream != null)
          stream.Dispose();
        stream = null;
      }
    }

    IReadOnlyCollection<FlagsDefinition> ReadDefinitionsFromFileStream(Stream stream, string filePath)
    {
      try
      {
        return definitionReader.GetFlagsDefinitions(stream);
      }
      catch(Exception ex)
      {
        Console.Error.WriteLine("WARNING: Skipped browser flags definition file '{0}' because reading the file raised an exception:{1}{2}",
                                filePath,
                                Environment.NewLine,
                                ex);

        return new FlagsDefinition[0];
      }
    }

    IReadOnlyCollection<FlagsDefinition> GetBadDefinitionsFileResult(Exception ex, string filePath)
    {
      Console.Error.WriteLine("WARNING: Skipped browser flags definition file '{0}' because opening the file raised an exception:{1}{2}",
                              filePath,
                              Environment.NewLine,
                              ex);

      return new FlagsDefinition[0];
    }

    public FlagsDefinitionProvider(params string[] extraDefinitionFilePaths)
      : this(Encoding.UTF8, extraDefinitionFilePaths) { }

    public FlagsDefinitionProvider(Encoding fileEncoding, params string[] extraDefinitionFilePaths)
    {
      if(fileEncoding == null)
        throw new ArgumentNullException(nameof(fileEncoding));
      
      this.fileEncoding = fileEncoding;
      this.extraDefinitionFilePaths = extraDefinitionFilePaths ?? new string[0];
      this.definitionReader = new DefinitionReader();
    }
  }
}
