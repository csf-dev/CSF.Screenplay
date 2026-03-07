import { getElementById } from './getElementById';

const reportContentId = 'reportSrc';

export class ReportLoader {
    scriptId;

    constructor(scriptId) {
        this.scriptId = scriptId;
    }

    loadJson() {
        const scriptElement = getElementById(this.scriptId);
        if (!scriptElement) {
            throw new Error(`Element with id ${this.scriptId} not found`);
        }

        try {
            const jsonData = JSON.parse(scriptElement.textContent);
            return jsonData;
        } catch (error) {
            console.error('Failed to parse JSON content whilst loading a Screenplay report', error);
            return this.#getEmptyReport();
        }
    }

    #getEmptyReport() {
        return {
            Metadata: {
                Timestamp: new Date(),
                ReportFormatVersion: "0.0.0",
            },
            Performances: [],
        };
    }
}

export function getReportLoader() {
    return new ReportLoader(reportContentId);
}