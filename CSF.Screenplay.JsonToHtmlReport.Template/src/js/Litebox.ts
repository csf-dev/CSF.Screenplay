import { getElementByIdNotNull, querySelectorNotNull } from './Utils';

const
    maskId = 'liteboxMask',
    downloadSelector = '#litebox .download',
    closeSelector = '#litebox .close',
    contentSelector = '#litebox .content',
    summarySelector = '#litebox .summary';

export interface DisplaysImage {
    open(imageUrl : string, filename : string, summary : string, isBlobUrl : boolean) : void,
    close() : void,
}

export class Litebox implements DisplaysImage {
    #main : HTMLElement;
    #download : HTMLElement;
    #close : HTMLElement;
    #content : HTMLImageElement;
    #summary : HTMLElement;
    #imageUrl : string = '';
    #isBlobUrl : boolean = false;
    #fileName : string = '';
    #downloadHandler : () => void = () => {};

    open(imageUrl : string, filename : string, summary : string, isBlobUrl : boolean) {
        this.#imageUrl = imageUrl;
        this.#isBlobUrl = isBlobUrl;
        this.#main.classList.add('open');

        this.#fileName = filename;
        this.#summary.textContent = summary;
        this.#content.src = this.#imageUrl;
        this.#content.title = filename;
        this.#downloadHandler = () => this.#onDownloadClick();
        this.#download.addEventListener('click', this.#downloadHandler);
    }

    close() {
        this.#main.classList.remove('open');
        if(this.#isBlobUrl) {
            URL.revokeObjectURL(this.#imageUrl);
        }
        this.#download.removeEventListener('click', this.#downloadHandler);
    }

    #onDownloadClick() {
        const hyperLink = document.createElement('a');
        hyperLink.href = this.#imageUrl;
        hyperLink.download = this.#fileName;
        hyperLink.click();
    }

    constructor() {
        this.#main = getElementByIdNotNull(maskId);
        this.#download = querySelectorNotNull(downloadSelector);
        this.#close = querySelectorNotNull(closeSelector);
        this.#close.addEventListener('click', () => this.close());
        this.#content = querySelectorNotNull(contentSelector);
        this.#summary = querySelectorNotNull(summarySelector);
    }
}

export function getLitebox() : DisplaysImage {
    return new Litebox();
}