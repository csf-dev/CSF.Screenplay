export class ScenarioAggregator {
    constructor(performances) {
        this.performances = performances;
    }

    getScenariosByFeature() {
        return this.performances.reduce((accumulator, performance) => {
            const names = performance.NamingHierarchy;
            const feature = this.#getFeature(names);
            const scenario = this.#getScenario(names);
            const scenarioContainer = { scenario, performance };            
            const featureContainer = this.#getFeatureContainer(accumulator, feature);
            
            featureContainer.scenarios.push(scenarioContainer);
            return accumulator;
        }, { features: {}, noFeatureScenarios: { scenarios: [] } });
    }

    #getFeature(names) {
        return names.length > 1
        ? names[0]
        : { Identifier: null, Name: "No Feature", WasIdentifierAutoGenerated: true };
    }

    #getScenario(names) {
        if (names.length > 1) return names[1];
        if (names.length > 0) return names[0];
        return { Identifier: crypto.randomUUID(), Name: "No Scenario", WasIdentifierAutoGenerated: true };
    }

    #getFeatureContainer(accumulator, feature) {
        if (feature.Identifier === null) return accumulator.noFeatureScenarios;
        if (Object.hasOwn(accumulator.features, feature.Identifier)) return accumulator.features[feature.Identifier];
        return accumulator.features[feature.Identifier] = this.#createFeatureContainer(feature);
    }

    #createFeatureContainer(feature) {
        return { feature: { Identifier: feature.Identifier, Name: feature.Name, WasIdentifierAutoGenerated: feature.WasIdentifierAutoGenerated }, scenarios: [] };
    }
}

export function getScenarioAggregator(performances) {
    return new ScenarioAggregator(performances);
}