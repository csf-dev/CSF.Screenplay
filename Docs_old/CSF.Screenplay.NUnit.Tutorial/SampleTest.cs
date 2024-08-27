using CSF.Screenplay;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Selenium.Abilities;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;


[TestFixture]
public class SampleTest
{
  [Test, Screenplay]
  public void A_new_item_can_be_added_to_an_empty_list(ICast cast, BrowseTheWeb browseTheWeb)
  {
    var joe = cast.Get("Joe");
    joe.IsAbleTo(browseTheWeb);

    // Given Joe opens an empty to-do list
    Given(joe).WasAbleTo<OpenAnEmptyToDoList>();

    // When Joe adds a new item called "Wash dishes"
    When(joe).AttemptsTo(AddAToDoItem.Named("Wash dishes"));

    // Then the top item should be called "Wash dishes"
    var theText = Then(joe).ShouldRead(TheTopToDoItem.FromTheList());
    Assert.That(theText, Is.EqualTo("Wash dishes"));
  }

  [Test, Screenplay]
  public void A_new_item_is_added_to_the_top_of_the_list(ICast cast, BrowseTheWeb browseTheWeb)
  {
    var joe = cast.Get("Joe");
    joe.IsAbleTo(browseTheWeb);

    // Given Joe opens an empty to-do list
    Given(joe).WasAbleTo<OpenAnEmptyToDoList>();
    // And he has added an item called "Buy bread"
    Given(joe).WasAbleTo(AddAToDoItem.Named("Buy bread"));

    // When Joe adds a new item called "Buy shampoo"
    When(joe).AttemptsTo(AddAToDoItem.Named("Buy shampoo"));

    // Then the top item should be called "Buy shampoo"
    var theText = Then(joe).ShouldRead(TheTopToDoItem.FromTheList());
    Assert.That(theText, Is.EqualTo("Buy shampoo"));
  }
}

