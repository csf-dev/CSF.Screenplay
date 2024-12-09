(function(window, $, undefined) {
  'use strict';

  var
    divWithClass,
    textInput,
    dateInput,
    getOutputButton,
    classDisplay,
    dateDisplay,
    textDisplay,
    textPlaceholder;

  function setupElements()
  {
    divWithClass = $('#DivWithClass');
    textInput = $('#TextInput');
    dateInput = $('#DateInput');

    classDisplay = $('#DisplayDivClass');
    dateDisplay = $('#DisplayDateReadonly');
    textDisplay = $('#DisplayTextReadonly');
    textPlaceholder = $('#DisplayTextPlaceholder');

    getOutputButton = $('#GetOutput');

    getOutputButton.on('click', function(ev) {
      classDisplay.text(divWithClass.attr('class'));
      dateDisplay.text(dateInput.attr('readonly'));
      textDisplay.text(textInput.attr('readonly'));
      textPlaceholder.text(textInput.attr('placeholder'));

      ev.preventDefault();
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