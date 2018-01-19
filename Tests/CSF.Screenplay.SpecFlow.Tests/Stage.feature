Feature: The stage
  In order to be able to use the passive voice within test
  bindings, I want to be able to refer to actors by names such
  as 'he' or 'she'.  This requires a contextual storage of
  an actor.
  
Scenario: Store an actor in the stage
  Given Joe is an actor in the spotlight
  When I get the actor in the spotlight
  Then that actor should be the same as Joe
