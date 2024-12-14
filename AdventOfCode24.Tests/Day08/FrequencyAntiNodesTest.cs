using AdventOfCode24.Day08;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day08;

[TestClass]
[TestSubject(typeof(FrequencyAntiNodes))]
public class FrequencyAntiNodesTest
{
    private const string ExampleInput = """
                                        ............
                                        ........0...
                                        .....0......
                                        .......0....
                                        ....0.......
                                        ......A.....
                                        ............
                                        ............
                                        ........A...
                                        .........A..
                                        ............
                                        ............
                                        """;

    private const string ExampleInput2 = """
                                         T.........
                                         ...T......
                                         .T........
                                         ..........
                                         ..........
                                         ..........
                                         ..........
                                         ..........
                                         ..........
                                         ..........
                                         """;

    [TestMethod]
    public void CountFrequencyAntiNodes_ExampleInput_ShouldReturnCorrectResult()
    {
        var count = FrequencyAntiNodes.CountFrequencyAntiNodes(ExampleInput);

        Assert.AreEqual(14, count);
    }

    [TestMethod]
    public void CountFrequencyAntiNodesWithLines_ExampleInput_ShouldReturnCorrectResult()
    {
        var count = FrequencyAntiNodes.CountFrequencyAntiNodesWithLines(ExampleInput);

        Assert.AreEqual(34, count);
    }
    
    
    [TestMethod]
    public void CountFrequencyAntiNodesWithLines_ExampleInput2_ShouldReturnCorrectResult()
    {
        var count = FrequencyAntiNodes.CountFrequencyAntiNodesWithLines(ExampleInput2);

        Assert.AreEqual(9, count);
    }
}