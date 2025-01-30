using AdventOfCode24.Day16;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day16;

[TestClass]
[TestSubject(typeof(CheapestPathSearch))]
public class CheapestPathSearchTest
{
    private const string ExampleInput1 = """
                                         ###############
                                         #.......#....E#
                                         #.#.###.#.###.#
                                         #.....#.#...#.#
                                         #.###.#####.#.#
                                         #.#.#.......#.#
                                         #.#.#####.###.#
                                         #...........#.#
                                         ###.#.#####.#.#
                                         #...#.....#.#.#
                                         #.#.#.###.#.#.#
                                         #.....#...#.#.#
                                         #.###.#.#.#.#.#
                                         #S..#.....#...#
                                         ###############
                                         """;

    private const string ExampleInput2 = """
                                         #################
                                         #...#...#...#..E#
                                         #.#.#.#.#.#.#.#.#
                                         #.#.#.#...#...#.#
                                         #.#.#.#.###.#.#.#
                                         #...#.#.#.....#.#
                                         #.#.#.#.#.#####.#
                                         #.#...#.#.#.....#
                                         #.#.#####.#.###.#
                                         #.#.#.......#...#
                                         #.#.###.#####.###
                                         #.#.#...#.....#.#
                                         #.#.#.#####.###.#
                                         #.#.#.........#.#
                                         #.#.#.#########.#
                                         #S#.............#
                                         #################
                                         """;

    [TestMethod]
    public void FindCheapestPathCost_ExampleInput1_ShouldReturnCorrectResult()
    {
        var cost = CheapestPathSearch.FindCheapestPathCost(ExampleInput1);

        Assert.AreEqual(7036, cost);
    }

    [TestMethod]
    public void FindCheapestPathCost_ExampleInput2_ShouldReturnCorrectResult()
    {
        var cost = CheapestPathSearch.FindCheapestPathCost(ExampleInput2);

        Assert.AreEqual(11048, cost);
    }
}