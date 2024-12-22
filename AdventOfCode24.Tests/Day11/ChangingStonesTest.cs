using AdventOfCode24.Day11;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day11;

[TestClass]
[TestSubject(typeof(ChangingStones))]
public class ChangingStonesTest
{
    private const string ExampleInput = "125 17";

    [TestMethod]
    [DataRow(1, 3)]
    [DataRow(2, 4)]
    [DataRow(3, 5)]
    [DataRow(4, 9)]
    [DataRow(5, 13)]
    [DataRow(6, 22)]
    [DataRow(25, 55312)]
    public void NumberOfStonesAfterBlinks_ExampleInput_ShouldReturnCorrectResult(
        int numberOfBlinks, int expectedNumberOfStones)
    {
        var numberOfStones = ChangingStones.NumberOfStonesAfterBlinks3(ExampleInput, numberOfBlinks);

        Assert.AreEqual(expectedNumberOfStones, numberOfStones);
    }

    [TestMethod]
    public void NumberOfStonesAfterBlinks_CustomInput_ShouldReturnCorrectResult()
    {
        var numberOfStones = ChangingStones.NumberOfStonesAfterBlinks3("1", 30);

        Assert.AreEqual(234511, numberOfStones);
    }

    [TestMethod]
    [DataRow(1, 1)]
    [DataRow(22, 2)]
    [DataRow(333, 3)]
    [DataRow(4444, 4)]
    [DataRow(55555, 5)]
    [DataRow(666666, 6)]
    [DataRow(7777777, 7)]
    [DataRow(123456789012345, 15)]
    public void NumberOfDigits(long number, int expectedNumberOfDigits)
    {
        var numberOfDigits = ChangingStones.NumberOfDigits(number);

        Assert.AreEqual(expectedNumberOfDigits, numberOfDigits);
    }
}