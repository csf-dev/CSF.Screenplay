import { setContentOrRemove } from './setContentOrRemove';
import { getElementById } from '../getElementById';
import { getAssetsWriter } from './AssetsWriter';

export class ReportableElementCreator {
    constructor(templateElement) {
        this.templateElement = templateElement;
    }

    createReportableElement(reportable, litebox) {
        const reportableElement = this.templateElement.content.cloneNode(true);

        this.#setupReportableKind(reportableElement, reportable);
        reportableElement.querySelector('.phase').textContent = reportable.Phase;
        setContentOrRemove(reportableElement, reportable, '.report', r => r.Report, r => r.Report);
        if(reportable.Phase && reportable.Report) {
            reportableElement.querySelector('.report').classList.add('hasPhase');
            reportableElement.querySelector('.report').classList.add(`phase_${reportable.Phase}`);
        }
        setContentOrRemove(reportableElement, reportable, '.exception', r => r.Exception && !r.ExceptionIsBubbling, r => r.Exception);
        this.#setupResult(reportableElement, reportable);
        this.#setupPerformableType(reportableElement, reportable);
        this.#setupAssets(reportableElement, reportable, litebox);
        this.#setupContainedReportables(reportableElement, reportable, litebox);

        return reportableElement;
    }

    #setupReportableKind(reportableElement, reportable) {
        const infoElement = reportableElement.querySelector('.reportableInfo');
        infoElement.classList.add(reportable.Kind);
        reportableElement.querySelector('.kind').textContent = this.#getHumanReadableKind(reportable.Kind);
    }

    #getHumanReadableKind(kind) {
        switch (kind) {
        case 'ActorCreatedReport':
            return 'Actor created';
        case 'ActorGainedAbilityReport':
            return 'Actor gained ability';
        case 'ActorSpotlitReport':
            return 'Actor put into the spotlight';
        case 'SpotlightTurnedOffReport':
            return 'Spotlight turned off';
        case 'PerformableReport':
            return 'Actor executed a performable';
        default:
            return kind;
        }
    }

    #setupResult(reportableElement, reportable) {
        const resultElement = reportableElement.querySelector('.result');
        if (reportable.HasResult) resultElement.textContent = reportable.Result !== null
            ? reportable.Result
            : '<null>';
        else resultElement.remove();
    }

    #setupPerformableType(reportableElement, reportable) {
        const performableTypeElement = reportableElement.querySelector('.dotnetType');
        if (reportable.Type) performableTypeElement.textContent = reportable.Type;
        else {
            performableTypeElement.textContent = 'Not applicable';
            performableTypeElement.classList.add('notApplicable');
        }
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