using NUnit.Framework.Constraints;

namespace CSF.Screenplay.Selenium;

public class Has : NUnit.Framework.Has
{
    public static Constraint PrivateFieldEqualTo(string fieldName, object expected)
        => new HasPrivateFieldEqualTo(fieldName, expected);
}