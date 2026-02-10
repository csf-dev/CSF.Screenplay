# Request actions

Sending an HTTP request to an API is accomplished using a Screenplay [Action].
There are three action types available; which to use depends upon the expected response from the API.
[This article helps you choose the appropriate combination of Request Action and Endpoint types].

[Action]: ../../../glossary/Action.md
[This article helps you choose the appropriate combination of Request Action and Endpoint types]: ChoosingEndpointsAndRequests.md

## The three action types

As might be evident from their names, each request action type is used for a different kind of expected response message.
In the case of `SendTheHttpRequest`, there is no response expected (or the response will be ignored).
The other two types are for either a JSON response or a response which requires custom deserialization.

* [`SendTheHttpRequest`](xref:CSF.Screenplay.WebApis.SendTheHttpRequest)
* [`SendTheHttpRequestAndGetJsonResponse<TResponse>`](xref:CSF.Screenplay.WebApis.SendTheHttpRequestAndGetJsonResponse`1)
* [`SendTheHttpRequestAndGetTheResponse<TResponse>`](xref:CSF.Screenplay.WebApis.SendTheHttpRequestAndGetTheResponse`1)

## Use `WebApiBuilder` to simplify usage

A builder/helper class is available to simplify getting the appropriate Request action; this is [`WebApiBuilder`].
The recommended way to consume this is to add `using static CSF.Screenplay.WebApis.WebApiBuilder;` to the source file for any [Performable] you write which consumes Web APIs via this extension.

When an approproate [Endpoint] type has been used, the [`WebApiBuilder`] class will make it very easy to select the correct action, via type inference.
To get a request for an API function which is expected to return a JSON-formatted response, use the method `GetTheJsonResult`.
To get a request for any other API function, use the method `SendTheHttpRequest`.
Both of these methods have several overloads, for each type of endpoint which could use them.

[`WebApiBuilder`]: xref:CSF.Screenplay.WebApis.WebApiBuilder
[Performable]: ../../../glossary/Performable.md
[Endpoint]: Endpoints.md
