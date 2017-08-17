using System;
using System.Collections.Generic;
using CSF.WebDriverFactory.Config;
using CSF.WebDriverFactory.Impl;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.WebDriverFactory.Tests
{
  [TestFixture,Parallelizable]
  public class ConfigurationWebDriverFactoryProviderTests
  {
    [TestCase(typeof(ChromeWebDriverFactory))]
    [TestCase(typeof(FirefoxGeckoDriverWebDriverFactory))]
    [TestCase(typeof(InternetExplorerWebDriverFactory))]
    [TestCase(typeof(RemoteWebDriverFactory))]
    [TestCase(typeof(RemoteWebDriverFromEnvironmentFactory))]
    public void GetFactory_gets_factory_of_appropriate_type(Type expectedType)
    {
      // Arrange
      var config = GetConfig(expectedType);
      var sut = new ConfigurationWebDriverFactoryProvider(config);

      // Act
      var result = sut.GetFactory();

      // Assert
      Assert.IsInstanceOf(expectedType, result);
    }

    [AutoData]
    public void GetFactory_sets_CommandTimeoutSeconds_property(int commandTimeout)
    {
      // Arrange
      var props = new Dictionary<string,string> {
        { nameof(ChromeWebDriverFactory.CommandTimeoutSeconds), commandTimeout.ToString() }
      };
      var config = GetConfig(typeof(ChromeWebDriverFactory), props);
      var sut = new ConfigurationWebDriverFactoryProvider(config);

      // Act
      var result = (ChromeWebDriverFactory) sut.GetFactory();

      // Assert
      Assert.AreEqual(commandTimeout, result.CommandTimeoutSeconds);
    }

    IWebDriverFactoryConfiguration GetConfig(Type factoryType,
                                             IDictionary<string,string> properties = null)
    {
      var config = Mock.Of<IWebDriverFactoryConfiguration>();
      Mock.Get(config)
          .Setup(x => x.GetFactoryType())
          .Returns(factoryType);
      Mock.Get(config)
          .Setup(x => x.GetFactoryProperties())
          .Returns(properties?? new Dictionary<string,string>());
      return config;
    }
  }
}
