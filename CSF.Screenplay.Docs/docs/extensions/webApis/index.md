---
uid: WebApisArticle
---

# CSF.Screenplay.WebApis Extension

The Web APIs Extension allows [Actors] to communicate with HTTP web API endpoints within a Screenplay [Performance].

[Actors]: xref:CSF.Screenplay.Actor
[Performance]: xref:CSF.Screenplay.IPerformance

## Overview

The fundamentals of this Screenplay extension are shown in the diagram below.
The concepts of the [Actor] and [Ability] (the ability is named [`MakeWebApiRequests`]) are explained in Screenplay's core documentation.
Other concepts are explained below.

This extension provides **[Actions]** which allow the Actor to build and send [HTTP requests] based upon [Endpoint] definitions.
These requests are sent via the HTTP client which is exposed by the [`MakeWebApiRequests`] Ability, to a live API server.
The server returns an [HTTP Response], which the extension formats into a result object.

```mermaid
flowchart 
  Ability -- "Has ability" --> Actor
  Actor -- "Makes requests" --> Request
  Endpoint -- "Requests target" --> Request
  Request --> api["Live API"]
  Ability -- "Facilitates request" --> api
  api --> Response

  style api fill:#ee9,stroke:#bb6
```

Note that the **Live API** in this diagram _is not a part of Screenplay or this extension_.
The Live API represents an actual  HTTP(S) web server which hosts the API with which Screenplay is communicating.

[Actor]: xref:CSF.Screenplay.Actor
[Ability]: xref:AbilityGlossaryItem
[`MakeWebApiRequests`]: xref:CSF.Screenplay.WebApis.MakeWebApiRequests
[Actions]: ../../../glossary/Action.md
[HTTP requests]: Requests.md
[Endpoint]: Endpoints.md
[HTTP Response]: Responses.md
