(function(window, $, undefined) {
  'use strict';
  
  var delayClickOne, delayTargetOne;


  function getElements()
  {
     delayClickOne = $('#delay_click_one');
     delayTargetOne = $('#delay_appear_target_one');
  }

  function setupEvents()
  {
    delayClickOne.on('click', function(ev) {
      delayTargetOne.empty();

      setTimeout(function() {
        delayTargetOne.append($('<a href="https://google.com/" class="appeared">This link appears!</a>'));
      }, 6000);
    });
  }

  function init()
  {
    getElements();

    setupEvents();
  }
  
  $(function() {
    init();
  });
  
}(window, jQuery));