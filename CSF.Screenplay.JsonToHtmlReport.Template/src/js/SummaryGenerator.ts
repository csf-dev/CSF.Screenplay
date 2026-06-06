import { ScenariosByFeature, SummaryData } from "./Models";
import { GetsSummaryData, getSummaryDataGenerator } from "./SummaryDataGenerator";
import { getElementByIdNotNull, querySelectorNotNull } from './Utils';

const templateId = 'summaryTableTemplate';

export interface GeneratesSummary {
    generateSummary() : HTMLElement
}

export class SummaryGenerator {
    constructor(readonly scenariosByFeature : ScenariosByFeature, readonly summaryDataGenerator : GetsSummaryData) {}

    generateSummary() {
        const template = getElementByIdNotNull<HTMLTemplateElement>(templateId);

        const summary = this.summaryDataGenerator.getSummaryData(this.scenariosByFeature);
        return this.#createSummaryTable(summary, template);
    }

    #createSummaryTable(summary : SummaryData, template : HTMLTemplateElement) {
        const table = template.content.cloneNode(true) as ParentNode;

        querySelectorNotNull('#featureSuccess', table).textContent = summary.features.successCount.toString();
        querySelectorNotNull('#featureFailure', table).textContent = summary.features.failCount.toString();
        querySelectorNotNull('#featureTotal', table).textContent = summary.features.totalCount.toString();

        querySelectorNotNull('#scenarioSuccess', table).textContent = summary.scenarios.successCount.toString();
        querySelectorNotNull('#scenarioFailure', table).textContent = summary.scenarios.failCount.toString();
        querySelectorNotNull('#scenarioTotal', table).textContent = summary.scenarios.totalCount.toString();

        return table;
    }
}

export function getSummaryGenerator(scenariosByFeature : ScenariosByFeature) {
    return new SummaryGenerator(scenariosByFeature, getSummaryDataGenerator());
}