using System.Collections.Generic;
using System.Linq;
using AdventOfCode24.Day07;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day07;

[TestClass]
[TestSubject(typeof(Combinations))]
public class CombinationsTest
{
    [TestMethod]
    public void AllCombinations_CombinationSize2AndElements2_ShouldReturnCorrectly()
    {
        var combinations = Combinations.AllCombinations(2, [0, 1]);

        List<int[]> expected =
        [
            [0, 0],
            [0, 1],

            [1, 0],
            [1, 1]
        ];

        AreEquivalent(expected, combinations);
    }

    [TestMethod]
    public void AllCombinations_CombinationSize3AndElements2_ShouldReturnCorrectly()
    {
        var combinations = Combinations.AllCombinations(3, [0, 1]);

        List<int[]> expected =
        [
            [0, 0, 0],
            [0, 0, 1],
            [0, 1, 0],
            [0, 1, 1],

            [1, 0, 0],
            [1, 0, 1],
            [1, 1, 0],
            [1, 1, 1],
        ];

        AreEquivalent(expected, combinations);
    }

    [TestMethod]
    public void AllCombinations_CombinationSize2AndElements3_ShouldReturnCorrectly()
    {
        var combinations = Combinations.AllCombinations(2, [0, 1, 2]);

        List<int[]> expected =
        [
            [0, 0],
            [0, 1],
            [0, 2],

            [1, 0],
            [1, 1],
            [1, 2],

            [2, 0],
            [2, 1],
            [2, 2]
        ];

        AreEquivalent(expected, combinations);
    }

    [TestMethod]
    public void AllCombinations_CombinationSize3AndElements3_ShouldReturnCorrectly()
    {
        var combinations = Combinations.AllCombinations(3, [0, 1, 2]);

        List<int[]> expected =
        [
            [0, 0, 0],
            [0, 0, 1],
            [0, 0, 2],
            [0, 1, 0],
            [0, 1, 1],
            [0, 1, 2],
            [0, 2, 0],
            [0, 2, 1],
            [0, 2, 2],

            [1, 0, 0],
            [1, 0, 1],
            [1, 0, 2],
            [1, 1, 0],
            [1, 1, 1],
            [1, 1, 2],
            [1, 2, 0],
            [1, 2, 1],
            [1, 2, 2],

            [2, 0, 0],
            [2, 0, 1],
            [2, 0, 2],
            [2, 1, 0],
            [2, 1, 1],
            [2, 1, 2],
            [2, 2, 0],
            [2, 2, 1],
            [2, 2, 2]
        ];

        AreEquivalent(expected, combinations);
    }

    private static void AreEquivalent(List<int[]> expected, IEnumerable<int[]> actual)
    {
        var i = 0;
        foreach (var a in actual)
        {
            CollectionAssert.AreEqual(expected[i], a);
            i++;
        }

        Assert.AreEqual(expected.Count, i);
    }
}