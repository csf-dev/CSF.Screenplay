using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSF.WebDriverExtras.Flags;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Helper type which gets browser flags definitions stored inside an assembly as embedded resources,
  /// using the resource suffix <c>.flags.json</c>.
  /// </summary>
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

    /// <summary>
    /// Gets the browser flags definitions contained within the <c>CSF.Screenplay.Selenium.BrowserFlags</c> assembly.
    /// </summary>
    /// <returns>The browser flags definitions.</returns>
    public IReadOnlyCollection<FlagsDefinition> GetDefinitions() => GetDefinitions(ThisAssembly);

    /// <summary>
    /// Gets the browser flags definitions contained within the given assembly.
    /// </summary>
    /// <returns>The browser flags definitions.</returns>
    /// <param name="assembly">An assembly to search for flags definitions.</param>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.GetBrowserFlagsDefinitions"/> class.
    /// </summary>
    public GetBrowserFlagsDefinitions() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.GetBrowserFlagsDefinitions"/> class.
    /// </summary>
    /// <param name="definitionReader">Definition reader.</param>
    public GetBrowserFlagsDefinitions(IReadsFlagsDefinitions definitionReader)
    {
      syncRoot = new object();
      this.definitionReader = definitionReader ?? new DefinitionReader();
    }

    #endregion

    #region static methods

    /// <summary>
    /// Helper method which gets the flags definitions from the <c>CSF.Screenplay.Selenium.BrowserFlags</c> assembly.
    /// </summary>
    /// <returns>The dlags definitions.</returns>
    public static IReadOnlyCollection<FlagsDefinition> FromDefinitionsAssembly()
      => new GetBrowserFlagsDefinitions().GetDefinitions();

    #endregion
  }
}
