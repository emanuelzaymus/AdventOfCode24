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

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_ExampleInput_ShouldReturnCorrectResult()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener(ExampleInput);

        Assert.AreEqual(4, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_ValidRow_ShouldReturn1()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 2 3 4 5");

        Assert.AreEqual(1, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_FirstNumberInvalid_ShouldReturn1()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("10 2 3 4 5");

        Assert.AreEqual(1, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_SecondNumberInvalid_ShouldReturn1()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 10 3 4 5");

        Assert.AreEqual(1, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_ThirdNumberInvalid_ShouldReturn1()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 2 10 4 5");

        Assert.AreEqual(1, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_LastButOneNumberInvalid_ShouldReturn1()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 2 3 10 5");

        Assert.AreEqual(1, numberOfSafeReports);
    }
    
    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_LastNumberInvalid_ShouldReturn1()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 2 3 4 10");

        Assert.AreEqual(1, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_Jump_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 2 3 10 11 12");

        Assert.AreEqual(0, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_FirstTwoNumbersInvalid_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("10 9 3 4 5");

        Assert.AreEqual(0, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_SecondTwoNumbersInvalid_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 10 9 2 3 4 5");

        Assert.AreEqual(0, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_ThirdTwoNumbersInvalid_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("0 1 10 9 2 3 4 5");

        Assert.AreEqual(0, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_LastButOneTwoNumbersInvalid_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("0 1 2 10 9 5");

        Assert.AreEqual(0, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_LastTwoNumbersInvalid_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("0 1 2 10 9");

        Assert.AreEqual(0, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_TwoNumbersInvalid_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("10 1 9 3 4 5");

        Assert.AreEqual(0, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_TwoDifferentNumbersInvalid_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 10 2 9 3 4 5");

        Assert.AreEqual(0, numberOfSafeReports);
    }

    [TestMethod]
    public void NumberOfSafeReportsWithProblemDampener_TwoDifferentNumbersInvalidFromEnd_ShouldReturn0()
    {
        var numberOfSafeReports = ReportsLevelAnalysis.NumberOfSafeReportsWithProblemDampener("1 2 3 10 4 9");

        Assert.AreEqual(0, numberOfSafeReports);
    }
}