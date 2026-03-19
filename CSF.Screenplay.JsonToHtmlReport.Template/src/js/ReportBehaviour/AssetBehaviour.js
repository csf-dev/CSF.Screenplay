import { imageTypes } from "./ImageTypes";

export class AssetBehaviour {
    #litebox;
    #linkElement;
    #assetModel;

    initialise() {
        this.#linkElement.textContent = this.#assetModel.Summary;

        if(imageTypes.includes(this.#assetModel.ContentType))
            this.#initialiseImage();
        else
            this.#initialiseDownload();
    }

    #initialiseImage() {
        this.#linkElement.addEventListener('click', ev => {
            const usesBlobUrl = !!this.#assetModel.Data
            const url = usesBlobUrl ? this.#getBlobUrl() : this.#assetModel.Path;
            this.#litebox.open(url, this.#assetModel.Name, this.#assetModel.Summary, usesBlobUrl);
            ev.preventDefault();
        });
        this.#linkElement.href = null;
    }

    #initialiseDownload() {
        const usesBlobUrl = !!this.#assetModel.Data
        const url = usesBlobUrl ? this.#getBlobUrl() : this.#assetModel.Path;
        this.#linkElement.href = url;
        this.#linkElement.download = this.#assetModel.Name;
    }

    #getBlobUrl() {
        const dataBytes = this.#base64ToBytes(this.#assetModel.Data);
        const blob = new Blob([dataBytes], { type: this.#assetModel.ContentType });
        return URL.createObjectURL(blob);
    }

    #base64ToBytes(base64Data) {
        const binaryData = atob(base64Data);
        const buffer = new Uint8Array(binaryData.length);
        for (let i = 0; i < binaryData.length; i++) {
            buffer[i] = binaryData.codePointAt(i);
        }
        return buffer;
    }

    constructor(litebox, element, assetModel) {
        this.#litebox = litebox;
        this.#linkElement = element.querySelector('a')
        this.#assetModel = assetModel;
    }
}