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
      return selectElement.item(index);
    }
    
    function getOptionByValue(selectElement, value)
    {
      var options = selectElement.options;
      for(var i = 0, len = options.length; i < len; i++)
        if(options[i].value === value) return options[i];
      return null;
    }
    
    function getOptionByText(selectElement, text)
    {
      var options = selectElement.options;
      for(var i = 0, len = options.length; i < len; i++)
        if(options[i].text === text) return options[i];
      return null;
    }
    
    function select(option)
    {
      option.selected = true;
      option.setAttribute('selected', '');
    }
    
    function deselect(option)
    {
      option.selected = false;
      option.removeAttribute('selected');
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
      var option = getOptionByValue(selectElement, value);
      if(!option) return false;
      select(option);
      return true;
    }
    
    function selectByText(selectElement, text)
    {
      var option = getOptionByText(selectElement, text);
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
      var option = getOptionByValue(selectElement, value);
      if(!option) return false;
      deselect(option);
      return true;
    }
    
    function deselectByText(selectElement, text)
    {
      var option = getOptionByText(selectElement, text);
      if(!option) return false;
      deselect(option);
      return true;
    }
    
    function deselectAll(selectElement)
    {
      var options = selectElement.options;
      for(var i = 0, len = options.length; i < len; i++)
        options.item(i).selected = false;
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