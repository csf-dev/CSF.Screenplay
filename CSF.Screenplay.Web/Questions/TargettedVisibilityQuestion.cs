using System;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// Specialisation of a targetted question.  This is a question which includes visibility amongst its queries.
  /// Thus, it should catch a <see cref="TargetNotFoundException"/> if it is raised, and return false to indicate
  /// that the element is not visible.
  /// </summary>
  public class TargettedVisibilityQuestion : TargettedQuestion<bool>
  {
    /// <summary>
    /// Gets the answer to the question.
    /// </summary>
    /// <returns>The answer.</returns>
    /// <param name="actor">The actor asking this question.</param>
    protected override bool GetAnswer(Actors.IPerformer actor)
    {
      var ability = GetAbility(actor);

      IWebElementAdapter adapter;
      try
      {
        adapter = GetWebElementAdapter(ability);
      }
      catch(TargetNotFoundException)
      {
        return false;
      }

      return GetAnswer(actor, ability, adapter);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Questions.TargettedVisibilityQuestion"/> class.
    /// </summary>
    /// <param name="target">Target.</param>
    /// <param name="query">Query.</param>
    public TargettedVisibilityQuestion(ITarget target, IQuery<bool> query) : base(target, query) {}
  }
}
