using AdventOfCode24.Day07;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day07;

[TestClass]
[TestSubject(typeof(CalibrationEquations))]
public class CalibrationEquationsTest
{
    private const string ExampleInput = """
                                        190: 10 19
                                        3267: 81 40 27
                                        83: 17 5
                                        156: 15 6
                                        7290: 6 8 6 15
                                        161011: 16 10 13
                                        192: 17 8 14
                                        21037: 9 7 18 13
                                        292: 11 6 16 20
                                        """;

    [TestMethod]
    public void SumOfPossibleCalibrationEquations_ExampleInput_ShouldReturnCorrectResult()
    {
        var sum = CalibrationEquations.SumOfPossibleCalibrationEquations(ExampleInput);

        Assert.AreEqual(3749, sum);
    }
}