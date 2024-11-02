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