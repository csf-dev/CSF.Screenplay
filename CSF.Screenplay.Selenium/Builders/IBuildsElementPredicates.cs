
using System;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;
using CSF.Specifications;
/// <summary>
/// An object which may be converted into either a <see cref="WaitUntilPredicate{Boolean}"/> or an <see cref="ISpecificationFunction{SeleniumElement}"/>.
/// </summary>
public interface IBuildsElementPredicates
{
    /// <summary>
    /// Gets a <see cref="WaitUntilPredicate{Boolean}"/> which may be used to configure a <see cref="WaitUntilPredicate{Boolean}"/>.
    /// </summary>
    /// <returns>A wait-until predicate object</returns>
    /// <exception cref="InvalidOperationException">If this instance has not been initialised in a manner compatible with creating Web Driver predicates</exception>
    WaitUntilPredicate<bool> GetWaitUntilPredicate();

    /// <summary>
    /// Gets a specification function which may be used to filter Selenium elements.
    /// </summary>
    /// <returns>A specification function for Selenium elements</returns>
    ISpecificationFunction<SeleniumElement> GetElementSpecification();
}
