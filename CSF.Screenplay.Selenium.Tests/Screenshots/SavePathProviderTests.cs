//
// SavePathProviderTests.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using CSF.FlexDi;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Selenium.Screenshots;
using Moq;
using NUnit.Framework;

namespace CSF.Screenplay.Selenium.Tests.Screenshots
{
  [TestFixture]
  public class SavePathProviderTests
  {
    [Test]
    public void GetSaveFile_filename_contains_feature_id_when_saving_in_root_directory()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var options = new SaveOptions(new DirectoryInfo(Environment.CurrentDirectory));
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Name, Does.Contain(@"MyFeatureId"));
    }

    [Test]
    public void GetSaveFile_filename_does_not_contain_feature_id_when_saving_in_feature_directories()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var options = new SaveOptions(new DirectoryInfo(Environment.CurrentDirectory),
                                    useDirectoryForEachFeature: true);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Name, Does.Not.Contain(@"MyFeatureId"));
    }

    [Test]
    public void GetSaveFile_filename_contains_scenario_id_when_saving_in_root_directory()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var options = new SaveOptions(new DirectoryInfo(Environment.CurrentDirectory));
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Name, Does.Contain(@"MyFeatureId"));
    }

    [Test]
    public void GetSaveFile_filename_contains_scenario_id_when_saving_in_feature_directories()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var options = new SaveOptions(new DirectoryInfo(Environment.CurrentDirectory),
                                    useDirectoryForEachFeature: true);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Name, Does.Contain(@"MyScenarioId"));
    }

    [Test]
    public void GetSaveFile_filename_does_not_contain_scenario_id_when_saving_in_scenario_directories()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var options = new SaveOptions(new DirectoryInfo(Environment.CurrentDirectory),
                                    useDirectoryForEachFeature: true,
                                    useDirectoryForEachScenario: true);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Name, Does.Not.Contain(@"MyScenarioId"));
    }

    [TestCase(false, false)]
    [TestCase(true, false)]
    [TestCase(true, true)]
    public void GetSaveFile_filename_always_contains_screenshot_name_when_provided(bool useFeatureDirs, bool useScenarioDirs)
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var options = new SaveOptions(new DirectoryInfo(Environment.CurrentDirectory),
                                    useFeatureDirs,
                                    useScenarioDirs);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Name, Does.Contain(@"MyScreenshotName"));
    }

    [TestCase(false, false)]
    [TestCase(true, false)]
    [TestCase(true, true)]
    public void GetSaveFile_filename_always_contains_screenshot_number(bool useFeatureDirs, bool useScenarioDirs)
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var options = new SaveOptions(new DirectoryInfo(Environment.CurrentDirectory),
                                    useFeatureDirs,
                                    useScenarioDirs);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Name, Does.Match(@"\b5\b"));
    }

    [Test]
    public void GetSaveFile_parent_directory_is_root_directory_when_saving_in_root_directory()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var rootDir = new DirectoryInfo(Environment.CurrentDirectory);
      var options = new SaveOptions(rootDir);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Directory, Is.EqualTo(rootDir));
    }

    [Test]
    public void GetSaveFile_parent_directory_is_named_after_feature_id_when_saving_in_feature_directories()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var rootDir = new DirectoryInfo(Environment.CurrentDirectory);
      var options = new SaveOptions(rootDir, useDirectoryForEachFeature: true);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Directory.Name, Is.EqualTo("MyFeatureId"));
    }

    [Test]
    public void GetSaveFile_parent_directory_is_named_after_scenario_id_when_saving_in_scenario_directories()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var rootDir = new DirectoryInfo(Environment.CurrentDirectory);
      var options = new SaveOptions(rootDir, useDirectoryForEachFeature: true, useDirectoryForEachScenario: true);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Directory.Name, Is.EqualTo("MyScenarioId"));
    }

    [Test]
    public void GetSaveFile_grandparent_directory_is_named_after_feature_id_when_saving_in_scenario_directories()
    {
      // Arrange
      var scenario = new Scenario(new IdAndName("MyFeatureId"),
                                  new IdAndName("MyScenarioId"),
                                  Mock.Of<IContainer>());
      var name = "MyScreenshotName";
      var counter = 5;
      var rootDir = new DirectoryInfo(Environment.CurrentDirectory);
      var options = new SaveOptions(rootDir, useDirectoryForEachFeature: true, useDirectoryForEachScenario: true);
      var sut = new SavePathProvider();

      // Act
      var result = sut.GetSaveFile(scenario, name, counter, options);

      // Assert
      Assert.That(result.Directory.Parent.Name, Is.EqualTo("MyFeatureId"));
    }
  }
}
