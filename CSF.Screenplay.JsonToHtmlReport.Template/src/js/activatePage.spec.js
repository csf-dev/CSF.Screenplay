import { expect, test, vi } from 'vitest';
import { activatePage } from "./activatePage";
import { getElementById } from './getElementById';

vi.mock('./getElementById');

test('activatePage should append the feature list to its container', () => {
    const featureList = document.createElement("ul");
    const featuresElement = document.createElement("div");
    const pageMask = document.createElement("div");
    const features = document.createElement("div");
    features.classList.add('hidden');

    getElementById.mockImplementation((id) => {
        switch (id) {
            case "summary":
                return summaryElement;
            case "featureList":
                return featuresElement;
            case "loadingMask":
                return pageMask;
            case "featuresSection":
                return features;
        }
    });

    activatePage(featureList);

    expect(featuresElement.children).toContain(featureList);
    expect(pageMask.classList).toContain("hidden");
    expect(features.classList).not.toContain("hidden");
});