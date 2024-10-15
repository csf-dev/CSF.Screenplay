export class ReportLoader {
    scriptId;

    constructor(scriptId) {
        this.scriptId = scriptId;
    }

    loadJson() {
        const scriptElement = document.getElementById(this.scriptId);
        if (!scriptElement) {
            throw new Error(`Element with id ${this.scriptId} not found`);
        }

        try {
            const jsonData = JSON.parse(scriptElement.textContent);
            return jsonData;
        } catch (error) {
            throw new Error('Failed to parse JSON content');
        }
    }
}
