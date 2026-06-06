import { getElementByIdNotNull } from "./Utils";

const featuresElementId = 'featureList';

export const activatePage = (featureList : Node) => {
    const featuresElement = getElementByIdNotNull(featuresElementId);
    featuresElement.appendChild(featureList);

    hideSpinner();
    showFeatures();
};

const hideSpinner = () => getElementByIdNotNull("loadingMask").classList.add("hidden");
const showFeatures = () => getElementByIdNotNull("featuresSection").classList.remove("hidden");
