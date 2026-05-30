import { getElementById } from "./getElementById";

const summaryElementId = 'summary', featuresElementId = 'featureList';

export const activatePage = (featureList) => {
    // const summaryElement = getElementById(summaryElementId);
    // summaryElement.appendChild(summaryTable);
    
    const featuresElement = getElementById(featuresElementId);
    featuresElement.appendChild(featureList);

    hideSpinner();
    showFeatures();
};

const hideSpinner = () => getElementById("loadingMask").classList.add("hidden");
const showFeatures = () => getElementById("featuresSection").classList.remove("hidden");
