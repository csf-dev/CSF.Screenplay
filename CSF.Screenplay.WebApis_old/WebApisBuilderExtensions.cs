//
// JsonApisBuilderExtensions.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.WebApis.Abilities;

namespace CSF.Screenplay.WebApis
{
  /// <summary>
  /// Convenience extension methods for registering the ability to consume web APIs.
  /// </summary>
  public static class WebApisBuilderExtensions
  {
    /// <summary>
    /// Configures the current integration to make the <see cref="ConsumeWebServices"/> ability available via
    /// dependency injection.
    /// </summary>
    /// <param name="helper">The integration helper.</param>
    /// <param name="baseUri">A base URI for all API requests which have relative URIs.</param>
    /// <param name="defaultTimeout">A default timeout for API requests which do not specify an explicit timeout.</param>
    /// <param name="name">The registered name of this ability.</param>
    public static void UseWebApis(this IIntegrationConfigBuilder helper,
                                  string baseUri,
                                  TimeSpan? defaultTimeout = null,
                                  string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      helper.UseWebApis(new Uri(baseUri), defaultTimeout, name);
    }

    /// <summary>
    /// Configures the current integration to make the <see cref="ConsumeWebServices"/> ability available via
    /// dependency injection.
    /// </summary>
    /// <param name="helper">The integration helper.</param>
    /// <param name="baseUri">A base URI for all API requests which have relative URIs.</param>
    /// <param name="defaultTimeout">A default timeout for API requests which do not specify an explicit timeout.</param>
    /// <param name="name">The registered name of this ability.</param>
    public static void UseWebApis(this IIntegrationConfigBuilder helper,
                                  Uri baseUri = null,
                                  TimeSpan? defaultTimeout = null,
                                  string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      helper.ServiceRegistrations.PerScenario.Add(regBuilder => {
        regBuilder
          .RegisterFactory(() => new ConsumeWebServices(baseUri: baseUri, defaultTimeout: defaultTimeout))
          .AsOwnType()
          .WithName(name);
      });
    }
  }
}
