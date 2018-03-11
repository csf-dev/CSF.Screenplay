function executeScript(argsArray)
{
  'use strict';
  
  argumentsArrayValidator.validate(argsArray, 2, 3);

  function validateElement(element)
  {
    if(element === null || element === undefined)
      throw new Error('You must provide an HTML element object.');
    if(!(element instanceof HTMLSelectElement))
      throw new Error('The element must be an HTML <select> element.');
  }
  
  var optionUpdater = function()
  {
    function getOptionByIndex(selectElement, index)
    {
      
    }
    
    function getOptionByValue(selectElement, value)
    {
      
    }
    
    function getOptionByText(selectElement, text)
    {
      
    }
    
    function deselect(optionElement)
    {
      
    }
    
    function select(optionElement)
    {
      
    }
    
    function selectByIndex(selectElement, index)
    {
      var option = getOptionByIndex(selectElement, index);
      if(!option) return false;
      select(option);
      return true;
    }
    
    function selectByValue(selectElement, value)
    {
      var option = getOptionByValue(selectElement, index);
      if(!option) return false;
      select(option);
      return true;
    }
    
    function selectByText(selectElement, text)
    {
      var option = getOptionByText(selectElement, index);
      if(!option) return false;
      select(option);
      return true;
    }
    
    function deselectByIndex(selectElement, index)
    {
      var option = getOptionByIndex(selectElement, index);
      if(!option) return false;
      deselect(option);
      return true;
    }
    
    function deselectByValue(selectElement, value)
    {
      var option = getOptionByValue(selectElement, index);
      if(!option) return false;
      deselect(option);
      return true;
    }
    
    function deselectByText(selectElement, text)
    {
      var option = getOptionByText(selectElement, index);
      if(!option) return false;
      deselect(option);
      return true;
    }
    
    function deselectAll(selectElement)
    {
      var options = selectElement.options;
      for(var i = 0, len = options.length; i < len; i++)
        deselect(options.item(i));
      return options.length > 0;
    }
    
    return {
      selectByIndex: selectByIndex,
      selectByValue: selectByValue,
      selectByText: selectByText,
      deselectByIndex: deselectByIndex,
      deselectByValue: deselectByValue,
      deselectByText: deselectByText,
      deselectAll: deselectAll,
    };
    
  }();
  
  var
    element = argsArray[0],
    action = argsArray[1],
    value = argsArray[2];
  
  validateElement(element);
  
  if(!optionUpdater.hasOwnProperty(action))
    throw new Error("The action '" + action + "' was not recognised.");
  
  return optionUpdater[action](element, value);
}