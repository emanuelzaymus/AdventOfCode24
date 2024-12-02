using AdventOfCode24.Day02;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day02;

[TestClass]
[TestSubject(typeof(ReportsLevelAnalysis))]
public class ReportsLevelAnalysisTest
{
    private const string ExampleInput = """
                                        7 6 4 2 1
                                        1 2 7 8 9
                                        9 7 6 2 1
                                        1 3 2 4 5
                                        8 6 4 4 1
                                        1 3 6 7 9
                                        """;

    [TestMethod]
    public void NumberOfSafeReports_ExampleInput_ShouldReturnCorrectResult()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReports(ExampleInput);

        Assert.AreEqual(2, numberOfSafeReports);
    }
}