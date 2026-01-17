# Tests

This directory contains .NET test projects; nothing here is packed or forms part of the CSF.Screenplay framework.

Of note, the **CSF.Screenplay.Selenium.TestWebapp** project is a small website without any test code of its own.
It is used alongside **CSF.Screenplay.Selenium.Tests**.
The Selenium-based tests are written to interact with this small app, the behaviour of which is already known and verified.
This way, the Selenium tests may verify that Screenplay/Selenium are interacting with web applications in the correct/expected manner.
