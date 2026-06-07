import { getReportableElementCreator, ReportableElementCreator } from './ReportableElementCreator';
import { getElementById, querySelectorNotNull, setContentOrRemove } from '../Utils';
import { getNameHtml } from './getNameHtml';
import { DisplaysImage } from '../Litebox';
import { ScenarioContainer } from '../Models';

export class ScenarioElementCreator {
    constructor(readonly scenarioTemplate : HTMLTemplateElement, readonly reportableElementCreator : ReportableElementCreator) {}

    createScenarioElement(scenario : ScenarioContainer, litebox : DisplaysImage) {
        const scenarioElement = this.scenarioTemplate.content.cloneNode(true) as HTMLElement;

        if(scenario.performance.Outcome == 'Failed')
            scenarioElement.firstElementChild!.classList.add('Failed');

        querySelectorNotNull('.scenarioName', scenarioElement).replaceChildren(getNameHtml(scenario.scenario));
        querySelectorNotNull('.scenarioName', scenarioElement).addEventListener('click', ev => (ev.currentTarget as HTMLElement).parentElement!.classList.toggle('collapsed'));

        setContentOrRemove(scenarioElement, scenario.scenario, '.scenarioIdentifier', s => !s.IsGeneratedId && s.Name != s.Id, s => s.Id || '');

        const reportablesElement = querySelectorNotNull('.reportableList', scenarioElement);
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