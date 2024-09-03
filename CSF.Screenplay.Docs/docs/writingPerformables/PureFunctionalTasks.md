# 'Pure functional' tasks

To maximise their reusability, developers are advised to write **[Tasks]** in a way which _either_:

* Changes the application's state
* Gets information from the application without changing its state

This draws from [the lessons that writing pure functions] teaches us.
If 'getting some information' has unwanted or unexpected _side-effects_ then the task becomes less reusable.
If a task _must have_ side effects then consider separating the parts of it which do not require those side-effects into a lower-level task which is reusable without the side-effects.
That lower-level task would then be consumed by a higher-level task which does include the side-effects.

[Tasks]: ../../glossary/Task.md
[the lessons that writing pure functions]: https://en.wikipedia.org/wiki/Pure_function
