using AdventOfCode24.Day09;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day09;

[TestClass]
[TestSubject(typeof(DiskOptimization))]
public class DiskOptimizationTest
{
    public const string ExampleInput = "2333133121414131402";

    [TestMethod]
    public void CalculateOptimizedDiskChecksum_ExampleInput_ShouldReturnCorrectResult()
    {
        var checksum = DiskOptimization.CalculateOptimizedDiskChecksum(ExampleInput);

        Assert.AreEqual(1928, checksum);
    }

    [TestMethod]
    public void CalculateOptimizedDiskChecksumWithoutFragmentation_ExampleInput_ShouldReturnCorrectResult()
    {
        var checksum = DiskOptimization.CalculateOptimizedDiskChecksum(ExampleInput, withFragmentation: false);

        Assert.AreEqual(2858, checksum);
    }
}