namespace AdventOfCode24.Day07;

using Operation = Func<long, long, long>;

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
        return SumOfPossibleCalibrationEquations(input, 2);
    }

    public static void RunTask2()
    {
        var sum = SumOfPossibleCalibrationEquationsWithConcatenation(Input);

        Console.WriteLine(sum); // 
    }

    public static long SumOfPossibleCalibrationEquationsWithConcatenation(string input)
    {
        return SumOfPossibleCalibrationEquations(input, 3);
    }

    private static long SumOfPossibleCalibrationEquations(string input, int operationsCount)
    {
        var equations = ParsEquations(input);

        long resultSum = 0;
        foreach (var equation in equations)
        {
            foreach (var operations in AllOperationCombinations(equation.Operands.Count, operationsCount))
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

    private static List<Operation> AllOperations = [Sum, Multiply, Cancat];

    private static IEnumerable<Operation[]> AllOperationCombinations(int numberOfOperands, int operationsCount)
    {
        var numberOfOperations = numberOfOperands - 1;
        var result = new Operation[numberOfOperations];

        var allCombinationsCount = Math.Pow(operationsCount, numberOfOperations);
        for (var i = 0; i < allCombinationsCount; i++)
        {
            var combination = Convert.ToString(i, operationsCount).PadLeft(numberOfOperations, '0');
            for (var j = 0; j < numberOfOperations; j++)
            {
                result[j] = combination[j] switch
                {
                    '0' => Sum,
                    '1' => Multiply,
                    '2' => Cancat,
                    _ => throw new ArgumentOutOfRangeException(nameof(operationsCount))
                };
            }

            yield return result;
        }
    }

    private static long Sum(long a, long b) => a + b;

    private static long Multiply(long a, long b) => a * b;

    private static long Cancat(long a, long b)
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