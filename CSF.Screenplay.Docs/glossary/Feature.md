# Feature

A **Feature** is a concept which is relevant when the Screenplay library is being used to perform automated tests.

Each feature is a logical group of one or more related **[Scenarios]**.
In Screenplay, features exist only for organising scenarios.
They have no first-class representation in the code; their only appearance is within **[Reports]**.

Everything related to features is handled automatically when consuming Screenplay from an appropriate **[Integration]**.

Depending upon the testing framework in use, features might alternatively be named "test fixture" or "test class". 

[Scenarios]: Scenario.md
[Reports]: Report.md
[Integration]: Integration.md
