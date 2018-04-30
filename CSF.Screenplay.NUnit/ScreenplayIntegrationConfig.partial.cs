//
// ScreenplayIntegrationConfig.cs
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
using CSF.Screenplay.Integration;

[assembly: CSF.Screenplay.NUnit.ScreenplayAssembly(typeof(ScreenplayIntegration.ScreenplayIntegrationConfig))]

namespace ScreenplayIntegration
{
  /// <summary>
  /// A screenplay integration configuration for NUnit.  This type sets up the various services which
  /// Screenplay uses (a limited form of dependency injection).
  /// </summary>
  public partial class ScreenplayIntegrationConfig : IIntegrationConfig
  {
    // This file contains a number of examples of Screenplay integrations

    /// <summary>
    /// Configures Screenplay using a configuration builder type.
    /// </summary>
    /// <param name="builder">Builder.</param>
    public void Configure(IIntegrationConfigBuilder builder) => ConfigurePartial(builder);

    // Implement this method in a partial class in order to customize your own Screenplay integration.
    // For example:
    // 
    // partial void ConfigurePartial(IIntegrationConfigBuilder builder)
    // {
    //   // Your integration code here
    // }
    partial void ConfigurePartial(IIntegrationConfigBuilder builder);
  }
}
