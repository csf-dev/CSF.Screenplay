describe('The SetAnElementAttribute service', function() {

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
    
    var element = addScratchElement('input');

    executeScript([element, 'foo', 'bar']);
    expect(argumentsArrayValidator.validate).toHaveBeenCalledWith([element, 'foo', 'bar'], 3);
  });

  it('should be able to remove a "readonly" attribute', function() {
    var element = addScratchElement('input');
    element.setAttribute('type', 'text');
    element.setAttribute('readonly', 'readonly');

    executeScript([element, 'readonly', null]);
    expect(element.getAttribute('readonly')).toEqual(null);
  });

  it('should be able to add a "readonly" attribute', function() {
    var element = addScratchElement('input');
    element.setAttribute('type', 'text');

    executeScript([element, 'readonly', 'readonly']);
    expect(element.getAttribute('readonly')).toEqual('readonly');
  });

  it('should be able to add a "placeholder" attribute', function() {
    var element = addScratchElement('input');
    element.setAttribute('type', 'text');

    executeScript([element, 'placeholder', 'hello']);
    expect(element.getAttribute('placeholder')).toEqual('hello');
  });

  it('should be able to change a "placeholder" attribute', function() {
    var element = addScratchElement('input');
    element.setAttribute('type', 'text');
    element.setAttribute('placeholder', 'hello');

    executeScript([element, 'placeholder', 'goodbye']);
    expect(element.getAttribute('placeholder')).toEqual('goodbye');
  });

});