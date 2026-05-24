(function() {
    if(globalThis['__csfScreenplayLogs']) return;

    globalThis['__csfScreenplayLogs'] = {originalFunctions:{},messages:[]};
    const logs = globalThis['__csfScreenplayLogs'];

    logs.originalFunctions.log = globalThis.console.log;
    logs.originalFunctions.info = globalThis.console.info;
    logs.originalFunctions.warn = globalThis.console.warn;
    logs.originalFunctions.error = globalThis.console.error;
    logs.originalFunctions.debug = globalThis.console.debug;
    logs.originalFunctions.clear = globalThis.console.clear;

    globalThis.console.log = function(...args) {
        captureMessage('Info', args);
        logs.originalFunctions.log(...args);
    }
    globalThis.console.info = function(...args) {
        captureMessage('Info', args);
        logs.originalFunctions.info(...args);
    }
    globalThis.console.warn = function(...args) {
        captureMessage('Warning', args);
        logs.originalFunctions.warn(...args);
    }
    globalThis.console.error = function(...args) {
        captureMessage('Severe', args);
        logs.originalFunctions.error(...args);
    }
    globalThis.console.debug = function(...args) {
        captureMessage('Debug', args);
        logs.originalFunctions.debug(...args);
    }
    globalThis.console.clear = function() {
        for(const message of logs.messages)
            message.Consumed = true;
        logs.originalFunctions.clear();
    }

    function captureMessage(level, args) {
        logs.messages.push({Level:level,Message:args,Timestamp:new Date()});
    }
})();
