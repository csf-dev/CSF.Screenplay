using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public interface ITarget : IHasTargetName
  {
    By GetWebDriverLocator();
  }
}
