import "./css/layers.css";
import "./css/reset.css";
import "./css/spinner.css"
import "./css/layout.css";
import "./css/content.css";
import "./css/summaryTable.css";
import "./css/scenarioList.css";
import { getReportLoader } from "./js/ReportLoader.js";
import { activatePage } from "./js/activatePage.js";
import { updateReportTime } from "./js/updateReportTime.js";
import { getScenarioAggregator } from "./js/ScenarioAggregator.js";
import { getSummaryGenerator } from "./js/SummaryGenerator.js";
import { getReportWriter } from "./js/ReportWriter.js";

document.onreadystatechange = () => {
    if (document.readyState !== "complete") return;

    const loader = getReportLoader();
    const report = loader.loadJson();

    updateReportTime(report.Metadata.Timestamp);
    
    const aggregator = getScenarioAggregator(report.Performances);
    const scenariosByFeature = aggregator.getScenariosByFeature();
    console.debug(scenariosByFeature);
    const summaryGenerator = getSummaryGenerator(scenariosByFeature);
    const summary = summaryGenerator.generateSummary();
    const reportWriter = getReportWriter();
    const featureReport = reportWriter.getReport(scenariosByFeature);

    activatePage(summary, featureReport);
}

