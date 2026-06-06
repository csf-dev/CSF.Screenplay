import { DisplaysImage } from "../Litebox";
import { PerformableReport } from "../Models";
import { AssetBehaviour } from "../ReportBehaviour";
import { getElementById, querySelectorNotNull } from "../Utils";

export class AssetsWriter {
    writeAssets(reportable : PerformableReport) {
        const assetsRootElement = querySelectorNotNull('.assets', this.reportableElement);

        if (!reportable.Assets?.length) {
            assetsRootElement.remove();
            return;
        }

        const assetsElement = querySelectorNotNull<HTMLUListElement>('ul', assetsRootElement);

        for (const asset of reportable.Assets) {
            const assetElement = this.assetTemplateElement.content.cloneNode(true) as HTMLLIElement;
            const behaviour = new AssetBehaviour(this.litebox, assetElement, asset);
            assetsElement.appendChild(assetElement);
            behaviour.initialise();
        }
    }

    constructor(readonly litebox : DisplaysImage, readonly reportableElement : HTMLElement, readonly assetTemplateElement : HTMLTemplateElement) { }
}

export function getAssetsWriter(reportableElement : HTMLElement, litebox : DisplaysImage) {
    return new AssetsWriter(litebox, reportableElement, getElementById('assetTemplate'));
}