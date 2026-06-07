import { expect, test } from 'vitest';
import { SummaryDataGenerator } from './SummaryDataGenerator';
import { ScenariosByFeature } from './Models';

test('getSummaryData should return the correct feature summary data', () => {
    const sut = new SummaryDataGenerator();
    const result = sut.getSummaryData(scenariosByFeature);
    expect(result.features).toEqual({ successCount: 2, failCount: 1, totalCount: 3 });
});


test('getSummaryData should return the correct scenario summary data', () => {

    const sut = new SummaryDataGenerator();
    const result = sut.getSummaryData(scenariosByFeature);
    expect(result.scenarios).toEqual({ successCount: 5, failCount: 2, totalCount: 8 });
});

const scenariosByFeature : ScenariosByFeature = {
    features: {
        'Feature 1': {
            scenarios: [
                {
                    scenario: { Id: 'S1', Name: 'Scenario 1', IsGeneratedId: false },
                    performance: { Outcome: 'Success', NamingHierarchy: [], Reportables: [], Started: '' }
                },
                {
                    scenario: { Id: 'S1', Name: 'Scenario 1', IsGeneratedId: false },
                    performance: { Outcome: 'Success', NamingHierarchy: [], Reportables: [], Started: '' }
                },
                {
                    scenario: { Id: 'S1', Name: 'Scenario 1', IsGeneratedId: false },
                    performance: { Outcome: 'Completed', NamingHierarchy: [], Reportables: [], Started: '' }
                },
            ],
            feature: { Id: 'F1', Name: 'Feature 1', IsGeneratedId: false }
        },
        'Feature 2': {
            scenarios: [
                {
                    scenario: { Id: 'S1', Name: 'Scenario 1', IsGeneratedId: false },
                    performance: { Outcome: 'Failed', NamingHierarchy: [], Reportables: [], Started: '' }
                },
                {
                    scenario: { Id: 'S1', Name: 'Scenario 1', IsGeneratedId: false },
                    performance: { Outcome: 'Success', NamingHierarchy: [], Reportables: [], Started: '' }
                }
            ],
            feature: { Id: 'F1', Name: 'Feature 1', IsGeneratedId: false }
        },
        'Feature 3': {
            scenarios: [
                {
                    scenario: { Id: 'S1', Name: 'Scenario 1', IsGeneratedId: false },
                    performance: { Outcome: 'Success', NamingHierarchy: [], Reportables: [], Started: '' }
                }
            ],
            feature: { Id: 'F1', Name: 'Feature 1', IsGeneratedId: false }
        }
    },
    noFeatureScenarios: {
        scenarios: [
            {
                scenario: { Id: 'S1', Name: 'Scenario 1', IsGeneratedId: false },
                performance: { Outcome: 'Success', NamingHierarchy: [], Reportables: [], Started: '' }
            },
            {
                scenario: { Id: 'S1', Name: 'Scenario 1', IsGeneratedId: false },
                performance: { Outcome: 'Failed', NamingHierarchy: [], Reportables: [], Started: '' }
            }
        ]
    }
};