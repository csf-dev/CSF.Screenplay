using System;
namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// A type which represents a URI within a web application.
  /// </summary>
  public class AppUri : IUriProvider
  {
    /// <summary>
    /// Gets the name of the default web application.  This is used when the application name is unspecified.
    /// </summary>
    public static readonly string DefaultApplicationName = "Default";

    readonly Uri uri;
    readonly string appName;

    /// <summary>
    /// Gets the URI.
    /// </summary>
    /// <value>The URI.</value>
    public Uri Uri => uri;

    /// <summary>
    /// Gets the name of the application.
    /// </summary>
    /// <value>The name of the application.</value>
    public string ApplicationName => appName;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppUri"/> class, using a relative URI string.
    /// </summary>
    /// <param name="uri">URI.</param>
    /// <param name="appName">The name of the application.</param>
    public AppUri(string uri, string appName = null)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      this.uri = new Uri(uri, UriKind.Relative);
      this.appName = appName?? DefaultApplicationName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppUri"/> class using a <c>System.Uri</c> instance.
    /// </summary>
    /// <param name="uri">URI.</param>
    /// <param name="appName">The name of the application.</param>
    public AppUri(Uri uri, string appName = null)
    {
      if(uri == null)
        throw new ArgumentNullException(nameof(uri));

      this.uri = uri;
      this.appName = appName?? DefaultApplicationName;
    }

    /// <summary>
    /// Creates an instance of <see cref="AppUri"/> using an absolute URI string.
    /// </summary>
    /// <param name="uri">URI.</param>
    /// <param name="appName">The name of the application.</param>
    public static AppUri Absolute(string uri, string appName = null)
    {
      var uriObj = new Uri(uri, UriKind.Absolute);
      return new AppUri(uriObj, appName);
    }
  }
}
