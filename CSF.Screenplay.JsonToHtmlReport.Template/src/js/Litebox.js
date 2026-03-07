import { getElementById } from './getElementById';

const
    maskId = 'liteboxMask',
    downloadSelector = '#litebox .download',
    closeSelector = '#litebox .close',
    contentSelector = '#litebox .content',
    summarySelector = '#litebox .summary';

export class Litebox {
    #main;
    #download;
    #close;
    #content;
    #summary;
    #imageUrl;
    #isBlobUrl;
    #fileName;
    #downloadHandler;

    open(imageUrl, filename, summary, isBlobUrl) {
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
        this.#main = getElementById(maskId);
        this.#download = document.querySelector(downloadSelector);
        this.#close = document.querySelector(closeSelector);
        this.#close.addEventListener('click', () => this.close());
        this.#content = document.querySelector(contentSelector);
        this.#summary = document.querySelector(summarySelector);
    }
}