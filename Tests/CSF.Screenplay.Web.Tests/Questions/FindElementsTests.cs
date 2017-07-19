﻿using System;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Tests.Pages;
using NUnit.Framework;
using FluentAssertions;
using static CSF.Screenplay.StepComposer;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tests.Questions
{
  [TestFixture]
  public class FindElementsTests
  {
    Actor joe;

    [SetUp]
    public void Setup()
    {
      joe = WebdriverTestSetup.GetJoe();
    }

    [Test]
    public void FindElements_In_gets_expected_count_of_elements()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      Then(joe)
        .ShouldSee(Elements.In(PageTwo.ListOfItems).Get())
        .Elements.Count.Should().Be(5);
    }

    [Test]
    public void FindElements_OnThePage_gets_expected_count_of_elements()
    {
      Given(joe).WasAbleTo(OpenTheirBrowserOn.ThePage<PageTwo>());

      var theDesiredItems = new CssSelector("#list_of_items li", "the items in the list");

      Then(joe)
        .ShouldSee(Elements.OnThePage().ThatAre(theDesiredItems).Called("the listed items"))
        .Elements.Count.Should().Be(5);
    }
  }
}
