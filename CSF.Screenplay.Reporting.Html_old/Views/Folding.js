(function(window, $, report, undefined) {
  'use strict';
  
  var collapsed = 'collapsed';
  
  function bindEvents()
  {
    var self = this;
    
    self.reportBody.on('click', self.selectors.featureHeaders, function() {
      var $this = $(this);
      $this.parent().toggleClass(collapsed);
    })
    
    self.reportBody.on('click', self.selectors.scenarioHeaders, function() {
      var $this = $(this);
      $this.parent().toggleClass(collapsed);
    })

    self.reportBody.on('click', self.selectors.reportables, function(ev) {
      var $this = $(this);
      $this.toggleClass(collapsed);
      $(self.selectors.reportables, $this).addClass(collapsed);
      ev.stopPropagation();
    })
  }
  
  function collapseAll()
  {
    var self = this;
    
    $(self.selectors.features, self.reportBody)
      .add(self.selectors.scenarios, self.reportBody)
      .add(self.selectors.reportables, self.reportBody)
      .not($(self.selectors.emptyReportables, self.reportBody).parent())
      .addClass(collapsed);
  }
  
  function FoldingCtor()
  {
    this.reportBody = $('section.report');
      
    this.selectors = {
      featureHeaders: '.feature>header',
      scenarioHeaders: '.scenario>header',
      reportables: '.reportable',
      emptyReportables: '.reportable>.empty',
      features: '.feature',
      scenarios: '.scenario',
    };
  }
  
  FoldingCtor.prototype.init = function()
  {
    bindEvents.apply(this);
    collapseAll.apply(this);
  }
  
  report.Folding = FoldingCtor;
  
}(window, window.jQuery || null, report = window.report || {}));