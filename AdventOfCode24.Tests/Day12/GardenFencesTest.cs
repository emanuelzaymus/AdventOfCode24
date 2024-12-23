using AdventOfCode24.Day12;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day12;

[TestClass]
[TestSubject(typeof(GardenFences))]
public class GardenFencesTest
{
    private const string ExampleInput = """
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

    [TestMethod]
    public void CalculatePriceOfFencesForAllRegions_ExampleInput_ShouldReturnCorrectResult()
    {
        var price = GardenFences.CalculatePriceOfFencesForAllRegions(ExampleInput);

        Assert.AreEqual(140, price);
    }

    [TestMethod]
    public void CalculatePriceOfFencesForAllRegions_ExampleInput2_ShouldReturnCorrectResult()
    {
        var price = GardenFences.CalculatePriceOfFencesForAllRegions(ExampleInput2);

        Assert.AreEqual(772, price);
    }

    [TestMethod]
    public void CalculatePriceOfFencesForAllRegions_ExampleInput3_ShouldReturnCorrectResult()
    {
        var price = GardenFences.CalculatePriceOfFencesForAllRegions(ExampleInput3);

        Assert.AreEqual(1930, price);
    }
}