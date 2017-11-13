(function($, report, undefined) {
  'use strict';
  
  $(function() {
    $('html').addClass('js');
    var page = new report.PageInteractions();
    page.init();
  })
  
}(jQuery, report = window.report || {}));