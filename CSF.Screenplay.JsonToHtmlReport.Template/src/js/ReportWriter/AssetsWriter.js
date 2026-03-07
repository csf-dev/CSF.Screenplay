import { AssetBehaviour } from "../ReportBehaviour";
import { getElementById } from "../getElementById";

export class AssetsWriter {
    #litebox;
    #reportableElement;
    #assetTemplateElement;

    writeAssets(reportable) {
        const assetsRootElement = this.#reportableElement.querySelector('.assets');

        if (!reportable.Assets?.length) {
            assetsRootElement.remove();
            return;
        }

        const assetsElement = assetsRootElement.querySelector('ul');

        for (const asset of reportable.Assets) {
            const assetElement = this.#assetTemplateElement.content.cloneNode(true);
            const behaviour = new AssetBehaviour(this.#litebox, assetElement, asset);
            assetsElement.appendChild(assetElement);
            behaviour.initialise();
        }
    }

    constructor(litebox, reportableElement, assetTemplateElement) {
        this.#litebox = litebox;
        this.#reportableElement = reportableElement;
        this.#assetTemplateElement = assetTemplateElement;
    }
}

export function getAssetsWriter(reportableElement, litebox) {
    return new AssetsWriter(litebox, reportableElement, getElementById('assetTemplate'));
}