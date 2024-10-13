import "./layers.css";
import "./reset.css";
import "./spinner.css"
import "./layout.css";
import "./content.css";

document.onreadystatechange = () => {
    if (document.readyState !== "complete") return;

    hideSpinner();
}

const hideSpinner = () => document.getElementById("pageMask").classList.add("hidden");
