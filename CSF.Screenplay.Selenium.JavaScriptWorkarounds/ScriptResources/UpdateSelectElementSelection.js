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
    
    function setSelectedByIndex(selectElement, index, selected)
    {
      var option = getOptionByIndex(selectElement, index);
      return setSelected(option, selected);
    }
    
    function setSelectedByValue(selectElement, value, selected)
    {
      var option = getOptionByValue(selectElement, value);
      return setSelected(option, selected);
    }
    
    function setSelectedByText(selectElement, text, selected)
    {
      var option = getOptionByText(selectElement, text);
      return setSelected(option, selected);
    }
    
    function setSelected(option, selected)
    {
      if(!option) return false;
      option.selected = selected;
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
      selectByIndex: function(ele, idx) { return setSelectedByIndex(ele, idx, true); },
      selectByValue: function(ele, val) { return setSelectedByValue(ele, val, true); },
      selectByText: function(ele, txt) { return setSelectedByText(ele, txt, true); },
      deselectByIndex: function(ele, idx) { return setSelectedByIndex(ele, idx, false); },
      deselectByValue: function(ele, val) { return setSelectedByValue(ele, val, false); },
      deselectByText: function(ele, txt) { return setSelectedByText(ele, txt, false); },
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