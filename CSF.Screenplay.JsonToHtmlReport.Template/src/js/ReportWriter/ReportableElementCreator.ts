import { setContentOrRemove } from '../Utils/setContentOrRemove';
import { getElementById, querySelectorNotNull } from '../Utils';
import { getAssetsWriter } from './AssetsWriter';
import { DisplaysImage } from '../Litebox';
import { PerformableReport, ReportableKind, ReportableModel } from '../Models';

export class ReportableElementCreator {
    constructor(readonly templateElement : HTMLTemplateElement) {}

    createReportableElement(reportable : ReportableModel, litebox : DisplaysImage) {
        const reportableElement = this.templateElement.content.cloneNode(true) as HTMLElement;
        setContentOrRemove(reportableElement, reportable, '.report', r => !!r.Report, r => r.Report);

        if(!this.#setupReportableKind(reportableElement, reportable)) {
            this.#removeNonPerformableElements(reportableElement);
            return reportableElement;
        }

        querySelectorNotNull('.phase', reportableElement).textContent = reportable.Phase;
        if(reportable.Phase && reportable.Report) {
            querySelectorNotNull('.report', reportableElement).classList.add('hasPhase');
            querySelectorNotNull('.report', reportableElement).classList.add(`phase_${reportable.Phase}`);
        }
        setContentOrRemove(reportableElement, reportable, '.exception', r => !!r.Exception && !r.ExceptionIsBubbling, r => r.Exception);
        this.#setupResult(reportableElement, reportable);
        this.#setupPerformableType(reportableElement, reportable);
        this.#setupAssets(reportableElement, reportable, litebox);
        this.#setupContainedReportables(reportableElement, reportable, litebox);

        return reportableElement;
    }

    #setupReportableKind(reportableElement : HTMLElement, reportable : ReportableModel) : reportable is PerformableReport {
        const infoElement = querySelectorNotNull('.reportableInfo', reportableElement);
        infoElement.classList.add(reportable.Kind);
        querySelectorNotNull('.kind', reportableElement).textContent = this.#getHumanReadableKind(reportable.Kind);
        return reportable.Kind == 'PerformableReport';
    }

    #getHumanReadableKind(kind : ReportableKind) {
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

    #setupResult(reportableElement : HTMLElement, reportable : PerformableReport) {
        const resultElement = querySelectorNotNull('.result', reportableElement);
        if (reportable.HasResult) resultElement.textContent = reportable.Result ?? '<null>';
        else resultElement.remove();
    }

    #setupPerformableType(reportableElement : HTMLElement, reportable : PerformableReport) {
        const performableTypeElement = querySelectorNotNull('.dotnetType', reportableElement);
        if (reportable.Type) performableTypeElement.textContent = reportable.Type;
        else {
            performableTypeElement.textContent = 'Not applicable';
            performableTypeElement.classList.add('notApplicable');
        }
    }

    #setupAssets(reportableElement : HTMLElement, reportable : PerformableReport, litebox : DisplaysImage) {
        const assetsWriter = getAssetsWriter(reportableElement, litebox);
        assetsWriter.writeAssets(reportable);
    }

    #setupContainedReportables(reportableElement : HTMLElement, reportable : PerformableReport, litebox : DisplaysImage) {
        const containedReportablesElement = querySelectorNotNull('.reportableList', reportableElement);
        if (!reportable.Reportables?.length) {
            reportableElement.firstElementChild!.classList.remove('collapsed');
            reportableElement.firstElementChild!.classList.add('non-collapsible');
            return;
        }

        const reportElement = reportableElement.querySelector('.report');
        if(reportElement) {
            reportElement.setAttribute('title', 'Performable report; click to expand/collapse');
            reportElement.addEventListener('click', ev => (ev.currentTarget as HTMLElement).parentElement!.classList.toggle('collapsed'));
        }


        for (const containedReportable of reportable.Reportables) {
            const reportableElement = this.createReportableElement(containedReportable, litebox);
            containedReportablesElement.appendChild(reportableElement);
        }
    }

    #removeNonPerformableElements(reportableElement : HTMLElement) {
        querySelectorNotNull('.exception', reportableElement).remove();
        querySelectorNotNull('.result', reportableElement).remove();
        const performableTypeElement = querySelectorNotNull('.dotnetType', reportableElement);
        performableTypeElement.textContent = 'Not applicable';
        performableTypeElement.classList.add('notApplicable');
        querySelectorNotNull('.assets', reportableElement).remove();
        reportableElement.firstElementChild!.classList.remove('collapsed');
        reportableElement.firstElementChild!.classList.add('non-collapsible');
    }
}

export function getReportableElementCreator() {
    return new ReportableElementCreator(getElementById('reportableTemplate'));                                      
}