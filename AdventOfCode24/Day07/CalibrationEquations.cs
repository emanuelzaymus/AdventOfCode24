namespace AdventOfCode24.Day07;

using Operation = Func<long, long, long>;

/// <summary>
/// https://adventofcode.com/2024/day/7
/// </summary>
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
        return SumOfPossibleCalibrationEquations(input, [Sum, Multiply]);
    }

    public static void RunTask2()
    {
        var sum = SumOfPossibleCalibrationEquationsWithConcatenation(Input);

        Console.WriteLine(sum); // 110365987435001
    }

    public static long SumOfPossibleCalibrationEquationsWithConcatenation(string input)
    {
        return SumOfPossibleCalibrationEquations(input, [Sum, Multiply, Concat]);
    }

    private static long SumOfPossibleCalibrationEquations(string input, Operation[] possibleOperations)
    {
        var equations = ParsEquations(input);

        long resultSum = 0;
        foreach (var equation in equations)
        {
            foreach (var operations in Combinations.AllCombinations(equation.Operands.Count - 1, possibleOperations))
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

                // ReSharper disable once InvertIf
                if (equationResult == equation.Result)
                {
                    resultSum += equationResult;
                    break;
                }
            }
        }

        return resultSum;
    }

    private static long Sum(long a, long b) => a + b;

    private static long Multiply(long a, long b) => a * b;

    private static long Concat(long a, long b)
    {
        var numberOfDigitsOfB = (long)Math.Floor(Math.Log10(b) + 1);
        var multiplicationCoefficientForA = (long)Math.Pow(10, numberOfDigitsOfB);
        return a * multiplicationCoefficientForA + b;
    }

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