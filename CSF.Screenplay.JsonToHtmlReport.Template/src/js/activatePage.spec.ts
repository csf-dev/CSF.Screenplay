import { expect, test, vi } from 'vitest';
import { activatePage } from "./activatePage";
import { getElementByIdNotNull } from './Utils';

vi.mock(import('./Utils'));

test('activatePage should append the feature list to its container', () => {
    const featureList = document.createElement("ul");
    const featuresElement = document.createElement("div");
    const pageMask = document.createElement("div");
    const features = document.createElement("div");
    features.classList.add('hidden');

    vi.mocked(getElementByIdNotNull).mockImplementation((id) => {
        switch (id) {
            case "featureList":
                return featuresElement;
            case "loadingMask":
                return pageMask;
            case "featuresSection":
                return features;
            default:
                throw new Error('Unexpected id');
        }
    });

    activatePage(featureList);

    expect(featuresElement.children).toContain(featureList);
    expect(pageMask.classList).toContain("hidden");
    expect(features.classList).not.toContain("hidden");
});