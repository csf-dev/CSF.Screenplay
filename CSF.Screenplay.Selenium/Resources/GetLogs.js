return function() {
    const
        storageKey = '__csfScreenplayLogs',
        allLogsStr = globalThis.sessionStorage.getItem(storageKey);

    if(!allLogsStr) throw new Error('The CaptureLogs script must have been executed on the current page before GetLogs may be used');

    globalThis.sessionStorage.setItem(storageKey, '[]');
    return JSON.parse(allLogsStr).map(x => ({
        Level: x.Level,
        Message: JSON.stringify(x.Message),
        Timestamp: x.Timestamp
    }));
}();
