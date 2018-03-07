describe('The script which gets a localised date', function() {
  describe('should raise errors if the input', function() {
    it('is not provided', function() {
      expect(function() {
        executeScript();
      }).toThrowError(Error, /non-null/);
    });

    it('is null', function() {
      expect(function() {
        executeScript(null);
      }).toThrowError(Error, /non-null/);
    });

    it('is not an array', function() {
      expect(function() {
        executeScript("Not an array");
      }).toThrowError(Error, /must be an array/);
    });

    it('has only two elements', function() {
      expect(function() {
        executeScript([1, 2]);
      }).toThrowError(Error, /must contain precisely three elements/);
    });

    it('has four elements', function() {
      expect(function() {
        executeScript([1, 2, 3, 4]);
      }).toThrowError(Error, /must contain precisely three elements/);
    });

    it('contains elements which are not numbers', function() {
      expect(function() {
        executeScript([1, 'not a number', 3]);
      }).toThrowError(Error, /must all be numbers/);
    });
  });

  it('should return a string which matches the locale-formatted date', function() {
    var expectedResult = new Date(2001, 2, 3).toLocaleDateString();
    var actualResult = executeScript([2001, 2, 3]);

    expect(actualResult).toBe(expectedResult);
  });
});