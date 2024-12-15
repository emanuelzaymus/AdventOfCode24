using System.Collections.Generic;
using System.Linq;
using AdventOfCode24.Day09;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day09;

[TestClass]
[TestSubject(typeof(Disk))]
public class DiskTest
{
    [TestMethod]
    public void GetBlockSequencesFromBack_ExampleInput_ShouldReturnCorrectly()
    {
        var disk = new Disk(DiskOptimizationTest.ExampleInput);
        var blockSequences = disk.GetBlockSequencesFromBack().ToList();

        List<Disk.BlockSequence> expected =
        [
            new(9, 40, 2),
            new(8, 36, 4),
            new(7, 32, 3),
            new(6, 27, 4),
            new(5, 22, 4),
            new(4, 19, 2),
            new(3, 15, 3),
            new(2, 11, 1),
            new(1, 5, 3),
            new(0, 0, 2)
        ];

        CollectionAssert.AreEquivalent(expected, blockSequences);
    }
}