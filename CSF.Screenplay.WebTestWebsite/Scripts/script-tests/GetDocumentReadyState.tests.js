describe('The GetDocumentReadyState service', function() {
  it('should return a string', function() {
    var result = executeScript();
    expect(typeof result).toBe('string');
  });

  it('should be equal to \'complete\'', function() {
    var result = executeScript();
    expect(result).toBe('complete');
  });
});