describe('The GetALocalisedDate service', function() {

  it('should use the arguments-array validator', function() {
    spyOn(argumentsArrayValidator, 'validate')
      .and
      .returnValue(true);
    
    executeScript([1, 2, 3]);
    expect(argumentsArrayValidator.validate).toHaveBeenCalledWith([1, 2, 3], 3);
  });

  it('should throw an error when the argument array contains elements which are not numbers', function() {
    expect(function() {
      executeScript([1, 'not a number', 3]);
    }).toThrowError(Error, /must all be numbers/);
  });

  it('should return a string which matches the locale-formatted date', function() {
    var expectedResult = new Date(2001, 2, 3).toLocaleDateString();
    var actualResult = executeScript([2001, 2, 3]);

    expect(actualResult).toBe(expectedResult);
  });

});