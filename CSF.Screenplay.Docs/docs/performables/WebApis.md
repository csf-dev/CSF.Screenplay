---
uid: WebApisArticle
---

# Web APIs

Screenplay may be used to interact with Web APIs.
Key to this is [Ability] class [`MakeWebApiRequests`], along with a number of performables and types which represent API endpoints.

## Use `WebApiBuilder` to simplify usage

The section & table below indicates the combinations of 'endpoint' & performable to use for several common use cases.
If the correct endpoint has been used though, the [`WebApiBuilder`] class will make it very easy to select the correct performable, by type inference.

For any requests which are expecting to read a response as a JSON string, to be deserialized, use an overload of `GetTheJsonResult` from the static web API builder.
For any other requests, use an overload of `SendTheHttpRequest`.

The recommended way to use this builder is via `using static CSF.Screenplay.WebApis.WebApiBuilder;` in the source file
for a custom [performable].

[`WebApiBuilder`]: xref:CSF.Screenplay.WebApis.WebApiBuilder
[performable]: ../../glossary/Performable.md

## Combinations of endpoints & performables for common usages

The performable which should be used, along with the approproate endpoint type depends upon your use case, summarised in the table below.
The table is organised by the expected body/content of the HTTP request, _the request payload_ and the expected type of the response body.

Where _None_ is listed, this usually means that either the request or response have no body, such as an HTTP GET request that does not send a body or an empty response.
In the case of responses this might alternatively mean that the response body will be ignored or will not be interpreted as an instance of any particular type.

| Request payload               | Response type                     | Endpoint type                     | Performable type                              |
| ---------------               | -------------                     | -------------                     | ----------------                              |
| _None_                        | _None_                            | [`Endpoint`]                      | [`SendTheHttpRequest`]                        |
| _None_                        | Deserialized with custom logic    | [`Endpoint<TResponse>`]                   | [`SendTheHttpRequestAndGetTheResponse<T>`]    |
| _None_                        | Deserialized from JSON            | [`Endpoint<TResponse>`]                   | [`SendTheHttpRequestAndGetJsonResponse<T>`]   |
| Serialized with custom logic  | _None_                            | Derive from [`ParameterizedEndpoint<TParameters>`]      | [`SendTheHttpRequest`]                        |
| Serialized with custom logic  | Deserialized with custom logic    | Derive from [`ParameterizedEndpoint<TParameters,TResponse>`]  | [`SendTheHttpRequestAndGetTheResponse<T>`]    |
| Serialized with custom logic  | Deserialized from JSON            | Derive from [`ParameterizedEndpoint<TParameters,TResponse>`]  | [`SendTheHttpRequestAndGetJsonResponse<T>`]   |
| Serialized with JSON          | _None_                            | [`JsonEndpoint<TParameters>`]               | [`SendTheHttpRequest`]                        |
| Serialized with JSON          | Deserialized with custom logic    | [`JsonEndpoint<TParameters,TResponse>`]           | [`SendTheHttpRequestAndGetTheResponse<T>`]    |
| Serialized with JSON          | Deserialized from JSON            | [`JsonEndpoint<TParameters,TResponse>`]           | [`SendTheHttpRequestAndGetJsonResponse<T>`]   |

> [!TIP]
> The rule to decide which types of endpoint & performable to choose is:
> _Choose the **endpoint type** based upon the needs of the **request**, adding an extra generic type parameter if the response is to be strongly-typed.
> Choose the **performable type** based upon how you intend to read the **response**._

[Ability]: ../../glossary/Ability.md
[`MakeWebApiRequests`]: xref:CSF.Screenplay.WebApis.MakeWebApiRequests
[`Endpoint`]: xref:CSF.Screenplay.WebApis.Endpoint
[`Endpoint<TResponse>`]: xref:CSF.Screenplay.WebApis.Endpoint`1
[`ParameterizedEndpoint<TParameters>`]: xref:CSF.Screenplay.WebApis.ParameterizedEndpoint`1
[`ParameterizedEndpoint<TParameters,TResponse>`]: xref:CSF.Screenplay.WebApis.ParameterizedEndpoint`2
[`JsonEndpoint<TParameters>`]: xref:CSF.Screenplay.WebApis.JsonEndpoint`1
[`JsonEndpoint<TParameters,TResponse>`]: xref:CSF.Screenplay.WebApis.JsonEndpoint`2
[`SendTheHttpRequest`]: xref:CSF.Screenplay.WebApis.SendTheHttpRequest
[`SendTheHttpRequestAndGetTheResponse<T>`]: xref:CSF.Screenplay.WebApis.SendTheHttpRequestAndGetTheResponse`1
[`SendTheHttpRequestAndGetJsonResponse<T>`]: xref:CSF.Screenplay.WebApis.SendTheHttpRequestAndGetJsonResponse`1
