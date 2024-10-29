import "./css/layers.css";
import "./css/reset.css";
import "./css/spinner.css"
import "./css/layout.css";
import "./css/content.css";
import "./css/summaryTable.css";
import "./css/scenarioList.css";
import { getReportLoader } from "./js/ReportLoader";
import { activatePage } from "./js/activatePage";
import { updateReportTime } from "./js/updateReportTime";
import { getScenarioAggregator } from "./js/ScenarioAggregator";
import { getSummaryGenerator } from "./js/SummaryGenerator";
import { getReportWriter } from "./js/ReportWriter";

document.onreadystatechange = () => {
    if (document.readyState !== "complete") return;

    const loader = getReportLoader();
    const report = loader.loadJson();

    updateReportTime(report.Metadata.Timestamp);
    
    const aggregator = getScenarioAggregator(report.Performances);
    const scenariosByFeature = aggregator.getScenariosByFeature();
    console.debug('Raw scenario data', scenariosByFeature);
    const summaryGenerator = getSummaryGenerator(scenariosByFeature);
    const summary = summaryGenerator.generateSummary();
    const reportWriter = getReportWriter();
    const featureReport = reportWriter.getReport(scenariosByFeature);

    activatePage(summary, featureReport);
}

