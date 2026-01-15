# Extension

The Screenplay library doesn't do much on its own.
The packages CSF.Screenplay.Abstractions and CSF.Screenplay offer very little in the way of [Abilities], [Actions] or [Questions].
Since these are the building blocks for writing [Tasks] and [Performances], developers won't get very far without installing one or more extensions.

[Abilities]: Ability.md
[Actions]: Action.md
[Questions]: Question.md
[Tasks]: Task.md
[Performances]: xref:CSF.Screenplay.IPerformance

## Extensions developed by the authors of CSF.Screenplay

This is a list of extensions which are developed and tested alongside Screenplay itself.

| Name                      | Summary                                                                           |
| ----                      | -------                                                                           |
| CSF.Screenplay.Selenium   | For using Screenplay to control a Selenium Web Driver, for browser automation     |
| CSF.Screenplay.WebApis    | For using Screenplay to send/receive requests/responses to/from web API endpoints |

## Developers are free to write their own extensions

Developers are encouraged to write new extensions if they would like.
See the [extending Screenplay documentation] for more information.

[extending Screenplay documentation]: ../docs/extendingScreenplay/ScreenplayExtensions.md
