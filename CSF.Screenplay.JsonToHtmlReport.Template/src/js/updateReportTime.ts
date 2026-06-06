import { getElementById } from './Utils';

export const updateReportTime = (timestampString : string) => {
    const date = new Date(Date.parse(timestampString));
    const element = getElementById("reportGeneratedOn");
    if(element) element.innerText = date.toLocaleString();
}