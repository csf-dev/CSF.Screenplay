const
    featureTemplateId = 'featureTemplate',
    scenarioTemplateId = 'scenarioTemplate',
    reportableTemplateId = 'reportableTemplate',
    assetTemplateId = 'assetTemplate';

export class ReportWriter {
    constructor() {
        this.featureTemplate = document.getElementById(featureTemplateId);
        this.scenarioTemplate = document.getElementById(scenarioTemplateId);
        this.reportableTemplate = document.getElementById(reportableTemplateId);
        this.assetTemplate = document.getElementById(assetTemplateId);
    }

    getReport(scenariosByFeature) {
        const report = document.createDocumentFragment();
        for (const feature in scenariosByFeature.features) {
            const featureElement = this.#createFeatureElement(scenariosByFeature.features[feature]);
            report.appendChild(featureElement);
        }
        if (scenariosByFeature.noFeatureScenarios.length) {
            const featureElement = this.#createFeatureElement(scenariosByFeature.noFeatureScenarios);
            report.appendChild(featureElement);
        }
        return report;
    }

    #createFeatureElement(scenarios) {
        const featureElement = this.featureTemplate.content.cloneNode(true);
        featureElement.querySelector('.featureName').textContent = scenarios[0].performance.NamingHierarchy[0]
            ? scenarios[0].performance.NamingHierarchy[0].Name
            : 'No Feature name';
        const scenariosElement = featureElement.querySelector('.scenarioList');
        for (const scenario of scenarios) {
            const scenarioElement = this.#createScenarioElement(scenario);
            scenariosElement.appendChild(scenarioElement);
        }
        return featureElement;
    }

    #createScenarioElement(scenario) {
        const scenarioElement = this.scenarioTemplate.content.cloneNode(true);
        scenarioElement.querySelector('.scenarioName').textContent = scenario.performance.NamingHierarchy[1]
            ? scenario.performance.NamingHierarchy[1].Name
            : 'No Scenario name';
        const reportablesElement = scenarioElement.querySelector('.reportableList');
        for (const reportable of scenario.performance.Reportables) {
            const reportableElement = this.#createReportableElement(reportable);
            reportablesElement.appendChild(reportableElement);
        }
        return scenarioElement;
    }

    #createReportableElement(reportable) {
        const reportableElement = this.reportableTemplate.content.cloneNode(true);

        reportableElement.querySelector('.type').classList.add(reportable.Type);
        reportableElement.querySelector('.type i').textContent = reportable.Type;

        const phaseElement = reportableElement.querySelector('.phase');
        if (reportable.PerformancePhase) phaseElement.textContent = reportable.PerformancePhase;
        else phaseElement.remove();

        const reportElement = reportableElement.querySelector('.report');
        if (reportable.Report) reportElement.textContent = reportable.Report;
        else reportElement.remove();

        const resultElement = reportableElement.querySelector('.result');
        if (reportable.HasResult) resultElement.textContent = reportable.Result !== null
            ? reportable.Result
            : '<null>';
        else resultElement.remove();

        const exceptionElement = reportableElement.querySelector('.exception');
        if (reportable.Exception && !reportable.ExceptionIsFromConsumedPerformable) exceptionElement.textContent = reportable.Exception;
        else exceptionElement.remove();

        const performableTypeElement = reportableElement.querySelector('.performableType');
        if (reportable.PerformableType) performableTypeElement.textContent = reportable.PerformableType;
        else performableTypeElement.remove();

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

        const containedReportablesElement = reportableElement.querySelector('.reportableList');
        if (!reportable.Reportables)
            return reportableElement;

        for (const containedReportable of reportable.Reportables) {
            const reportableElement = this.#createReportableElement(containedReportable);
            containedReportablesElement.appendChild(reportableElement);
        }

        return reportableElement;
    }
}

export function getReportWriter() {
    return new ReportWriter();
}