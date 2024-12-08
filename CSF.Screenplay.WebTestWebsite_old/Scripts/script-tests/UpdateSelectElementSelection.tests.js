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
    
    selectElement.add(new Option('Option one', '1', false, false));
    selectElement.add(new Option('Option two', '2', true, true));
    selectElement.add(new Option('Option three', '3', false, false));
    
    var multiSelectElement = document.createElement('select');
    multiSelectElement.setAttribute('multiple', '');
    multiSelectElement.multiple = true;
    
    multiSelectElement.add(new Option('Option one', '1', false, false));
    multiSelectElement.add(new Option('Option two', '2', true, true));
    multiSelectElement.add(new Option('Option three', '3', true, true));
    scratchArea.appendChild(multiSelectElement);
    
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
  
  describe('can update <select> elements which do not have the multiple="multiple" attribute', function() {
    it('when selecting by index', function() {
      var element = addStandardScratchElements().select;
      
      executeScript([element, 'selectByIndex', 0]);
      
      expect(element.selectedIndex).toEqual(0);
    });
    
    it('when selecting by value', function() {
      var element = addStandardScratchElements().select;
      
      executeScript([element, 'selectByValue', '3']);
      
      expect(element.selectedIndex).toEqual(2);
    });
    
    it('when selecting by text', function() {
      var element = addStandardScratchElements().select;
      
      executeScript([element, 'selectByText', 'Option three']);
      
      expect(element.selectedIndex).toEqual(2);
    });
  });
  
  describe('can update <select> elements which have the multiple="multiple" attribute', function() {
    it('when selecting by index', function() {
      var element = addStandardScratchElements().multiSelect;
      
      executeScript([element, 'selectByIndex', 0]);
      
      expect(element.options[0].selected).toEqual(true);
      expect(element.options[1].selected).toEqual(true);
      expect(element.options[2].selected).toEqual(true);
    });
    
    it('when deselecting by index', function() {
      var element = addStandardScratchElements().multiSelect;
      
      executeScript([element, 'deselectByIndex', 1]);
      
      expect(element.options[0].selected).toEqual(false);
      expect(element.options[1].selected).toEqual(false);
      expect(element.options[2].selected).toEqual(true);
    });
    
    it('when selecting by value', function() {
      var element = addStandardScratchElements().multiSelect;
      
      executeScript([element, 'selectByValue', '1']);
      
      expect(element.options[0].selected).toEqual(true);
      expect(element.options[1].selected).toEqual(true);
      expect(element.options[2].selected).toEqual(true);
    });
    
    it('when deselecting by value', function() {
      var element = addStandardScratchElements().multiSelect;
      
      executeScript([element, 'deselectByValue', '2']);
      
      expect(element.options[0].selected).toEqual(false);
      expect(element.options[1].selected).toEqual(false);
      expect(element.options[2].selected).toEqual(true);
    });
    
    it('when selecting by text', function() {
      var element = addStandardScratchElements().multiSelect;
      
      executeScript([element, 'selectByText', 'Option one']);
      
      expect(element.options[0].selected).toEqual(true);
      expect(element.options[1].selected).toEqual(true);
      expect(element.options[2].selected).toEqual(true);
    });
    
    it('when deselecting by text', function() {
      var element = addStandardScratchElements().multiSelect;
      
      executeScript([element, 'deselectByText', 'Option two']);
      
      expect(element.options[0].selected).toEqual(false);
      expect(element.options[1].selected).toEqual(false);
      expect(element.options[2].selected).toEqual(true);
    });
    
    it('when deselecting all options', function() {
      var element = addStandardScratchElements().multiSelect;
      
      executeScript([element, 'deselectAll']);
      
      expect(element.options[0].selected).toEqual(false);
      expect(element.options[1].selected).toEqual(false);
      expect(element.options[2].selected).toEqual(false);
    });
  });
  
  describe('should always raise the change event', function() {
    it('after selecting by index', function() {
      var element = addStandardScratchElements().multiSelect, eventTriggered = false;
      element.addEventListener('change', function() { eventTriggered = true; return true; })
      
      executeScript([element, 'selectByIndex', 0]);
      
      expect(eventTriggered).toEqual(true);
    });
    
    it('after selecting by value', function() {
      var element = addStandardScratchElements().multiSelect, eventTriggered = false;
      element.addEventListener('change', function() { eventTriggered = true; return true; })
      
      executeScript([element, 'selectByValue', '1']);
      
      expect(eventTriggered).toEqual(true);
    });
    
    it('after selecting by text', function() {
      var element = addStandardScratchElements().multiSelect, eventTriggered = false;
      element.addEventListener('change', function() { eventTriggered = true; return true; })
      
      executeScript([element, 'selectByText', 'Option one']);
      
      expect(eventTriggered).toEqual(true);
    });
    
    it('after deselecting by index', function() {
      var element = addStandardScratchElements().multiSelect, eventTriggered = false;
      element.addEventListener('change', function() { eventTriggered = true; return true; })
      
      executeScript([element, 'deselectByIndex', 1]);
      
      expect(eventTriggered).toEqual(true);
    });
    
    it('after deselecting by value', function() {
      var element = addStandardScratchElements().multiSelect, eventTriggered = false;
      element.addEventListener('change', function() { eventTriggered = true; return true; })
      
      executeScript([element, 'deselectByValue', '2']);
      
      expect(eventTriggered).toEqual(true);
    });
    
    it('after deselecting by text', function() {
      var element = addStandardScratchElements().multiSelect, eventTriggered = false;
      element.addEventListener('change', function() { eventTriggered = true; return true; })
      
      executeScript([element, 'deselectByText', 'Option two']);
      
      expect(eventTriggered).toEqual(true);
    });
    
    it('after deselecting everything', function() {
      var element = addStandardScratchElements().multiSelect, eventTriggered = false;
      element.addEventListener('change', function() { eventTriggered = true; return true; })
      
      executeScript([element, 'deselectAll']);
      
      expect(eventTriggered).toEqual(true);
    });
  });
});