import type { ScreenplayReport } from "./Models";
import { getElementByIdNotNull } from './Utils';

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
        const scriptElement = getElementByIdNotNull(this.scriptId);

        try {
            return JSON.parse(scriptElement.textContent);
        } catch (error : unknown) {
            throw new Error('Failed to parse JSON content whilst loading a Screenplay report', {cause: error});
        }
    }
}

export function getReportLoader() : LoadsReport {
    return new ReportLoader(reportContentId);
}