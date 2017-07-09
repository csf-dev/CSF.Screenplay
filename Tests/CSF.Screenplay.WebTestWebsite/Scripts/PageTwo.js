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
    singleSelectionValue = $('#single_selected_value');
    
  singleSelection.on('change', function() {
    singleSelectionValue.text(singleSelection.val());
  });
  
}(window, jQuery));