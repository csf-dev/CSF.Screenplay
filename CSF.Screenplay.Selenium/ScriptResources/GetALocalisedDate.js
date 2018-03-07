function executeScript(argsArray) {
  'use strict';

  if(!argsArray) throw new Error('Arguments must be a non-null array.');
  if(!(argsArray instanceof Array)) throw new Error('Arguments must be an array.');
  if(argsArray.length != 3) throw new Error('Arguments must contain precisely three elements (Y/M/D).');

  var year = argsArray[0], month = argsArray[1], day = argsArray[2], theDate;

  if(isNaN(year) || isNaN(month) || isNaN(day))
    throw new Error('The supplied year, month and day must all be numbers.');
  
  theDate = new Date(year, month, day);
  return theDate.toLocaleDateString();
}