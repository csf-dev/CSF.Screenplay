return function() {
    if(!window['__csfScreenplayLogs']) throw new Error('The CaptureLogs script must have been executed on the current page before GetLogs may be used');

    const logs = window['__csfScreenplayLogs'].messages.filter(x => !x.Consumed);
    for(const log of logs)
        log.Consumed = true;
    return logs;
}();
