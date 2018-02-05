using System;
using System.Text.RegularExpressions;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tasks
{
  /// <summary>
  /// A task which manages the inputting of a date value into an HTML 5 <c>&lt;input type="date" /&gt;</c> element
  /// as a locale-formatted string.
  /// </summary>
  public class EnterTheDateIntoAnHtml5InputTypeDate : Performable
  {
    const string
      Numbers = @"\d+",
      NonNumericCharacters = @"\D";
    static readonly Regex
      NumberMatcher = new Regex(Numbers, RegexOptions.Compiled),
      NonNumericStripper = new Regex(NonNumericCharacters, RegexOptions.Compiled);

    readonly DateTime date;
    readonly ITarget target;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} enters the date {date.ToString("yyyy-MM-dd")} into {target.GetName()} using the current locale's format";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var localeFormattedDate = actor.Perform(GetTheLocaleFormattedDate());
      var keysToPress = GetTheKeysToPress(localeFormattedDate);
      actor.Perform(Enter.TheText(keysToPress).Into(target));
    }

    IQuestion<string> GetTheLocaleFormattedDate()
      => new GetTheLocaleFormattedDate(date);

    string GetTheKeysToPress(string formattedDate)
    {
      var zeroPaddedFormattedDate = GetZeroPaddedFormattedDate(formattedDate);
      return NonNumericStripper.Replace(zeroPaddedFormattedDate, String.Empty);
    }

    string GetZeroPaddedFormattedDate(string formattedDate)
    {
      return NumberMatcher.Replace(formattedDate, match => {
        if(match.Length > 1)
          return match.Value;

        return String.Concat("0", match.Value);
      });
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Tasks.EnterTheDateIntoAnHtml5InputTypeDate"/> class.
    /// </summary>
    /// <param name="date">Date.</param>
    /// <param name="target">Target.</param>
    public EnterTheDateIntoAnHtml5InputTypeDate(DateTime date, ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.date = date;
      this.target = target;
    }
  }
}
