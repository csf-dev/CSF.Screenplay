using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverFactory.Impl
{
  public class RemoteWebDriverFromEnvironmentFactory : RemoteWebDriverFactory
  {
    public string BrowserNameVar { get; set; }

    public string BrowserVersionVar { get; set; }

    public string PlatformVar { get; set; }

    public string RemoteAddressVar { get; set; }

    public string CommandTimeoutSecondsVar { get; set; }

    protected override Uri GetRemoteUri()
    {
      if(String.IsNullOrEmpty(RemoteAddressVar))
      {
        return base.GetRemoteUri();
      }

      var uri = GetEnv(RemoteAddressVar);
      return new Uri(uri);
    }

    protected override TimeSpan GetTimeout()
    {
      if(String.IsNullOrEmpty(CommandTimeoutSecondsVar))
      {
        return base.GetTimeout();
      }

      int seconds;
      var val = GetEnv(CommandTimeoutSecondsVar);
      if(Int32.TryParse(val, out seconds))
      {
        return TimeSpan.FromSeconds(seconds);
      }

      return base.GetTimeout();
    }

    protected override ICapabilities GetCapabilities()
    {
      var caps = new DesiredCapabilities();

      SetCapabilityToEnvOrSetting(caps, CapabilityType.BrowserName, BrowserNameVar, BrowserName);
      SetCapabilityToEnvOrSetting(caps, CapabilityType.Version, BrowserVersionVar, BrowserVersion);
      SetCapabilityToEnvOrSetting(caps, CapabilityType.Platform, PlatformVar, Platform);

      return caps;
    }

    protected virtual void SetCapabilityToEnvOrSetting(DesiredCapabilities caps, string name, string envName, string value)
    {
      if(String.IsNullOrEmpty(envName))
      {
        base.SetCapabilityIfNotNull(caps, name, value);
        return;
      }

      var val = GetEnv(envName);
      caps.SetCapability(name, val);
    }

    string GetEnv(string name)
    {
      return Environment.GetEnvironmentVariable(name);
    }
  }
}
