(function(window, $, report, undefined) {
  'use strict';
  
  function getFilterClassName(level, type)
  {
    return 'filter_' + level + '_' + type;
  }
  
  function setSummaryTableStyle(level, type, active)
  {
    var
      selector = 'tr.' + level + ' .' + type,
      element = $(selector, this.summaryTable);
    if(active)
      element.addClass('active');
    else
      element.removeClass('active');
  }
  
  function clearFilter(level)
  {
    var types = [ 'success', 'failing', 'total' ];
    for(var i = 0; i < types.length; i++)
    {
      var type = types[i];
      this.reportBody.removeClass(getFilterClassName(level, type));
      setSummaryTableStyle.apply(this, [ level, type, false ])
    }
  }
  
  function markFirstAndLastFeatures()
  {
    $('.feature', this.reportBody)
      .removeClass('first_visible')
      .removeClass('last_visible');
    
    $('.feature:visible:first', this.reportBody)
      .addClass('first_visible');
    
    $('.feature:visible:last', this.reportBody)
      .addClass('last_visible');
  }
  
  function markFirstAndLastScenarios()
  {
    $('.feature .scenario', this.reportBody)
      .removeClass('first_visible')
      .removeClass('last_visible');
      
    $('.feature', this.reportBody)
      .each(function(id, feature) {
        var $feature = $(feature);
        $('.scenario:visible:first', $feature)
          .addClass('first_visible');
        
        $('.scenario:visible:last', $feature)
          .addClass('last_visible');
      });
  }
  
  function applyFilter(level, type)
  {
    clearFilter.apply(this, [ level ]);
    this.reportBody.addClass(getFilterClassName(level, type));
    setSummaryTableStyle.apply(this, [ level, type, true ])
    if(level === 'features')
    {
      markFirstAndLastFeatures.apply(this);
    }
    if(level === 'scenarios')
    {
      markFirstAndLastScenarios.apply(this);
    }
  }
  
  function bindEvents()
  {
    var self = this;
    
    self.summaryTable.on('click', 'td', function() {
      var
        $this = $(this),
        cellClass = $this.attr('class'),
        rowClass = $this.parent().attr('class');
      
      if($this.hasClass('active')) return;
      
      applyFilter.apply(self, [ rowClass, cellClass ]);
    });
  }
  
  function activateTotals()
  {
    applyFilter.apply(this, [ 'features', 'total' ]);
    applyFilter.apply(this, [ 'scenarios', 'total' ]);
  }
  
  function FilterCtor()
  {
    this.summaryTable = $('#summary_table');
    this.reportBody = $('section.report');
  }
  
  FilterCtor.prototype.init = function()
  {
    bindEvents.apply(this);
    activateTotals.apply(this);
  }
  
  report.Filter = FilterCtor;
  
}(window, jQuery, report = window.report || {}));