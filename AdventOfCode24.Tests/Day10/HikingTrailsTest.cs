using AdventOfCode24.Day10;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day10;

[TestClass]
[TestSubject(typeof(HikingTrails))]
public class HikingTrailsTest
{
    private const string ExampleInput = """
                                        0123
                                        1234
                                        8765
                                        9876
                                        """;

    private const string ExampleInput2 = """
                                         89010123
                                         78121874
                                         87430965
                                         96549874
                                         45678903
                                         32019012
                                         01329801
                                         10456732
                                         """;

    [TestMethod]
    public void CountOfHikingTrails_ExampleInput_ShouldReturnCorrectResult()
    {
        var count = HikingTrails.CountOfHikingTrails(ExampleInput);

        Assert.AreEqual(1, count);
    }

    [TestMethod]
    public void CountOfHikingTrails_ExampleInput2_ShouldReturnCorrectResult()
    {
        var count = HikingTrails.CountOfHikingTrails(ExampleInput2);

        Assert.AreEqual(36, count);
    }

    [TestMethod]
    public void CountOfHikingTrails_ExampleInput2AndDoNotCountDistinctTrails_ShouldReturnCorrectResult()
    {
        var count = HikingTrails.CountOfHikingTrails(ExampleInput2, countDistinctTrails: false);

        Assert.AreEqual(81, count);
    }
}