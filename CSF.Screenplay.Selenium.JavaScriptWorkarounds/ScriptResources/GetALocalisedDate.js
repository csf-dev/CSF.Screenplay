function executeScript(argsArray) {
  'use strict';

  argumentsArrayValidator.validate(argsArray, 3);

  var
    year = argsArray[0],
    month = argsArray[1],
    day = argsArray[2],
    theDate;

  if(typeof year !== 'number' || typeof month !== 'number' || typeof day !== 'number')
    throw new Error('The supplied year, month and day must all be numbers.');
  
  theDate = new Date(year, month, day);
  return theDate.toLocaleDateString();
}