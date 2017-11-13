(function(report, undefined) {
  'use strict';
  
  function PageInteractionsCtor()
  {
    var self = this;
    
    self.filter = new report.Filter();
    self.folding = new report.Folding();
  }
  
  PageInteractionsCtor.prototype.init = function()
  {
    this.filter.init();
    this.folding.init();
  }
  
  report.PageInteractions = PageInteractionsCtor;
  
}(report = window.report || {}));