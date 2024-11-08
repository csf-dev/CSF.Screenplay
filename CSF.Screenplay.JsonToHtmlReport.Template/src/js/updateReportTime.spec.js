import { updateReportTime } from './updateReportTime';
import { getElementById } from './getElementById';

jest.mock('./getElementById');

test('updateReportTime should set the element content to the specified timestamp', () => {
    const timestampString = "2020-01-01T00:00:00Z";
    const date = new Date(Date.parse(timestampString));
    const element = { innerText: "" };
    getElementById.mockReturnValue(element);

    updateReportTime(timestampString);

    expect(element.innerText).toBe(date.toLocaleString());
});