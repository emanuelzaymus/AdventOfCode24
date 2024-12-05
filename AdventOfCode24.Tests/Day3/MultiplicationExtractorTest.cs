using AdventOfCode24.Day3;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day3;

[TestClass]
[TestSubject(typeof(MultiplicationExtractor))]
public class MultiplicationExtractorTest
{
    private const string ExampleInput = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

    [TestMethod]
    public void MultiplicationSum_ExampleInput_ShouldReturnCorrectResult()
    {
        var multiplicationSum = MultiplicationExtractor.MultiplicationSum(ExampleInput);

        Assert.AreEqual(161, multiplicationSum);
    }
}