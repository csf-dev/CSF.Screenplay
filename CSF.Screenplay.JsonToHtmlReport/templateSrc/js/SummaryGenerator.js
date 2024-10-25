import { getSummaryDataGenerator } from "./SummaryDataGenerator";

const templateId = 'summaryTableTemplate';

export class SummaryGenerator {
    constructor(scenariosByFeature, summaryDataGenerator) {
        this.scenariosByFeature = scenariosByFeature;
        this.summaryDataGenerator = summaryDataGenerator;
    }

    generateSummary() {
        const template = document.getElementById(templateId);
        if (!template) {
            throw new Error(`Element with id ${templateId} not found`);
        }

        const summary = this.summaryDataGenerator.getSummaryData(this.scenariosByFeature);
        return this.#createSummaryTable(summary, template);
    }

    #createSummaryTable(summary, template) {
        const table = template.content.cloneNode(true);

        table.querySelector('#featureSuccess').textContent = summary.features.successCount;
        table.querySelector('#featureFailure').textContent = summary.features.failCount;
        table.querySelector('#featureTotal').textContent = summary.features.totalCount;

        table.querySelector('#scenarioSuccess').textContent = summary.scenarios.successCount;
        table.querySelector('#scenarioFailure').textContent = summary.scenarios.failCount;
        table.querySelector('#scenarioTotal').textContent = summary.scenarios.totalCount;

        return table;
    }
}

export function getSummaryGenerator(scenariosByFeature) {
    return new SummaryGenerator(scenariosByFeature, getSummaryDataGenerator());
}