import { getElementById } from "./getElementById";

const featuresElementId = 'featureList';

export const activatePage = (featureList) => {
    const featuresElement = getElementById(featuresElementId);
    featuresElement.appendChild(featureList);

    hideSpinner();
    showFeatures();
};

const hideSpinner = () => getElementById("loadingMask").classList.add("hidden");
const showFeatures = () => getElementById("featuresSection").classList.remove("hidden");
