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
            ? names[names.length - 2]
            : { Id: null, Name: "No Feature", IsGeneratedId: true };
    }

    #getScenario(names) {
        if (names.length > 0) return names[names.length - 1];
        return { Id: crypto.randomUUID(), Name: "No Scenario", IsGeneratedId: true };
    }

    #getFeatureContainer(accumulator, feature) {
        if (feature.Id === null) return accumulator.noFeatureScenarios;
        if (Object.hasOwn(accumulator.features, feature.Id)) return accumulator.features[feature.Id];
        accumulator.features[feature.Id] = this.#createFeatureContainer(feature);
        return accumulator.features[feature.Id];
    }

    #createFeatureContainer(feature) {
        return { feature: { Id: feature.Id, Name: feature.Name, IsGeneratedId: feature.IsGeneratedId }, scenarios: [] };
    }
}

export function getScenarioAggregator(performances) {
    return new ScenarioAggregator(performances);
}