export const activatePage = () => {
    hideSpinner();
    showFeatures();
};

const hideSpinner = () => document.getElementById("pageMask").classList.add("hidden");
const showFeatures = () => document.getElementById("features").classList.remove("hidden");
