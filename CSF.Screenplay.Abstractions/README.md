# CSF.Screenplay.Abstractions

**CSF.Screenplay.Abstractions** provides the interfaces and abstractions for much of [CSF.Screenplay]'s API.
It does not contain the logic and associated dependencies for the Screenplay framework.
As such, this library is expected to receive changes less frequently than the other libraries in the CSF.Screenplay ecosystem.

Use the library for:

* Authoring new [Screenplay extensions]
* Writing [Performables] and/or [Abilities] for your own software or tests, if you wish to keep those types in an assembly which does not depend upon the full Screenplay package

[CSF.Screenplay]: https://www.nuget.org/packages/CSF.Screenplay
[Screenplay extensions]: https://csf-dev.github.io/CSF.Screenplay/glossary/Extension.html
[Performables]: https://csf-dev.github.io/CSF.Screenplay/glossary/Performable.html
[Abilities]: https://csf-dev.github.io/CSF.Screenplay/glossary/Ability.html
