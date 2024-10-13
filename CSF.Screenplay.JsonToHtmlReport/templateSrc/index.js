import "./layers.css";
import "./reset.css";
import "./spinner.css"
import "./layout.css";
import "./content.css";

document.onreadystatechange = () => {
    if (document.readyState !== "complete") return;

    hideSpinner();
    showFeatures();
}

const hideSpinner = () => document.getElementById("pageMask").classList.add("hidden");
const showFeatures = () => document.getElementById("features").classList.remove("hidden");
