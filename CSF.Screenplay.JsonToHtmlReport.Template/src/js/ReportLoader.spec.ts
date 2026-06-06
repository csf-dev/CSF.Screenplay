import { expect, test, vi } from 'vitest';
import { ReportLoader } from "./ReportLoader";
import { getElementByIdNotNull } from './Utils';

vi.mock(import('./Utils'));

test('ReportLoader should load JSON content from the specified script element', () => {
    const scriptElement = { textContent: '{"foo": "bar"}' };
    vi.mocked(getElementByIdNotNull).mockReturnValue(scriptElement as unknown as HTMLElement);

    const reportLoader = new ReportLoader('scriptId');
    const jsonData = reportLoader.loadJson();

    expect(jsonData).toEqual({foo: 'bar'});
});

test('ReportLoader should throw an error if the specified script element does not contain valid JSON', () => {
    const scriptElement = { textContent: 'invalid json' };
    vi.mocked(getElementByIdNotNull).mockReturnValue(scriptElement as unknown as HTMLElement);

    const reportLoader = new ReportLoader('scriptId');
    expect(() => reportLoader.loadJson()).toThrow('Failed to parse JSON content whilst loading a Screenplay report');
});