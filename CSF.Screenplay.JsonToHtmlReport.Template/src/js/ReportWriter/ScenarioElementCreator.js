import { getReportableElementCreator } from './ReportableElementCreator';
import { setContentOrRemove } from './setContentOrRemove';
import { getElementById } from '../getElementById';
import { getNameHtml } from './getNameHtml';

export class ScenarioElementCreator {
    constructor(scenarioTemplate, reportableElementCreator) {
        this.scenarioTemplate = scenarioTemplate;
        this.reportableElementCreator = reportableElementCreator;
    }

    createScenarioElement(scenario, litebox) {
        const scenarioElement = this.scenarioTemplate.content.cloneNode(true);

        if(scenario.performance.Outcome == 'Failed')
            scenarioElement.firstElementChild.classList.add('Failed');

        scenarioElement.querySelector('.scenarioName').replaceChildren(getNameHtml(scenario.scenario));
        scenarioElement.querySelector('.scenarioName').addEventListener('click', ev => ev.currentTarget.parentElement.classList.toggle('collapsed'));

        setContentOrRemove(scenarioElement, scenario.scenario, '.scenarioIdentifier', s => !s.IsGeneratedId && s.Name != s.Id, s => s.Id);

        const reportablesElement = scenarioElement.querySelector('.reportableList');
        for (const reportable of scenario.performance.Reportables) {
            const reportableElement = this.reportableElementCreator.createReportableElement(reportable, litebox);
            reportablesElement.appendChild(reportableElement);
        }

        return scenarioElement;
    }
}

export function getScenarioElementCreator() {
    return new ScenarioElementCreator(getElementById('scenarioTemplate'), getReportableElementCreator());
}