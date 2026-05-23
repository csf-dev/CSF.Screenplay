(function() {
    if(window['__csfScreenplayLogs']) return;

    window['__csfScreenplayLogs'] = {originalFunctions:{},messages:[]};
    const logs = window['__csfScreenplayLogs'];

    logs.originalFunctions.log = window.console.log;
    logs.originalFunctions.info = window.console.info;
    logs.originalFunctions.warn = window.console.warn;
    logs.originalFunctions.error = window.console.error;
    logs.originalFunctions.debug = window.console.debug;
    logs.originalFunctions.clear = window.console.clear;

    window.console.log = function(...args) {
        captureMessage('Info', args);
        logs.originalFunctions.log(...args);
    }
    window.console.info = function(...args) {
        captureMessage('Info', args);
        logs.originalFunctions.info(...args);
    }
    window.console.warn = function(...args) {
        captureMessage('Warning', args);
        logs.originalFunctions.warn(...args);
    }
    window.console.error = function(...args) {
        captureMessage('Severe', args);
        logs.originalFunctions.error(...args);
    }
    window.console.debug = function(...args) {
        captureMessage('Debug', args);
        logs.originalFunctions.debug(...args);
    }
    window.console.clear = function() {
        for(const message of logs.messages)
            message.Consumed = true;
        logs.originalFunctions.clear();
    }

    function captureMessage(level, args) {
        logs.messages.push({Level:level,Message:args,Timestamp:new Date()});
    }
})();
