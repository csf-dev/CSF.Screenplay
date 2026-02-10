# Responses

The WebAPIs extension for Screenplay supports two kinds of responses.

## JSON

For JSON-based endpoints, use the [`SendTheHttpRequestAndGetJsonResponse<TResponse>`] [request action type] and the response from the API function will automatically be deserialized into an instance of the `TResponse` type.

[`SendTheHttpRequestAndGetJsonResponse<TResponse>`]: xref:CSF.Screenplay.WebApis.SendTheHttpRequestAndGetJsonResponse`1
[request action type]: Requests.md

## Other response types

For other endpoints with non-JSON responses, it is up to the consumer to deserialize the response using whatever logic is appropriate.
In this case, using the [`SendTheHttpRequestAndGetTheResponse<TResponse>`] request action type, the returned value from `PerformAsAsync` is an instance of [`HttpResponseMessageAndResponseType<TResponse>`].
This model object includes the raw/original [`HttpResponseMessage`] which was returned from the HTTP Client, but the class is generic for the intended type of the response.
This should provide sufficient information to deserialize the response accordingly.

[`SendTheHttpRequestAndGetTheResponse<TResponse>`]: xref:CSF.Screenplay.WebApis.SendTheHttpRequestAndGetTheResponse`1
[`HttpResponseMessageAndResponseType<TResponse>`]: xref:CSF.Screenplay.WebApis.HttpResponseMessageAndResponseType`1
[`HttpResponseMessage`]: xref:System.Net.Http.HttpResponseMessage
