import { getScenarioElementCreator, ScenarioElementCreator } from './ScenarioElementCreator';
import { getElementByIdNotNull, querySelectorNotNull } from '../Utils';
import { getNameHtml } from './getNameHtml';
import { DisplaysImage } from '../Litebox';
import { FeatureContainer, HasScenarios } from '../Models';

export class FeatureElementCreator {
    constructor(readonly featureTemplate : HTMLTemplateElement, readonly scenarioElementCreator : ScenarioElementCreator) {}

    createFeatureElement(feature : HasScenarios, litebox : DisplaysImage) {
        const featureElement = this.featureTemplate.content.cloneNode(true) as HTMLElement;
        const hasFailures = feature.scenarios.some(x => x.performance.Outcome == 'Failed');

        if(hasFailures) featureElement.firstElementChild!.classList.add('Failures');

        const
            featureName = querySelectorNotNull('.featureName', featureElement),
            featureIdentifier = querySelectorNotNull('.featureIdentifier', featureElement),
            scenarios = querySelectorNotNull('.scenarioList', featureElement);
        
        if(!this.#isFeature(feature)) {
            featureIdentifier.remove();
            return featureElement;
        }

        featureName.replaceChildren(getNameHtml(feature.feature));
        featureName.addEventListener('click', ev => (ev.currentTarget as HTMLElement).parentElement!.classList.toggle('collapsed'));

        if (!feature.feature.IsGeneratedId && feature.feature.Name != feature.feature.Id)
            featureIdentifier.textContent = feature.feature.Id;
        else
            featureIdentifier.remove();

        for (const scenario of feature.scenarios) {
            const scenarioElement = this.scenarioElementCreator.createScenarioElement(scenario, litebox);
            scenarios.appendChild(scenarioElement);
        }
        return featureElement;
    }

    #isFeature(feature : HasScenarios) : feature is FeatureContainer {
        return Object.hasOwn(feature, 'feature');
    }
}

export function getFeatureElementCreator() {
    return new FeatureElementCreator(getElementByIdNotNull('featureTemplate'), getScenarioElementCreator());
}