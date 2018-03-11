describe('The UpdateSelectElementSelection service', function() {
  'use strict';

  var scratchArea;

  function clearNodes(element)
  {
    while(element.firstChild)
      element.removeChild(element.firstChild);
  }

  function addScratchElement(name)
  {
    var element = document.createElement(name);
    scratchArea.appendChild(element);
    return element;
  }
  
  function addStandardScratchElements()
  {
    var divElement = addScratchElement('div');
    var selectElement = addScratchElement('select');
    var multiSelectElement = addScratchElement('select');
    multiSelectElement.setAttribute('multiple', 'multiple');
    
    return {
      div: divElement,
      select: selectElement,
      multiSelect: multiSelectElement
    };
  }

  beforeEach(function() {
     scratchArea = document.getElementById('test_scratch_area');
  });

  afterEach(function() {
    clearNodes(scratchArea);
  });

  it('should use the arguments-array validator', function() {
    spyOn(argumentsArrayValidator, 'validate')
      .and
      .returnValue(true);
    
    var elements = addStandardScratchElements();

    executeScript([elements.select, 'deselectAll']);
    expect(argumentsArrayValidator.validate).toHaveBeenCalledWith([elements.select, 'deselectAll'], 2, 3);
  });
  
  describe('should raise an error', function() {
    it('when the given element is null', function() {
      expect(function() {
        executeScript([null, 'deselectAll']);
      }).toThrowError(Error, /must provide an HTML element/);
    });
    
    it('when the given element is not an HTML select element', function() {
      var elements = addStandardScratchElements();
    
      expect(function() {
        executeScript([elements.div, 'deselectAll']);
      }).toThrowError(Error, /must be an HTML <select> element/);
    });
    
    it('when the action is not supported', function() {
      var elements = addStandardScratchElements();
    
      expect(function() {
        executeScript([elements.select, 'foobar']);
      }).toThrowError(Error, /'foobar' was not recognised/);
    });
  });
});