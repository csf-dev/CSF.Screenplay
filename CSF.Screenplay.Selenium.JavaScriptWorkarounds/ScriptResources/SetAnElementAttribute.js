function executeScript(argsArray)
{
  'use strict';

  argumentsArrayValidator.validate(argsArray, 3);

  var htmlElement = argsArray[0], attributeName = argsArray[1], newValue = argsArray[2];

  function validateElement(element)
  {
    if(element === null || element === undefined)
      throw new Error('You must provide an HTML element object.');
    if(!(element instanceof HTMLElement))
      throw new Error('The element must be an HTML element.');
  }

  validateElement(htmlElement);
  if(newVal === null)
    htmlElement.removeAttribute(attributeName);
  else
    htmlElement.setAttribute(attributeName, newValue);

  return true;
}
