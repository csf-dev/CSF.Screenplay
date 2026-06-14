# Using CSF.Screenplay with legacy SpecFlow

For now, _CSF.Screenplay supports SpecFlow_.
It is strongly recommended to [upgrade to Reqnroll at your earliest opportunity].
No promises can be made about future support, particularly as Reqnroll diverges from SpecFlow's original architecture.

## Adapting instructions for SpecFlow

Users of SpecFlow may use [the Getting Started guide for Reqnroll], with the following adaptations.

* At step 1, install SpecFlow version 3.4.3 or higher instead
* At step 2, install the package [CSF.Screenplay.SpecFlow] instead
* When using the framework, replace any usage of the `Reqnroll` namespace with `TechTalk.SpecFlow`

The remainder of the guide holds true for both Reqnroll or SpecFlow.
The same techniques & syntax mentioned in the guide are supported in both frameworks, with only the namespace difference noted above.

[upgrade to Reqnroll at your earliest opportunity]: https://docs.reqnroll.net/latest/guides/migrating-from-specflow.html
[the Getting Started guide for Reqnroll]: ./index.md
[CSF.Screenplay.SpecFlow]: https://www.nuget.org/packages/CSF.Screenplay.SpecFlow
