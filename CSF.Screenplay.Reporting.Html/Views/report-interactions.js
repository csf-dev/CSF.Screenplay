(function($, report, undefined) {
  'use strict';
  
  if(!$) return;
  
  $(function() {
    $('html').addClass('js');
    var page = new report.PageInteractions();
    page.init();
  })
  
}(window.jQuery || null, report = window.report || {}));