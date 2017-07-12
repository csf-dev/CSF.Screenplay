(function(window, $, undefined) {
  'use strict';
  
  var specialInputField = $('.special_text input');
  
  specialInputField.on('keyup', function() {
    if(specialInputField.val() === 'The right value')
    {
      $('#dynamic_value').text('different value');
    }
    else
    {
      $('#dynamic_value').text('dynamic value');
    }
  });
  
  var
    singleSelection = $('#single_selection'),
    singleSelectionValue = $('#single_selected_value'),
    multiSelection = $('#multiple_selection'),
    multiSelectionValue = $('#multiple_selected_value');
  
  function recalculateSelections(selection, value)
  {
    var val = selection.val();
    
    if(!val || !val.length)
      value.text('Nothing!');
    else
      value.text(val);
  }
  
  singleSelection.on('change', function() {
    recalculateSelections(singleSelection, singleSelectionValue);
  });
    
  multiSelection.on('change', function() {
    recalculateSelections(multiSelection, multiSelectionValue);
  });
  
  function init()
  {
    recalculateSelections(singleSelection, singleSelectionValue);
    recalculateSelections(multiSelection, multiSelectionValue);
  }
  
  $(function() {
    init();
  });
  
}(window, jQuery));