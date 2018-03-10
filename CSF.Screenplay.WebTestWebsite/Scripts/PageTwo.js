(function(window, $, undefined) {
  'use strict';

  var
    specialInputField,
    scriptOutput,
    singleSelection,
    singleSelectionValue,
    multiSelection,
    multiSelectionValue;

  function setupElements()
  {
    specialInputField = $('.special_text input');
    scriptOutput = $('#ScriptOutput');
    
    specialInputField.on('change keyup', function() {
      if(specialInputField.val() === 'The right value')
      {
        $('#dynamic_value').text('different value');
      }
      else
      {
        $('#dynamic_value').text('dynamic value');
      }
    });
    
    singleSelection = $('#single_selection');
    singleSelectionValue = $('#single_selected_value');
    multiSelection = $('#multiple_selection');
    multiSelectionValue = $('#multiple_selected_value');

    singleSelection.on('change click', function() {
      recalculateSelections(singleSelection, singleSelectionValue);
    });
      
    multiSelection.on('change click', function() {
      recalculateSelections(multiSelection, multiSelectionValue);
    });
  }

  function recalculateSelections(selection, value)
  {
    var val = selection.val();
    
    if(!val || !val.length)
      value.text('Nothing!');
    else
      value.text(val);
  }

  function addCallableScripts()
  {
    window.myCallableScript = function()
    {
      scriptOutput.text('myCallableScript called');
    }
    
    window.addFive = function(inputVal)
    {
      if(!inputVal)
      {
        inputVal = 1;
        scriptOutput.text('addFive called without any input');
      }
      else
      {
        scriptOutput.text('addFive called with ' + inputVal);
      }
      
      return inputVal + 5;
    }
  }
  
  function init()
  {
    setupElements();

    recalculateSelections(singleSelection, singleSelectionValue);
    recalculateSelections(multiSelection, multiSelectionValue);
    addCallableScripts();
  }
  
  $(function() {
    init();
  });
  
}(window, jQuery));