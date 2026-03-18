import { setContentOrRemove } from './setContentOrRemove';
import { getElementById } from '../getElementById';
import { getAssetsWriter } from './AssetsWriter';

export class ReportableElementCreator {
    constructor(templateElement) {
        this.templateElement = templateElement;
    }

    createReportableElement(reportable, litebox) {
        const reportableElement = this.templateElement.content.cloneNode(true);

        this.#setupReportableType(reportableElement, reportable);
        setContentOrRemove(reportableElement, reportable, '.phase', r => r.Phase, r => r.Phase);
        setContentOrRemove(reportableElement, reportable, '.report', r => r.Report, r => r.Report);
        setContentOrRemove(reportableElement, reportable, '.exception', r => r.Exception && !r.ExceptionIsBubbling, r => r.Exception);
        this.#setupResult(reportableElement, reportable);
        this.#setupPerformableType(reportableElement, reportable);
        this.#setupAssets(reportableElement, reportable, litebox);
        this.#setupContainedReportables(reportableElement, reportable, litebox);

        return reportableElement;
    }

    #setupReportableType(reportableElement, reportable) {
        const typeElement = reportableElement.querySelector('.type');
        typeElement.classList.add(reportable.Kind);
        switch (reportable.Kind) {
        case 'ActorCreatedReport':
            typeElement.setAttribute('title', 'Actor created');
            break;
        case 'ActorGainedAbilityReport':
            typeElement.setAttribute('title', 'Actor gained ability');
            break;
        case 'ActorSpotlitReport':
            typeElement.setAttribute('title', 'Actor put into the spotlight');
            break;
        case 'SpotlightTurnedOffReport':
            typeElement.setAttribute('title', 'Spotlight turned off');
            break;
        case 'PerformableReport':
            typeElement.setAttribute('title', 'Actor executed a performable');
            break;
        }

        reportableElement.querySelector('.type i').textContent = reportable.Kind;
    }

    #setupResult(reportableElement, reportable) {
        const resultElement = reportableElement.querySelector('.result');
        if (reportable.HasResult) resultElement.textContent = reportable.Result !== null
            ? reportable.Result
            : '<null>';
        else resultElement.remove();
    }

    #setupPerformableType(reportableElement, reportable) {
        const performableTypeElement = reportableElement.querySelector('.performableType');
        if (reportable.Type) performableTypeElement.querySelector('i').textContent = reportable.Type;
        else performableTypeElement.remove()
    }

    #setupAssets(reportableElement, reportable, litebox) {
        const assetsWriter = getAssetsWriter(reportableElement, litebox);
        assetsWriter.writeAssets(reportable);
    }

    #setupContainedReportables(reportableElement, reportable, litebox) {
        const containedReportablesElement = reportableElement.querySelector('.reportableList');
        if (!reportable.Reportables?.length) {
            reportableElement.firstElementChild.classList.remove('collapsed');
            reportableElement.firstElementChild.classList.add('non-collapsible');
            return;
        }

        const reportElement = reportableElement.querySelector('.report');
        if(reportElement) {
            reportElement.setAttribute('title', 'Performable report; click to expand/collapse');
            reportElement.addEventListener('click', ev => ev.currentTarget.parentElement.classList.toggle('collapsed'));
        }


        for (const containedReportable of reportable.Reportables) {
            const reportableElement = this.createReportableElement(containedReportable, litebox);
            containedReportablesElement.appendChild(reportableElement);
        }
    }
}

export function getReportableElementCreator() {
    return new ReportableElementCreator(getElementById('reportableTemplate'));                                      
}