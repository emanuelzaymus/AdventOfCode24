using AdventOfCode24.Day01;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day01;

[TestClass]
[TestSubject(typeof(LocationIds))]
public class LocationIdsTest
{
    private const string ExampleInput = """
                                        3   4
                                        4   3
                                        2   5
                                        1   3
                                        3   9
                                        3   3
                                        """;

    [TestMethod]
    public void CalculateDistanceSum_ExampleInput_ShouldReturnCorrectResult()
    {
        var calculateDistanceSum = LocationIds.CalculateDistanceSum(ExampleInput);

        Assert.AreEqual(11, calculateDistanceSum);
    }
}