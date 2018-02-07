using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSF.WebDriverExtras.Flags;

namespace CSF.Screenplay.Selenium
{
  public sealed class GetBrowserFlagsDefinitions
  {
    #region constants

    const string FlagsResourceSuffix = ".flags.json";

    #endregion

    #region fields

    readonly IReadsFlagsDefinitions definitionReader;
    readonly object syncRoot;
    IReadOnlyCollection<FlagsDefinition> cachedDefinitions;

    #endregion

    #region properties

    Assembly ThisAssembly => Assembly.GetExecutingAssembly();

    #endregion

    #region public methods

    public IReadOnlyCollection<FlagsDefinition> GetDefinitions() => GetDefinitions(ThisAssembly);

    public IReadOnlyCollection<FlagsDefinition> GetDefinitions(Assembly assembly)
    {
      if(assembly == null)
        throw new ArgumentNullException(nameof(assembly));

      if(assembly != ThisAssembly)
      {
        return GetDefinitionsFromAssembly(assembly);
      }

      lock(syncRoot)
      {
        if(cachedDefinitions != null)
          return cachedDefinitions;

        cachedDefinitions = GetDefinitionsFromAssembly(assembly);
        return cachedDefinitions;
      }
    }

    #endregion

    #region methods

    IReadOnlyCollection<FlagsDefinition> GetDefinitionsFromAssembly(Assembly assembly)
    {
      var resourceNames = GetDefinitionsResourceNames(assembly);
      return resourceNames
        .SelectMany(x => GetDefinitions(x, assembly))
        .ToArray();
    }

    IEnumerable<string> GetDefinitionsResourceNames(Assembly assembly)
      => assembly
        .GetManifestResourceNames()
        .Where(x => x.EndsWith(FlagsResourceSuffix, StringComparison.InvariantCulture));

    IReadOnlyCollection<FlagsDefinition> GetDefinitions(string resourceName, Assembly assembly)
    {
      using(var stream = assembly.GetManifestResourceStream(resourceName))
        return definitionReader.GetFlagsDefinitions(stream);
    }

    #endregion

    #region constructor

    public GetBrowserFlagsDefinitions() : this(null) {}

    public GetBrowserFlagsDefinitions(IReadsFlagsDefinitions definitionReader)
    {
      syncRoot = new object();
      this.definitionReader = definitionReader ?? new DefinitionReader();
    }

    #endregion

    #region static methods

    public static IReadOnlyCollection<FlagsDefinition> FromDefinitionsAssembly()
      => new GetBrowserFlagsDefinitions().GetDefinitions();

    #endregion
  }
}
