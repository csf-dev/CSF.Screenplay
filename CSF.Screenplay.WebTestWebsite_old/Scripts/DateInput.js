(function(window, $, undefined) {
  'use strict';

  var
    dateInput,
    dateOutput;

  function setupElements()
  {
    dateInput = $('#DateInput');
    dateOutput = $('#DateOutput');

    dateInput.on('change click keyup', function() {
      dateOutput.text(dateInput.val());
    });
  }

  function init()
  {
    setupElements();
  }
  
  $(function() {
    init();
  });
  
}(window, jQuery));