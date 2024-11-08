/**
 * @jest-environment jsdom
*/

import { activatePage } from "./activatePage";
import { getElementById } from './getElementById';

jest.mock('./getElementById');

test('activatePage should append the summary table and feature list to the specified elements', () => {
    const summaryTable = document.createElement("table");
    const featureList = document.createElement("ul");
    const summaryElement = document.createElement("div");
    const featuresElement = document.createElement("div");
    const pageMask = document.createElement("div");
    const features = document.createElement("div");

    getElementById.mockImplementation((id) => {
        switch (id) {
            case "summary":
                return summaryElement;
            case "featureList":
                return featuresElement;
            case "pageMask":
                return pageMask;
            case "features":
                return features;
        }
    });

    activatePage(summaryTable, featureList);

    expect(summaryElement.children).toContain(summaryTable);
    expect(featuresElement.children).toContain(featureList);
    expect(pageMask.classList).toContain("hidden");
    expect(features.classList).not.toContain("hidden");
});