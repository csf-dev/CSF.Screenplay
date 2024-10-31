import { getElementById } from './getElementById';

export const updateReportTime = (timestampString) => {
    const date = new Date(Date.parse(timestampString));
    const element = getElementById("reportGeneratedOn");
    element.innerText = date.toLocaleString();
}