# Endpoints

**Endpoints** are fundamental to this extension; they are .NET class instances which describe a piece of API functionality which may be consumed.
The single most important (and obvious) piece of information stored in an endpoint object is the URL (route) which is used to communicate with the API function.

## Endpoint classes

The WebAPIs Screenplay extension provides a number of classes by which to describe endpoints.
Developers using this extension are encouraged to create 'libraries' of Endpoints within their own logic.
Such a library might be as simple as a `static class` which contains `public static` get-only properties, each of which returns a named endpoint.
Every endpoint class derives from a base class named [`EndpointBase`].

The endpoint classes available are listed below.
There is also [an article indicating how to choose an appropriate combination] of Endpoint and [Request action].

* [`Endpoint`]
* [`Endpoint<TResponse>`]
* [`ParameterizedEndpoint<TParameters>`] - _intended to be used as a base class_
* [`ParameterizedEndpoint<TParameters,TResponse>`] - _intended to be used as a base class_
* [`JsonEndpoint<TParameters>`]
* [`JsonEndpoint<TParameters,TResponse>`]

[`EndpointBase`]: xref:CSF.Screenplay.WebApis.EndpointBase
[`Endpoint`]: xref:CSF.Screenplay.WebApis.Endpoint
[`Endpoint<TResponse>`]: xref:CSF.Screenplay.WebApis.Endpoint`1
[`ParameterizedEndpoint<TParameters>`]: xref:CSF.Screenplay.WebApis.ParameterizedEndpoint`1
[`ParameterizedEndpoint<TParameters,TResponse>`]: xref:CSF.Screenplay.WebApis.ParameterizedEndpoint`2
[`JsonEndpoint<TParameters>`]: xref:CSF.Screenplay.WebApis.JsonEndpoint`1
[`JsonEndpoint<TParameters,TResponse>`]: xref:CSF.Screenplay.WebApis.JsonEndpoint`2
[an article indicating how to choose an appropriate combination]: ChoosingEndpointsAndRequests.md
[Request action]: Requests.md

## Common to all endpoints

All endpoints describe:

* [The URL pattern to reach the endpoint](xref:CSF.Screenplay.WebApis.EndpointBase.%23ctor(System.Uri,System.Net.Http.HttpMethod))
* [The HTTP method used with the endpoint](xref:CSF.Screenplay.WebApis.EndpointBase.%23ctor(System.Uri,System.Net.Http.HttpMethod))
* [A human-readable name for the endpoint](xref:CSF.Screenplay.WebApis.EndpointBase.Name)
* [An optional timeout for the endpoint](xref:CSF.Screenplay.WebApis.EndpointBase.Timeout)
