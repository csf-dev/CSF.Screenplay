import { IdentifierAndNameModel, PerformanceReport } from "./ScreenplayReport"

export interface HasScenarios {
    scenarios: ScenarioContainer[]
}

export interface FeatureContainer extends HasScenarios {
    feature: IdentifierAndNameModel,
}

export interface ScenarioContainer {
    scenario: IdentifierAndNameModel,
    performance: PerformanceReport
}

export interface ScenariosByFeature {
    features: FeatureList,
    noFeatureScenarios: HasScenarios
}

export interface FeatureList {
    [key: string]: FeatureContainer
}