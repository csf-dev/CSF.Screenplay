namespace CSF.Screenplay.Reporting;

[TestFixture,Parallelizable]
public class WritePermissionTesterTests
{
    const string windowsFilePathToWhichWeHaveNoPermissions = @"\\nonexistent-host-4a3fb907-e634-449c-b724-b5c80e3eaffb\this\path\does\not\exist";

    const string linuxFilePathToWhichWeHaveNoPermissions = "/root/no-permissions";

    static string GetPathWithNoPermissions() => OperatingSystem.IsWindows() ? windowsFilePathToWhichWeHaveNoPermissions : linuxFilePathToWhichWeHaveNoPermissions;


    [Test,AutoMoqData]
    public void HasWritePermissionShouldReturnFalseForANullString(WritePermissionTester sut)
    {
        Assert.That(sut.HasWritePermission(null), Is.False);
    }

    [Test,AutoMoqData]
    public void HasWritePermissionShouldReturnFalseForAnEmptyString(WritePermissionTester sut)
    {
        Assert.That(sut.HasWritePermission(string.Empty), Is.False);
    }

    [Test,AutoMoqData]
    public void HasWritePermissionShouldReturnFalseForAWhitespaceOnlyString(WritePermissionTester sut)
    {
        Assert.That(sut.HasWritePermission("  "), Is.False);
    }

    [Test,AutoMoqData]
    public void HasWritePermissionShouldReturnFalseForNonExistentNetworkShare(WritePermissionTester sut)
    {
        Assert.That(sut.HasWritePermission(GetPathWithNoPermissions()), Is.False);
    }

    [Test,AutoMoqData]
    public void HasWritePermissionShouldReturnTrueForARelativeFilenameInTheCurrentDirectory(WritePermissionTester sut)
    {
        Assert.That(sut.HasWritePermission("WritePermissionTesterTests_testfile.txt"), Is.True);
    }
}