import { ReportLoader } from "./ReportLoader";
import { getElementById } from './getElementById';

jest.mock('./getElementById');

test('ReportLoader should load JSON content from the specified script element', () => {
    const scriptElement = { textContent: '{"foo": "bar"}' };
    getElementById.mockReturnValue(scriptElement);

    const reportLoader = new ReportLoader('scriptId');
    const jsonData = reportLoader.loadJson();

    expect(jsonData).toEqual({foo: 'bar'});
});

test('ReportLoader should throw an error if the specified script element is not found', () => {
    getElementById.mockReturnValue(null);

    const reportLoader = new ReportLoader('scriptId');
    expect(() => reportLoader.loadJson()).toThrowError('Element with id scriptId not found');
});

test('ReportLoader should return an empty report if the specified script element does not contain valid JSON', () => {
    const scriptElement = { textContent: 'invalid json' };
    getElementById.mockReturnValue(scriptElement);

    const reportLoader = new ReportLoader('scriptId');
    const jsonData = reportLoader.loadJson();

    expect(jsonData).toEqual({
        Metadata: {
            Timestamp: new Date(),
            ReportFormatVersion: "0.0.0",
        },
        Performances: [],
    });
});