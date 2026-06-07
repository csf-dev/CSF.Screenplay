import { expect, test, vi } from 'vitest';
import { updateReportTime } from './updateReportTime';
import { getElementById } from './Utils';

vi.mock(import('./Utils'));

test('updateReportTime should set the element content to the specified timestamp', () => {
    const timestampString = "2020-01-01T00:00:00Z";
    const date = new Date(Date.parse(timestampString));
    const element = { innerText: "" };
    vi.mocked(getElementById).mockReturnValue(element as unknown as HTMLElement);

    updateReportTime(timestampString);

    expect(element.innerText).toBe(date.toLocaleString());
});