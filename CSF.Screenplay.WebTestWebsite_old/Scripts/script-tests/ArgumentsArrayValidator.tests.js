describe('The ArgumentsArrayValidator object', function() {

  describe('should raise errors when', function() {
    it('the array is not provided', function() {
      expect(function() {
        argumentsArrayValidator.validate();
      }).toThrowError(Error, /non-null/);
    });

    it('the array is null', function() {
      expect(function() {
        argumentsArrayValidator.validate(null);
      }).toThrowError(Error, /non-null/);
    });

    it('the array is not an array', function() {
      expect(function() {
        argumentsArrayValidator.validate("Not an array");
      }).toThrowError(Error, /must be an array/);
    });

    it('the array does not have the correct count of elements', function() {
      expect(function() {
        argumentsArrayValidator.validate([1, 2], 5);
      }).toThrowError(Error, /must contain precisely 5/);
    });

    it('the array has too few elements', function() {
      expect(function() {
        argumentsArrayValidator.validate([1, 2], 4, null);
      }).toThrowError(Error, /must contain at least 4/);
    });

    it('the array has too many elements', function() {
      expect(function() {
        argumentsArrayValidator.validate([1, 2, 3, 4], null, 2);
      }).toThrowError(Error, /must contain no more than 2/);
    });
  });

  it('should return true when the array has the correct count of elements', function() {
    expect(argumentsArrayValidator.validate([1, 2], 2))
      .toEqual(true);
  });

  it('should return true when the array has at least the minimum number of elements', function() {
    expect(argumentsArrayValidator.validate([1, 2, 3], 2, null))
      .toEqual(true);
  });

  it('should return true when the array has no more than the maximum number of elements', function() {
    expect(argumentsArrayValidator.validate([1, 2], null, 4))
      .toEqual(true);
  });

});