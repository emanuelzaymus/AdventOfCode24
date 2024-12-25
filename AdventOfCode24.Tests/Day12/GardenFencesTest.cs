using AdventOfCode24.Day12;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day12;

[TestClass]
[TestSubject(typeof(GardenFences))]
public class GardenFencesTest
{
    private const string ExampleInput1 = """
                                         AAAA
                                         BBCD
                                         BBCC
                                         EEEC
                                         """;

    private const string ExampleInput2 = """
                                         OOOOO
                                         OXOXO
                                         OOOOO
                                         OXOXO
                                         OOOOO
                                         """;

    private const string ExampleInput3 = """
                                         RRRRIICCFF
                                         RRRRIICCCF
                                         VVRRRCCFFF
                                         VVRCCCJFFF
                                         VVVVCJJCFE
                                         VVIVCCJJEE
                                         VVIIICJJEE
                                         MIIIIIJJEE
                                         MIIISIJEEE
                                         MMMISSJEEE
                                         """;

    private const string ExampleInput4 = """
                                         EEEEE
                                         EXXXX
                                         EEEEE
                                         EXXXX
                                         EEEEE
                                         """;

    private const string ExampleInput5 = """
                                         AAAAAA
                                         AAABBA
                                         AAABBA
                                         ABBAAA
                                         ABBAAA
                                         AAAAAA
                                         """;


    [TestMethod]
    [DataRow(ExampleInput1, 140, DisplayName = "Example 1")]
    [DataRow(ExampleInput2, 772, DisplayName = "Example 2")]
    [DataRow(ExampleInput3, 1930, DisplayName = "Example 3")]
    public void CalculatePriceOfFencesForAllRegions_ExampleInput_ShouldReturnCorrectResult(string input, int expected)
    {
        var price = GardenFences.CalculatePriceOfFencesForAllRegions(input);

        Assert.AreEqual(expected, price);
    }

    [TestMethod]
    [DataRow(ExampleInput1, 80, DisplayName = "Example 1")]
    [DataRow(ExampleInput2, 436, DisplayName = "Example 2")]
    [DataRow(ExampleInput3, 1206, DisplayName = "Example 3")]
    [DataRow(ExampleInput4, 236, DisplayName = "Example 4")]
    [DataRow(ExampleInput5, 368, DisplayName = "Example 5")]
    public void CalculatePriceOfFencesForAllRegionsWithDiscount_ExampleInput_ShouldReturnCorrectResult(
        string input, int expected)
    {
        var price = GardenFences.CalculatePriceOfFencesForAllRegions(input, withDiscount: true);

        Assert.AreEqual(expected, price);
    }
}