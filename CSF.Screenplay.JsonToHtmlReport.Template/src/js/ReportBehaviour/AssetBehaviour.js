import { imageTypes } from "./ImageTypes";

export class AssetBehaviour {
    #litebox;
    #linkElement;
    #assetModel;

    initialise() {
        this.#linkElement.textContent = this.#assetModel.FileSummary;

        if(imageTypes.includes(this.#assetModel.ContentType))
            this.#initialiseImage();
        else
            this.#initialiseDownload();
    }

    #initialiseImage() {
        this.#linkElement.addEventListener('click', ev => {
            const usesBlobUrl = !!this.#assetModel.FileData
            const url = usesBlobUrl ? this.#getBlobUrl() : this.#assetModel.FilePath;
            this.#litebox.open(url, this.#assetModel.FileName, this.#assetModel.FileSummary, usesBlobUrl);
            ev.preventDefault();
        });
        this.#linkElement.href = null;
    }

    #initialiseDownload() {
        const usesBlobUrl = !!this.#assetModel.FileData
        const url = usesBlobUrl ? this.#getBlobUrl() : this.#assetModel.FilePath;
        this.#linkElement.href = url;
        this.#linkElement.download = this.#assetModel.FileName;
    }

    #getBlobUrl() {
        const dataBytes = this.#base64ToBytes(this.#assetModel.FileData);
        const blob = new Blob([dataBytes], { type: this.#assetModel.ContentType });
        return URL.createObjectURL(blob);
    }

    #base64ToBytes(base64Data) {
        const binaryData = atob(base64Data);
        const buffer = new Uint8Array(binaryData.length);
        for (let i = 0; i < binaryData.length; i++) {
            buffer[i] = binaryData.charCodeAt(i);
        }
        return buffer;
    }

    constructor(litebox, element, assetModel) {
        this.#litebox = litebox;
        this.#linkElement = element.querySelector('a')
        this.#assetModel = assetModel;
    }
}