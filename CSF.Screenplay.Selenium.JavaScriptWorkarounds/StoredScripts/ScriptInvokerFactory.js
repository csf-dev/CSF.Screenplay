var invoker = function()
{
  'use strict';

  function validateEntryPoint(entryPoint)
  {
    if(!entryPoint || typeof entryPoint !== 'function')
      throw new Error('The script entry-point must be a function');
  }

  function getArgumentsObjectAsArray(argumentsObject)
  {
    return Array.prototype.slice.call(argumentsObject);
  }

  function invoke(entryPoint, argumentsObject)
  {
    validateEntryPoint(entryPoint);
    var args = getArgumentsObjectAsArray(argumentsObject);
    return entryPoint(args);
  }

  return { invoke: invoke };
}();
