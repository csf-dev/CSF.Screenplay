import { getFeatureElementCreator } from './FeatureElementCreator';

export class ReportWriter {
    constructor(featureElementCreator) {
        this.featureElementCreator = featureElementCreator;
    }

    getReport(scenariosByFeature, litebox) {
        const report = document.createDocumentFragment();
        for (const feature in scenariosByFeature.features) {
            const featureElement = this.featureElementCreator.createFeatureElement(scenariosByFeature.features[feature], litebox);
            report.appendChild(featureElement);
        }
        if (scenariosByFeature.noFeatureScenarios.scenarios.length) {
            const featureElement = this.featureElementCreator.createFeatureElement(scenariosByFeature.noFeatureScenarios, litebox);
            report.appendChild(featureElement);
        }
        return report;
    }

}

export function getReportWriter() {
    return new ReportWriter(getFeatureElementCreator());
}