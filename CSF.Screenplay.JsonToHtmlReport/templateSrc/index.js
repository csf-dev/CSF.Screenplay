import "./css/layers.css";
import "./css/reset.css";
import "./css/spinner.css"
import "./css/layout.css";
import "./css/content.css";
import { ReportLoader } from "./js/ReportLoader.js";
import { activatePage } from "./js/activatePage.js";
import { updateReportTime } from "./js/updateReportTime.js";
import { ScenarioAggregator } from "./js/ScenarioAggregator.js";

document.onreadystatechange = () => {
    if (document.readyState !== "complete") return;

    const loader = new ReportLoader('reportSrc');
    const report = loader.loadJson();

    updateReportTime(report.Metadata.Timestamp);
    const aggregator = new ScenarioAggregator(report.Performances);
    const scenariosByFeature = aggregator.getScenariosByFeature();
    console.log(scenariosByFeature);

    activatePage();
}

