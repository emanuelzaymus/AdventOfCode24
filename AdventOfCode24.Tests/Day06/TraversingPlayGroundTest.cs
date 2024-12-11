using AdventOfCode24.Day06;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day06;

[TestClass]
[TestSubject(typeof(TraversingPlayGround))]
public class TraversingPlayGroundTest
{
    private const string ExampleInput = """
                                        ....#.....
                                        .........#
                                        ..........
                                        ..#.......
                                        .......#..
                                        ..........
                                        .#..^.....
                                        ........#.
                                        #.........
                                        ......#...
                                        """;

    [TestMethod]
    public void CountVisitedPositions_ExampleInput_ShouldReturnCorrectResult()
    {
        var visitedPositions = TraversingPlayGround.CountVisitedPositions(ExampleInput);

        Assert.AreEqual(41, visitedPositions);
    }
}