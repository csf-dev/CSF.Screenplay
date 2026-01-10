using NUnit.Framework.Internal;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium;

[TestFixture, Parallelizable]
public class NamedScriptTests
{
    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformable(NamedScript script)
    {
        var result = ExecuteAScript(script);
        Assert.That(result, Is.InstanceOf<Actions.ExecuteJavaScript>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith1Param(NamedScript<string> script)
    {
        var result = ExecuteAScript(script, "foo");
        Assert.That(result, Is.InstanceOf<Actions.ExecuteJavaScript>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith2Params(NamedScript<int, string> script)
    {
        var result = ExecuteAScript(script, 5, "bar");
        Assert.That(result, Is.InstanceOf<Actions.ExecuteJavaScript>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith3Params(NamedScript<bool, int, string> script)
    {
        var result = ExecuteAScript(script, true, 99, "test");
        Assert.That(result, Is.InstanceOf<Actions.ExecuteJavaScript>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith4Params(NamedScript<int, int, int, int> script)
    {
        var result = ExecuteAScript(script, 1, 2, 3, 4);
        Assert.That(result, Is.InstanceOf<Actions.ExecuteJavaScript>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith5Params(NamedScript<bool, bool, bool, bool, bool> script)
    {
        var result = ExecuteAScript(script, false, false, false, false, false);
        Assert.That(result, Is.InstanceOf<Actions.ExecuteJavaScript>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith6Params(NamedScript<int, int, int, int, int, int> script)
    {
        var result = ExecuteAScript(script, 0, 0, 0, 0, 0, 0);
        Assert.That(result, Is.InstanceOf<Actions.ExecuteJavaScript>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith7Params(NamedScript<int, int, int, int, int, int, string> script)
    {
        var result = ExecuteAScript(script, 2, 2, 2, 2, 2, 2, "Two!");
        Assert.That(result, Is.InstanceOf<Actions.ExecuteJavaScript>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWithAReturnType(NamedScriptWithResult<string> script)
    {
        var result = ExecuteAScript(script);
        Assert.That(result, Is.InstanceOf<Questions.ExecuteJavaScriptAndGetResult<string>>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith1ParamAndAReturnType(NamedScriptWithResult<string, string> script)
    {
        var result = ExecuteAScript(script, "foo");
        Assert.That(result, Is.InstanceOf<Questions.ExecuteJavaScriptAndGetResult<string>>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith2ParamsAndAReturnType(NamedScriptWithResult<int, string, string> script)
    {
        var result = ExecuteAScript(script, 5, "bar");
        Assert.That(result, Is.InstanceOf<Questions.ExecuteJavaScriptAndGetResult<string>>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith3ParamsAndAReturnType(NamedScriptWithResult<bool, int, string, string> script)
    {
        var result = ExecuteAScript(script, true, 99, "test");
        Assert.That(result, Is.InstanceOf<Questions.ExecuteJavaScriptAndGetResult<string>>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith4ParamsAndAReturnType(NamedScriptWithResult<int, int, int, int, string> script)
    {
        var result = ExecuteAScript(script, 1, 2, 3, 4);
        Assert.That(result, Is.InstanceOf<Questions.ExecuteJavaScriptAndGetResult<string>>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith5ParamsAndAReturnType(NamedScriptWithResult<bool, bool, bool, bool, bool, string> script)
    {
        var result = ExecuteAScript(script, false, false, false, false, false);
        Assert.That(result, Is.InstanceOf<Questions.ExecuteJavaScriptAndGetResult<string>>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith6ParamsAndAReturnType(NamedScriptWithResult<int, int, int, int, int, int, string> script)
    {
        var result = ExecuteAScript(script, 0, 0, 0, 0, 0, 0);
        Assert.That(result, Is.InstanceOf<Questions.ExecuteJavaScriptAndGetResult<string>>());
    }

    [Test, AutoMoqData]
    public void ExecuteAScriptShouldReturnAPerformableWith7ParamsAndAReturnType(NamedScriptWithResult<int, int, int, int, int, int, string, string> script)
    {
        var result = ExecuteAScript(script, 2, 2, 2, 2, 2, 2, "Two!");
        Assert.That(result, Is.InstanceOf<Questions.ExecuteJavaScriptAndGetResult<string>>());
    }
}
