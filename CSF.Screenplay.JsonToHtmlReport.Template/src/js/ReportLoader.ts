import type { ScreenplayReport } from "./ScreenplayReport";
import { getElementById } from './getElementById';

const reportContentId = 'reportSrc';

export interface LoadsReport {
    loadJson() : ScreenplayReport
}

export class ReportLoader implements LoadsReport {
    scriptId : string;

    constructor(scriptId : string) {
        this.scriptId = scriptId;
    }

    loadJson() : ScreenplayReport {
        const scriptElement : HTMLElement | null = getElementById(this.scriptId);
        if (!scriptElement) {
            throw new Error(`Element with id ${this.scriptId} not found`);
        }

        try {
            return JSON.parse(scriptElement.textContent);
        } catch (error : mixed) {
            throw new Error('Failed to parse JSON content whilst loading a Screenplay report', {cause: error});
        }
    }
}

export function getReportLoader() : LoadsReport {
    return new ReportLoader(reportContentId);
}