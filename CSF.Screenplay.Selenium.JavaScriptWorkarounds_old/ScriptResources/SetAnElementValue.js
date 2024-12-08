function executeScript(argsArray)
{
  'use strict';

  argumentsArrayValidator.validate(argsArray, 2);

  var htmlElement = argsArray[0], newValue = argsArray[1];

  function validateElement(element)
  {
    if(element === null || element === undefined)
      throw new Error('You must provide an HTML element object.');
    if(!(element instanceof HTMLElement))
      throw new Error('The element must be an HTML element.');
    if(element.value === undefined)
      throw new Error('The element must have a \'value\' property.');
  }

  function setValue(element, val)
  {
    element.value = val;
  }

  function triggerChangeEvent(element)
  {
    var ev = document.createEvent('Event');
    ev.initEvent('change', true, true);
    element.dispatchEvent(ev);
  }

  validateElement(htmlElement);
  setValue(htmlElement, newValue);
  triggerChangeEvent(htmlElement);

  return true;
}
