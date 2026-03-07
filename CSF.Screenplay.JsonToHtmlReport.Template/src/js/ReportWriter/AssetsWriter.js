export class AssetsWriter {
    #litebox;
    #reportableElement;
    #assetTemplateElement;

    writeAssets(reportable) {
        const assetsRootElement = this.#reportableElement.querySelector('.assets');
        if (!reportable.Assets?.length) {
            assetsRootElement.remove();
        }
        else {
            const assetsElement = assetsRootElement.querySelector('ul');

            for (const asset of reportable.Assets) {
                const assetElement = this.#assetTemplateElement.content.cloneNode(true);
                const assetLinkElement = assetElement.querySelector('a');
                assetLinkElement.textContent = asset.FileSummary;
                assetLinkElement.href = asset.FilePath;
                assetsElement.appendChild(assetElement);
            }
        }

    }

    constructor(litebox, reportableElement, assetTemplateElement) {
        this.#litebox = litebox;
        this.#reportableElement = reportableElement;
        this.#assetTemplateElement = assetTemplateElement;
    }
}

export function getAssetsWriter(reportableELement, litebox) {
    return new AssetsWriter(litebox, reportableELement, getElementById('assetTemplate'));
}