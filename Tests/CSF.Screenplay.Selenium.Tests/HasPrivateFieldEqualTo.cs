using System.Reflection;
using NUnit.Framework.Constraints;

namespace CSF.Screenplay.Selenium;

public class HasPrivateFieldEqualTo : Constraint
{
    readonly string fieldName;
    readonly object expectedValue;

    public override ConstraintResult ApplyTo<TActual>(TActual actual)
    {
        var field = typeof(TActual).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        if(field == null) return new PrivateFieldEqualToConstraintResult(this, actual) { Problem = $"The object does not have a private field named {fieldName}" };

        var fieldValue = field.GetValue(actual);
        return new EqualConstraint(expectedValue).ApplyTo(fieldValue).IsSuccess
            ? new ConstraintResult(this, actual, true)
            : new PrivateFieldEqualToConstraintResult(this, actual) { Problem = $"The value of {fieldName} was {fieldValue}, expected {expectedValue}" };
    }

    public HasPrivateFieldEqualTo(string fieldName, object expectedValue)
    {
        this.fieldName = fieldName;
        this.expectedValue = expectedValue;
    }

    public class PrivateFieldEqualToConstraintResult : ConstraintResult
    {
        public string Problem { get; set; }

        public override void WriteMessageTo(MessageWriter writer)
            => writer.Write(Problem);

        public PrivateFieldEqualToConstraintResult(IConstraint constraint, object actualValue) : base(constraint, actualValue, false)
        {
        }
    }
}