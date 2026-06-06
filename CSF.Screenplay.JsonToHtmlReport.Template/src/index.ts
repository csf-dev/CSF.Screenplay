import "./css/index";
import { getReportLoader } from "./js/ReportLoader";
import { activatePage } from "./js/activatePage";
import { updateReportTime } from "./js/updateReportTime";
import { getScenarioAggregator } from "./js/ScenarioAggregator";
import { getReportWriter } from "./js/ReportWriter";
import { Litebox } from "./js/Litebox";

document.onreadystatechange = () => {
    if (document.readyState !== "complete") return;

    const litebox = new Litebox();
    const loader = getReportLoader();
    const report = loader.loadJson();

    updateReportTime(report.Metadata.Timestamp);
    
    const aggregator = getScenarioAggregator(report.Performances);
    const scenariosByFeature = aggregator.getScenariosByFeature();
    console.debug('Raw scenario data', scenariosByFeature);
    const reportWriter = getReportWriter();
    const featureReport = reportWriter.getReport(scenariosByFeature, litebox);

    activatePage(featureReport);
}

