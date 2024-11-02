const successOutcome = 'Success';
const failOutcome = 'Failed';

export class SummaryDataGenerator {
    getSummaryData(scenariosByFeature) {
        const features = scenariosByFeature.features;
        const noFeatureScenarios = scenariosByFeature.noFeatureScenarios;

        const featureSummary = this.#getFeatureSummary(features);
        const scenarioSummary = this.#getScenarioSummary(features, noFeatureScenarios);

        return { features: featureSummary, scenarios: scenarioSummary };
    }

    #getScenarioSummary(features, noFeatureScenarios) {
        const featureScenarios = Object.values(features).map(x => x.scenarios).flat();
        const allScenarios = [...featureScenarios, ...noFeatureScenarios.scenarios];

        return allScenarios.reduce((accumulator, scenarioContainer) => {
            const outcome = scenarioContainer.performance.Outcome;
            switch (outcome) {
                case successOutcome:
                    return { ...accumulator, successCount: accumulator.successCount + 1, totalCount: accumulator.totalCount + 1 };
                case failOutcome:
                    return { ...accumulator, failCount: accumulator.failCount + 1, totalCount: accumulator.totalCount + 1 };
                default:
                    return { ...accumulator, totalCount: accumulator.totalCount + 1 };
            }
        }, { successCount: 0, failCount: 0, totalCount: 0 });
    }

    #getFeatureSummary(features) {
        return Object.entries(features).reduce((accumulator, [_, feature]) => {
            if (!feature.scenarios.length) return accumulator;
            
            if (feature.scenarios.every(scenario => scenario.performance.Outcome === successOutcome)) {
                return { ...accumulator, successCount: accumulator.successCount + 1, totalCount: accumulator.totalCount + 1 };
            }
            else if(feature.scenarios.some(scenario => scenario.performance.Outcome === failOutcome)) {
                return { ...accumulator, failCount: accumulator.failCount + 1, totalCount: accumulator.totalCount + 1 };
            }

            return { ...accumulator, totalCount: accumulator.totalCount + 1 };
        }, { successCount: 0, failCount: 0, totalCount: 0 });
    }
}

export function getSummaryDataGenerator() {
    return new SummaryDataGenerator();
}