import { SummaryDataGenerator } from './SummaryDataGenerator';

test('getSummaryData should return the correct feature summary data', () => {
    const scenariosByFeature = {
        features: {
            'Feature 1': {
                scenarios: [
                    { performance: { Outcome: 'Success' } },
                    { performance: { Outcome: 'Success' } },
                    { performance: { Outcome: 'Completed' } },
                ]
            },
            'Feature 2': {
                scenarios: [
                    { performance: { Outcome: 'Failed' } },
                    { performance: { Outcome: 'Success' } }
                ]
            },
            'Feature 3': {
                scenarios: [
                    { performance: { Outcome: 'Success' } }
                ]
            }
        },
        noFeatureScenarios: {
            scenarios: [
                { performance: { Outcome: 'Success' } },
                { performance: { Outcome: 'Failed' } }
            ]
        }
    };
    const sut = new SummaryDataGenerator();
    const result = sut.getSummaryData(scenariosByFeature);
    expect(result.features).toEqual({ successCount: 2, failCount: 1, totalCount: 3 });
});


test('getSummaryData should return the correct scenario summary data', () => {
    const scenariosByFeature = {
        features: {
            'Feature 1': {
                scenarios: [
                    { performance: { Outcome: 'Success' } },
                    { performance: { Outcome: 'Success' } },
                    { performance: { Outcome: 'Completed' } },
                ]
            },
            'Feature 2': {
                scenarios: [
                    { performance: { Outcome: 'Failed' } },
                    { performance: { Outcome: 'Success' } }
                ]
            },
            'Feature 3': {
                scenarios: [
                    { performance: { Outcome: 'Success' } }
                ]
            }
        },
        noFeatureScenarios: {
            scenarios: [
                { performance: { Outcome: 'Success' } },
                { performance: { Outcome: 'Failed' } }
            ]
        }
    };
    const sut = new SummaryDataGenerator();
    const result = sut.getSummaryData(scenariosByFeature);
    expect(result.scenarios).toEqual({ successCount: 5, failCount: 2, totalCount: 8 });
});