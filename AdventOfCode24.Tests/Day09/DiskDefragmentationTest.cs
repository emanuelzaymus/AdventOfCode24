using AdventOfCode24.Day09;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day09;

[TestClass]
[TestSubject(typeof(DiskDefragmentation))]
public class DiskDefragmentationTest
{
    private const string ExampleInput = "2333133121414131402";

    [TestMethod]
    public void CalculateOptimizedDiskChecksum_ExampleInput_ShouldReturnCorrectResult()
    {
        var checksum = DiskDefragmentation.CalculateOptimizedDiskChecksum(ExampleInput);

        Assert.AreEqual(1928, checksum);
    }
}