var argumentsArrayValidator = function()
{
  'use strict';

  function getLengthParams(min, max)
  {
    if(typeof min === 'number' && max === undefined)
    {
      return {
        count: min,
        validateCount: true,
      };
    }

    var minCount, validateMinCount = false, maxCount, validateMaxCount = false;

    if(typeof min === 'number')
    {
      minCount = min;
      validateMinCount = true;
    }

    if(typeof max === 'number')
    {
      maxCount = max;
      validateMaxCount = true;
    }

    return {
      min: minCount,
      validateMin: validateMinCount,
      max: maxCount,
      validateMax: validateMaxCount,
      validateCount: false,
    };
  }

  function validateType(argsArray)
  {
    if(!argsArray) throw new Error('Arguments must be a non-null array.');
    if(!(argsArray instanceof Array)) throw new Error('Arguments must be an array.');
  }

  function validateLength(argsArray, lengthParams)
  {
    if(lengthParams.validateCount && argsArray.length !== lengthParams.count)
      throw new Error('Arguments array must contain precisely ' + lengthParams.count + ' item(s).');

    if(lengthParams.validateMin && argsArray.length < lengthParams.min)
      throw new Error('Arguments array must contain at least ' + lengthParams.min + ' item(s).');

    if(lengthParams.validateMax && argsArray.length > lengthParams.max)
      throw new Error('Arguments array must contain no more than ' + lengthParams.max + ' item(s).');
  }

  function validate(argsArray, min, max)
  {
    validateType(argsArray);
    var lengthParams = getLengthParams(min, max);
    validateLength(argsArray, lengthParams);

    return true;
  }

  function selfValidate(argsArray, min, max)
  {
    try
    {
      return validate(argsArray, min, max);
    }
    catch(e) { throw new Error('The call to \'validateArgumentsArray\' raised an error: ' + e.message); }
  }

  return {
   validate: validate,
   selfValidate: selfValidate,
  };
}();

function validateArgumentsArray(argsArray)
{
  'use strict';

  validator.selfValidate(argsArray, 1, 3);
  var arrayToValidate = argsArray[0], min = argsArray[1], max = argsArray[2];
  return validator.validate(arrayToValidate, min, max);
}