/**
 * @jest-environment node
*/

import { ScenarioAggregator } from './ScenarioAggregator';

test('getScenariosByFeature should return an object which aggregates two scenarios into a single feature', () => {
    const performances = [
        { NamingHierarchy: [{ Id: "feature-1", Name: "Feature 1", IsGeneratedId: false }, { Id: "scenario-1", Name: "Scenario 1", IsGeneratedId: false }] },
        { NamingHierarchy: [{ Id: "feature-1", Name: "Feature 1", IsGeneratedId: false }, { Id: "scenario-2", Name: "Scenario 2", IsGeneratedId: false }] }
    ];
    const sut = new ScenarioAggregator(performances);
    const result = sut.getScenariosByFeature();
    expect(result).toEqual({
        features: {
            "feature-1": {
                feature: {
                    Id: "feature-1",
                    Name: "Feature 1",
                    IsGeneratedId: false
                },
                scenarios: [
                    {
                        scenario: {
                            Id: "scenario-1",
                            Name: "Scenario 1",
                            IsGeneratedId: false
                        },
                        performance: performances[0]
                    },
                    {
                        scenario: {
                            Id: "scenario-2",
                            Name: "Scenario 2",
                            IsGeneratedId: false
                        },
                        performance: performances[1]
                    }
                ]
            }
        },
        noFeatureScenarios: { scenarios: [] }
    });
});

test('getScenariosByFeature should return an object which correctly aggregates scenarios which belong to different features', () => {
    const performances = [
        { NamingHierarchy: [{ Id: "feature-1", Name: "Feature 1", IsGeneratedId: false }, { Id: "scenario-1", Name: "Scenario 1", IsGeneratedId: false }] },
        { NamingHierarchy: [{ Id: "feature-2", Name: "Feature 2", IsGeneratedId: false }, { Id: "scenario-2", Name: "Scenario 2", IsGeneratedId: false }] }
    ];
    const sut = new ScenarioAggregator(performances);
    const result = sut.getScenariosByFeature();
    expect(result).toEqual({
        features: {
            "feature-1": {
                feature: {
                    Id: "feature-1",
                    Name: "Feature 1",
                    IsGeneratedId: false
                },
                scenarios: [
                    {
                        scenario: {
                            Id: "scenario-1",
                            Name: "Scenario 1",
                            IsGeneratedId: false
                        },
                        performance: performances[0]
                    }
                ]
            },
            "feature-2": {
                feature: {
                    Id: "feature-2",
                    Name: "Feature 2",
                    IsGeneratedId: false
                },
                scenarios: [
                    {
                        scenario: {
                            Id: "scenario-2",
                            Name: "Scenario 2",
                            IsGeneratedId: false
                        },
                        performance: performances[1]
                    }
                ]
            }
        },
        noFeatureScenarios: { scenarios: [] }
    });
});

test('getScenariosByFeature should place a no-feature scenario into the no-feature container', () => {
    const performances = [
        { NamingHierarchy: [{ Id: "scenario-1", Name: "Scenario 1", IsGeneratedId: false }] }
    ];
    const sut = new ScenarioAggregator(performances);
    const result = sut.getScenariosByFeature();
    expect(result).toEqual({
        features: {},
        noFeatureScenarios: {
            scenarios: [
                {
                    scenario: {
                        Id: "scenario-1",
                        Name: "Scenario 1",
                        IsGeneratedId: false
                    },
                    performance: performances[0]
                }
            ]
        }
    });
});

test('getScenariosByFeature should invent a name and Id for a scenario with no name', () => {
    const performances = [
        { NamingHierarchy: [] }
    ];
    const sut = new ScenarioAggregator(performances);
    const result = sut.getScenariosByFeature();
    expect(result).toEqual({
        features: {},
        noFeatureScenarios: {
            scenarios: [
                {
                    scenario: {
                        Id: expect.any(String),
                        Name: "No Scenario",
                        IsGeneratedId: true
                    },
                    performance: performances[0]
                }
            ]
        }
    });
});

test('getScenariosByFeature should choose the correct feature and scenario from the naming hierarchy when using NUnit', () => {
    // The naming hierarchy below was taken from an actual test run using NUnit3 and Screenplay
    const performances = [
        {
            NamingHierarchy: [
                { Id: "CSF.Screenplay.Selenium.TestWebappSetupAndTeardown", Name: null, IsGeneratedId: false },
                { Id: "CSF.Screenplay.Selenium.Actions.ClearCookiesTests", Name: null, IsGeneratedId: false },
                { Id: "CSF.Screenplay.Selenium.Actions.ClearCookiesTests.ClearCookiesShouldClearCookies", Name: null, IsGeneratedId: false }
            ]
        }
    ];
    const sut = new ScenarioAggregator(performances);
    const result = sut.getScenariosByFeature();
    expect(result).toEqual({
        features: {
            "CSF.Screenplay.Selenium.Actions.ClearCookiesTests": {
                feature: {
                    Id: "CSF.Screenplay.Selenium.Actions.ClearCookiesTests",
                    Name: null,
                    IsGeneratedId: false
                },
                scenarios: [
                    {
                        scenario: {
                            Id: "CSF.Screenplay.Selenium.Actions.ClearCookiesTests.ClearCookiesShouldClearCookies",
                            Name: null,
                            IsGeneratedId: false
                        },
                        performance: performances[0]
                    }
                ]
            }
        },
        noFeatureScenarios: { scenarios: [] }
    });
});
    
