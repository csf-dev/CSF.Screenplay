using System;
using CSF.Screenplay.Stopwatch;
using NUnit.Framework;

namespace CSF.Screenplay.Tests.Stopwatch
{
  [TestFixture]
  public class TimeSpanFormatterTests
  {
    // Just one component of the time
    [TestCase(1,  0,  0,  "1 minute")]
    [TestCase(2,  0,  0,  "2 minutes")]
    [TestCase(0,  1,  0,  "1 second")]
    [TestCase(0,  2,  0,  "2 seconds")]
    [TestCase(0,  0,  1,  "1 millisecond")]
    [TestCase(0,  0,  2,  "2 milliseconds")]
    // Multiple time components
    [TestCase(1,  5,  2,  "1 minute, 5 seconds, 2 milliseconds")]
    [TestCase(0,  1,  46,  "1 second, 46 milliseconds")]
    [Description("Tests a variety of timespan values and ensures that the formatter produces the expected results for each")]
    public void Format_returns_expected_results(int mins, int secs, int milli, string expected)
    {
      // Arrange
      var sut = new TimeSpanFormatter();
      var timespan = new TimeSpan(0, 0, mins, secs, milli);

      // Act
      var result = sut.Format(timespan);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }
  }
}
