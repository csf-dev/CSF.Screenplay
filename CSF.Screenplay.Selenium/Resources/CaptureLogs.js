(function() {
    const
        storageKey = '__csfScreenplayLogs',
        orig = {};
    globalThis.sessionStorage.setItem(storageKey, '[]');

    orig.log = globalThis.console.log;
    orig.info = globalThis.console.info;
    orig.warn = globalThis.console.warn;
    orig.error = globalThis.console.error;
    orig.debug = globalThis.console.debug;
    orig.clear = globalThis.console.clear;

    globalThis.console.log = function(...args) {
        captureMessage('Info', args);
        orig.log(...args);
    }
    globalThis.console.info = function(...args) {
        captureMessage('Info', args);
        orig.info(...args);
    }
    globalThis.console.warn = function(...args) {
        captureMessage('Warning', args);
        orig.warn(...args);
    }
    globalThis.console.error = function(...args) {
        captureMessage('Severe', args);
        orig.error(...args);
    }
    globalThis.console.debug = function(...args) {
        captureMessage('Debug', args);
        orig.debug(...args);
    }
    globalThis.console.clear = function() {
        for(const message of logs.messages)
            message.Consumed = true;
        orig.clear();
    }

    function captureMessage(level, args) {
        const logs = JSON.parse(globalThis.sessionStorage.getItem(storageKey));
        logs.push({Level:level,Message:args,Timestamp:new Date()});
        globalThis.sessionStorage.setItem(storageKey, JSON.stringify(logs));
    }
})();
