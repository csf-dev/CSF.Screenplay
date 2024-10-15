export const updateReportTime = (timestampString) => {
    const date = new Date(Date.parse(timestampString));
    var element = document.getElementById("reportGeneratedOn");
    element.innerText = date.toLocaleString();
}