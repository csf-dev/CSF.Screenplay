describe('The SetAnElementValue service', function() {

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
    
    executeScript([element, 3]);
    expect(argumentsArrayValidator.validate).toHaveBeenCalledWith([element, 3], 2);
  });

  it('should throw an error when the element is null', function() {
    expect(function() {
      executeScript([null, 'value']);
    }).toThrowError(Error, /must provide an HTML element/);
  });

  it('should throw an error when used with an object which is not an HTMLElement', function() {
    expect(function() {
      executeScript([{ not: 'an', html: 'element' }, 'value']);
    }).toThrowError(Error, /must be an HTML element/);
  });

  it('should throw an error when used with an HTML element that does not have a value', function() {
    var element = addScratchElement('div');
    expect(function() {
      executeScript([element, 'value']);
    }).toThrowError(Error, /must have a 'value' property/);
  });

  it('should be able to set the value of an HTML <input> element', function() {
    var element = addScratchElement('input');

    executeScript([element, 'newVal']);

    expect(element.value).toEqual('newVal');
  });

  it('should be able to set the value of an HTML <textarea> element', function() {
    var element = addScratchElement('textarea');

    executeScript([element, 'newVal']);

    expect(element.value).toEqual('newVal');
  });

  it('should trigger the change event when it sets the value', function() {
    var element = addScratchElement('input'), eventTriggered = false;
    element.addEventListener('change', function() { eventTriggered = true; return true; })

    executeScript([element, 'newVal']);

    expect(eventTriggered).toEqual(true);
  });

});