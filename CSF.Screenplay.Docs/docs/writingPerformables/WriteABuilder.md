# Write builders for your performables

If you write a new performable class, it is strongly recommended to write [a static builder] for it.
Benefits include:

* Ensuring that the performable may only be created in a valid state
* Making your performance logic more human-readable, almost like a [domain specific language]

[a static builder]: ../builderPattern/index.md
[domain specific language]: https://en.wikipedia.org/wiki/Domain-specific_language
