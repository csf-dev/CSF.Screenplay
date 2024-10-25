const summaryElementId = 'summary', featuresElementId = 'featureList';

export const activatePage = (summaryTable, featureList) => {
    const summaryElement = document.getElementById(summaryElementId);
    summaryElement.appendChild(summaryTable);
    
    const featuresElement = document.getElementById(featuresElementId);
    featuresElement.appendChild(featureList);

    hideSpinner();
    showFeatures();
};

const hideSpinner = () => document.getElementById("pageMask").classList.add("hidden");
const showFeatures = () => document.getElementById("features").classList.remove("hidden");
