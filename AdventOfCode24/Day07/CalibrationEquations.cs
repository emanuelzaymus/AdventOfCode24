using System.Collections;

namespace AdventOfCode24.Day07;

public static class CalibrationEquations
{
    private static string Input => File.ReadAllText("Data/day07.txt");

    public static void RunTask1()
    {
        var sum = SumOfPossibleCalibrationEquations(Input);

        Console.WriteLine(sum); // 663613490587
    }

    public static long SumOfPossibleCalibrationEquations(string input)
    {
        var equations = ParsEquations(input);

        long resultSum = 0;
        foreach (var equation in equations)
        {
            foreach (var operations in AllOperationCombinations(equation.Operands.Count))
            {
                var equationResult = equation.Operands[0];
                var i = 1;
                foreach (var operation in operations)
                {
                    equationResult = operation.Invoke(equationResult, equation.Operands[i]);
                    i++;
                    if (equationResult > equation.Result)
                    {
                        break;
                    }
                }

                if (equationResult == equation.Result)
                {
                    resultSum += equationResult;
                    break;
                }
            }
        }

        return resultSum;
    }

    private static IEnumerable<Func<long, long, long>[]> AllOperationCombinations(int numberOfOperands)
    {
        var numberOfOperations = numberOfOperands - 1;
        var result = new Func<long, long, long>[numberOfOperations];

        var allCombinationsCount = Math.Pow(2, numberOfOperations);
        for (var i = 0; i < allCombinationsCount; i++)
        {
            var bitArray = new BitArray([i]);
            for (var j = 0; j < numberOfOperations; j++)
            {
                result[j] = bitArray[j] ? Sum : Multiply;
            }

            yield return result;
        }
    }

    private static long Sum(long a, long b) => a + b;

    private static long Multiply(long a, long b) => a * b;

    private static List<Equation> ParsEquations(string input)
    {
        return input
            .Split(Environment.NewLine)
            .Select(row => row.Split(": "))
            .Select(pair =>
                new Equation(
                    long.Parse(pair[0]),
                    pair[1]
                        .Split(' ')
                        .Select(long.Parse)
                        .ToList()
                )
            )
            .ToList();
    }

    private record Equation(long Result, List<long> Operands);
}