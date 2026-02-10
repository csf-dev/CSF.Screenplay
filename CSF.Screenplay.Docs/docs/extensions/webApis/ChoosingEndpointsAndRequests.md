# Common combinations of requests and endpoints

The appropriate combination of [Request action] type and [Endpoint] type depends upon your use case.
Common use cases are summarised in the table below.
Information about reading this table follows.

| Request payload               | Response type                     | Endpoint type                                                 | Request action type                           |
| ---------------               | -------------                     | -------------                                                 | ----------------                              |
| _None_                        | _None_                            | [`Endpoint`]                                                  | [`SendTheHttpRequest`]                        |
| _None_                        | Deserialized with custom logic    | [`Endpoint<TResult>`]                                         | [`SendTheHttpRequestAndGetTheResponse<T>`]    |
| _None_                        | Deserialized from JSON            | [`Endpoint<TResult>`]                                         | [`SendTheHttpRequestAndGetJsonResponse<T>`]   |
| Serialized with custom logic  | _None_                            | Derive from [`ParameterizedEndpoint<TParameters>`]            | [`SendTheHttpRequest`]                        |
| Serialized with custom logic  | Deserialized with custom logic    | Derive from [`ParameterizedEndpoint<TParameters,TResult>`]    | [`SendTheHttpRequestAndGetTheResponse<T>`]    |
| Serialized with custom logic  | Deserialized from JSON            | Derive from [`ParameterizedEndpoint<TParameters,TResult>`]    | [`SendTheHttpRequestAndGetJsonResponse<T>`]   |
| Serialized with JSON          | _None_                            | [`JsonEndpoint<TParameters>`]                                 | [`SendTheHttpRequest`]                        |
| Serialized with JSON          | Deserialized with custom logic    | [`JsonEndpoint<TParameters,TResult>`]                         | [`SendTheHttpRequestAndGetTheResponse<T>`]    |
| Serialized with JSON          | Deserialized from JSON            | [`JsonEndpoint<TParameters,TResult>`]                         | [`SendTheHttpRequestAndGetJsonResponse<T>`]   |

> [!TIP]
> To decide which types of endpoint & performable:
> _Choose the **endpoint type** based upon the needs of the **request**, adding an extra generic type parameter if the response is to be strongly-typed.
> Choose the **action type** based upon the technical details of reading the **response**._

[Request action]: Requests.md
[Endpoint]: Endpoints.md
[`Endpoint`]: xref:CSF.Screenplay.WebApis.Endpoint
[`Endpoint<TResult>`]: xref:CSF.Screenplay.WebApis.Endpoint`1
[`ParameterizedEndpoint<TParameters>`]: xref:CSF.Screenplay.WebApis.ParameterizedEndpoint`1
[`ParameterizedEndpoint<TParameters,TResult>`]: xref:CSF.Screenplay.WebApis.ParameterizedEndpoint`2
[`JsonEndpoint<TParameters>`]: xref:CSF.Screenplay.WebApis.JsonEndpoint`1
[`JsonEndpoint<TParameters,TResult>`]: xref:CSF.Screenplay.WebApis.JsonEndpoint`2
[`SendTheHttpRequest`]: xref:CSF.Screenplay.WebApis.SendTheHttpRequest
[`SendTheHttpRequestAndGetTheResponse<T>`]: xref:CSF.Screenplay.WebApis.SendTheHttpRequestAndGetTheResponse`1
[`SendTheHttpRequestAndGetJsonResponse<T>`]: xref:CSF.Screenplay.WebApis.SendTheHttpRequestAndGetJsonResponse`1

## The first two columns

The first two columns indicate:

1. The kind of **request payload** which will be sent<br>
   These are the parameters to the API function described by the endpoint
1. The type of the expected **response body**<br>
   Where an API function returns a response, this is the .NET type which will be used to represent that response

Where _None_ is listed in either column this means _"not applicable"_.
For example, an API function which uses no parameters will have no request payload.
In the case of responses, _None_ might mean that the response body will be ignored.

## The second two columns

The second two columns indicate the Endpoint type and Request action type which are recommended in this scenario.
