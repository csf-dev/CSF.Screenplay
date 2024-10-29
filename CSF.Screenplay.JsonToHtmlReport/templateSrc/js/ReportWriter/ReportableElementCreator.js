import { setContentOrRemove } from './setContentOrRemove';

export class ReportableElementCreator {
    constructor(templateElement, assetTemplateElement) {
        this.templateElement = templateElement;
        this.assetTemplate = assetTemplateElement;
    }

    createReportableElement(reportable) {
        const reportableElement = this.templateElement.content.cloneNode(true);

        this.#setupReportableType(reportableElement, reportable);
        setContentOrRemove(reportableElement, reportable, '.phase', r => r.PerformancePhase, r => r.PerformancePhase);
        setContentOrRemove(reportableElement, reportable, '.report', r => r.Report, r => r.Report);
        setContentOrRemove(reportableElement, reportable, '.exception', r => r.Exception && !r.ExceptionIsFromConsumedPerformable, r => r.Exception);
        this.#setupResult(reportableElement, reportable);
        this.#setupPerformableType(reportableElement, reportable);
        this.#setupAssets(reportableElement, reportable);
        this.#setupContainedReportables(reportableElement, reportable);

        return reportableElement;
    }

    #setupReportableType(reportableElement, reportable) {
        const typeElement = reportableElement.querySelector('.type');
        typeElement.classList.add(reportable.Type);
        switch (reportable.Type) {
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

        reportableElement.querySelector('.type i').textContent = reportable.Type;
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
        if (reportable.PerformableType) performableTypeElement.querySelector('i').textContent = reportable.PerformableType;
        else performableTypeElement.remove
    }

    #setupAssets(reportableElement, reportable) {
        const assetsRootElement = reportableElement.querySelector('.assets');
        if (!reportable.Assets || !reportable.Assets.length) {
            assetsRootElement.remove();
        }
        else {
            const assetsElement = assetsRootElement.querySelector('ul');

            for (const asset of reportable.Assets) {
                const assetElement = this.assetTemplate.cloneNode(true);
                const assetLinkElement = assetElement.querySelector('a');
                assetLinkElement.textContent = asset.FileSummary;
                assetLinkElement.href = asset.FilePath;
                assetsElement.appendChild(assetElement);
            }
        }
    }

    #setupContainedReportables(reportableElement, reportable) {
        const containedReportablesElement = reportableElement.querySelector('.reportableList');
        if (!reportable.Reportables || !reportable.Reportables.length) {
            reportableElement.firstElementChild.classList.remove('collapsed');
            reportableElement.firstElementChild.classList.add('non-collapsible');
            return;
        }

        const reportElement = reportableElement.querySelector('.report');
        reportElement.setAttribute('title', 'Performable report; click to expand/collapse');
        reportElement.addEventListener('click', ev => ev.currentTarget.parentElement.classList.toggle('collapsed'));

        for (const containedReportable of reportable.Reportables) {
            const reportableElement = this.createReportableElement(containedReportable);
            containedReportablesElement.appendChild(reportableElement);
        }
    }
}

export function getReportableElementCreator() {
    return new ReportableElementCreator(document.getElementById('reportableTemplate'),
                                        document.getElementById('assetTemplate'));                                      
}