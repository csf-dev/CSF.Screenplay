---
uid: SeleniumQueriesArticle
---

# Queries

Queries are a unified mechanism by which [Elements] are interrogated/observed.
Queries broadly serve two purposes, as described below.

[Elements]: xref:CSF.Screenplay.Selenium.Elements.SeleniumElement

## Observing the state of elements

Queries are used directly by the [`SingleElementQuery<TResult>`] and [`ElementCollectionQuery<TResult>`] questions which are included in the Selenium extension for Screenplay.
In this usage, the query indicates which piece of state is to be retrieved from the element(s) and returned to the consuming performable.

[`SingleElementQuery<TResult>`]: xref:CSF.Screenplay.Selenium.Questions.SingleElementQuery`1
[`ElementCollectionQuery<TResult>`]: xref:CSF.Screenplay.Selenium.Questions.ElementCollectionQuery`1

## Specifying criteria

A mechanism which parallels queries is present in the class [`QueryPredicatePrototypeBuilder`].
This mechanism uses the combination of a query and a predicate to specify criteria by which element(s) may be matched.
This applies to two use-cases:

The methods of [`TargetExtensions`] may be used to build instances of the [`Wait`] action.
Together, the query and predicate specify the condition for the wait to end.

The query prototype builder is also used by one of the overloads of the [`ForThoseWhich`] builder method.
This builder may be used to create the [`FilterElements`] question, to specify the criteria by which to match elements which should be accepted by the filter.

[`QueryPredicatePrototypeBuilder`]: xref:CSF.Screenplay.Selenium.Builders.QueryPredicatePrototypeBuilder
[`TargetExtensions`]: xref:CSF.Screenplay.Selenium.Elements.TargetExtensions
[`Wait`]: xref:CSF.Screenplay.Selenium.Actions.Wait
[`ForThoseWhich`]: xref:CSF.Screenplay.Selenium.Builders.FilterElementsBuilder.ForThoseWhich(System.Func{CSF.Screenplay.Selenium.Builders.QueryPredicatePrototypeBuilder,CSF.Screenplay.Selenium.Builders.IBuildsElementPredicates})
[`FilterElements`]: xref:CSF.Screenplay.Selenium.Questions.FilterElements

## List of queries

The available queries are best found by reading the list of methods available on two classes.
For usages of queries to observe/read a piece of information, see the functionality present upon the [`QuestionQueryBuilder`] class.
For usages relating the the creation of criteria, see the members of the [`QueryPredicatePrototypeBuilder`] class.
Recall, each query reads a piece of information from [an HTML element] (or [elements in a collection]), or it creates a criteria for matching either one or a collection of elements.

[`QuestionQueryBuilder`]: xref:CSF.Screenplay.Selenium.Builders.QuestionQueryBuilder
[an HTML element]: xref:CSF.Screenplay.Selenium.Elements.SeleniumElement
[elements in a collection]: xref:CSF.Screenplay.Selenium.Elements.SeleniumElementCollection
