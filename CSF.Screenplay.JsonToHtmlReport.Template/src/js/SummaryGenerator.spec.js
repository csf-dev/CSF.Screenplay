/**
 * @jest-environment jsdom
*/

import { SummaryDataGenerator } from './SummaryDataGenerator';
import { getElementById } from './getElementById';
import { SummaryGenerator } from './SummaryGenerator';

jest.mock('./getElementById');
jest.mock('./SummaryDataGenerator');

test('SummaryGenerator should generate a summary table', () => {
    const summaryDataGenerator = new SummaryDataGenerator();
    const summary = {
        features: {
            successCount: "1",
            failCount: "1",
            totalCount: "2"
        },
        scenarios: {
            successCount: "2",
            failCount: "1",
            totalCount: "3"
        }
    };
    const htmlNodes = {};
    const templateContent = {
        querySelector: jest.fn(id => {
            if(htmlNodes[id] === undefined) {
                htmlNodes[id] = { textContent: "" };
            }
            return htmlNodes[id];
        })
    };
    const template = {
        content: { cloneNode: jest.fn(() => templateContent) }
    };
    getElementById.mockReturnValue(template);
    summaryDataGenerator.getSummaryData.mockReturnValue(summary);

    const sut = new SummaryGenerator({}, summaryDataGenerator);
    const result = sut.generateSummary();

    expect(result).toBe(templateContent);
    expect(template.content.cloneNode).toHaveBeenCalledWith(true);
    expect(templateContent.querySelector).toHaveBeenCalledWith('#featureSuccess');
    expect(templateContent.querySelector).toHaveBeenCalledWith('#featureFailure');
    expect(templateContent.querySelector).toHaveBeenCalledWith('#featureTotal');
    expect(templateContent.querySelector).toHaveBeenCalledWith('#scenarioSuccess');
    expect(templateContent.querySelector).toHaveBeenCalledWith('#scenarioFailure');
    expect(templateContent.querySelector).toHaveBeenCalledWith('#scenarioTotal');
    expect(templateContent.querySelector('#featureSuccess').textContent).toBe("1");
    expect(templateContent.querySelector('#featureFailure').textContent).toBe("1");
    expect(templateContent.querySelector('#featureTotal').textContent).toBe("2");
    expect(templateContent.querySelector('#scenarioSuccess').textContent).toBe("2");
    expect(templateContent.querySelector('#scenarioFailure').textContent).toBe("1");
    expect(templateContent.querySelector('#scenarioTotal').textContent).toBe("3");
});