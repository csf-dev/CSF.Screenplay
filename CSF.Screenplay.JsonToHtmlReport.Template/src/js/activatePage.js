import { getElementById } from "./getElementById";

const summaryElementId = 'summary', featuresElementId = 'featureList';

export const activatePage = (summaryTable, featureList) => {
    const summaryElement = getElementById(summaryElementId);
    summaryElement.appendChild(summaryTable);
    
    const featuresElement = getElementById(featuresElementId);
    featuresElement.appendChild(featureList);

    hideSpinner();
    showFeatures();
};

const hideSpinner = () => getElementById("pageMask").classList.add("hidden");
const showFeatures = () => getElementById("features").classList.remove("hidden");
