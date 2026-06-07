import { FeatureContainer, HasScenarios, ScenariosByFeature, PerformanceReport, IdentifierAndNameModel } from "./Models";

export interface GetsScenariosByFeature {
    getScenariosByFeature() : ScenariosByFeature,
}

export class ScenarioAggregator implements GetsScenariosByFeature {
    performances : PerformanceReport[]

    constructor(performances : PerformanceReport[]) {
        this.performances = performances;
    }

    getScenariosByFeature() : ScenariosByFeature {
        return this.performances.reduce((accumulator : ScenariosByFeature, performance) => {
            const names = performance.NamingHierarchy;
            const feature = this.#getFeature(names);
            const scenario = this.#getScenario(names);
            const scenarioContainer = { scenario, performance };            
            const featureContainer = this.#getFeatureContainer(accumulator, feature);
            
            featureContainer.scenarios.push(scenarioContainer);
            return accumulator;
        }, { features: {}, noFeatureScenarios: { scenarios: [] } });
    }

    #getFeature(names : IdentifierAndNameModel[]) : IdentifierAndNameModel {
        return names.length > 1
            ? names.at(names.length - 2)!
            : { Id: null, Name: "No Feature", IsGeneratedId: true };
    }

    #getScenario(names : IdentifierAndNameModel[]) : IdentifierAndNameModel {
        if (names.length > 0) return names.at(names.length - 1)!;
        return { Id: crypto.randomUUID(), Name: "No Scenario", IsGeneratedId: true };
    }

    #getFeatureContainer(accumulator : ScenariosByFeature, feature : IdentifierAndNameModel) : HasScenarios | FeatureContainer {
        if (feature.Id === null) return accumulator.noFeatureScenarios;
        const featureId = feature.Id;
        if (Object.hasOwn(accumulator.features, featureId)) return accumulator.features[featureId];
        accumulator.features[featureId] = this.#createFeatureContainer(feature);
        return accumulator.features[featureId];
    }

    #createFeatureContainer(feature : IdentifierAndNameModel) : FeatureContainer {
        return { feature: { Id: feature.Id, Name: feature.Name, IsGeneratedId: feature.IsGeneratedId }, scenarios: [] };
    }
}

export function getScenarioAggregator(performances : PerformanceReport[]) : GetsScenariosByFeature {
    return new ScenarioAggregator(performances);
}