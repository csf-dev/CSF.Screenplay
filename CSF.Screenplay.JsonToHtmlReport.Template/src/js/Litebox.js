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

    open(imageUrl, filename, summary, isBlobUrl) {
        this.#imageUrl = imageUrl;
        this.#isBlobUrl = isBlobUrl;
        this.#main.classList.add('open');

        this.#summary.textContent = summary;
        this.#content.src = this.#imageUrl;
        this.#content.alt = summary;
        this.#download.href = this.#imageUrl;
        this.#download.download = filename;
    }

    close() {
        this.#main.classList.remove('open');
        if(this.#isBlobUrl) {
            URL.revokeObjectURL(this.#imageUrl);
        }
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