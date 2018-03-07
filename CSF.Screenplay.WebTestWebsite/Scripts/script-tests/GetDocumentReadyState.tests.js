describe('The script which gets the document ready-state', function() {
  it('should return a string', function() {
    var result = executeScript();
    expect(typeof result).toBe('string');
  });

  it('should be equal to \'complete\'', function() {
    var result = executeScript();
    expect(result).toBe('complete');
  });
});