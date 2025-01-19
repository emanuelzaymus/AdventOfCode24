using AdventOfCode24.Day14;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day14;

[TestClass]
[TestSubject(typeof(MovingRobots))]
public class MovingRobotsTest
{
    private const string ExampleInput = """
                                        p=0,4 v=3,-3
                                        p=6,3 v=-1,-3
                                        p=10,3 v=-1,2
                                        p=2,0 v=2,-1
                                        p=0,0 v=1,3
                                        p=3,0 v=-2,-2
                                        p=7,6 v=-1,-3
                                        p=3,0 v=-1,-2
                                        p=9,3 v=2,3
                                        p=7,3 v=-1,2
                                        p=2,4 v=2,-3
                                        p=9,5 v=-3,-3
                                        """;

    [TestMethod]
    public void ProductOfRobotCountsInQuadrantsAfterMoves_ExampleInput_ShouldReturnCorrectResult()
    {
        var product = MovingRobots.ProductOfRobotCountsInQuadrantsAfterMoves(ExampleInput,
            7, 11, MovingRobots.NumberOfMoves);

        Assert.AreEqual(1 * 3 * 4 * 1, product);
    }

    [TestMethod]
    public void ProductOfRobotCountsInQuadrantsAfterMoves_SingleRobot_ShouldReturnCorrectResult()
    {
        var product = MovingRobots.ProductOfRobotCountsInQuadrantsAfterMoves("p=2,4 v=2,-3",
            7, 11, 5);

        Assert.AreEqual(0, product);
    }
}