namespace AdventOfCode24.Day3;

public static class MultiplicationExtractor
{
    private static string Input => File.ReadAllText("Data/day03.txt");

    public static void RunTask1()
    {
        var multiplicationSum = MultiplicationSum(Input);

        Console.WriteLine(multiplicationSum); // 166905464
    }

    public static long MultiplicationSum(string input)
    {
        var multiplicationInstructions = new List<(int, int)>();

        var startIndex = 0;
        int mulWordIndex;
        while ((mulWordIndex = input.IndexOf("mul(", startIndex, StringComparison.Ordinal)) != -1)
        {
            startIndex = mulWordIndex + 1;

            var firstNumberStartIndex = mulWordIndex + 4;
            var commaIndex = input.IndexOf(',', firstNumberStartIndex);

            if (!int.TryParse(input.AsSpan(firstNumberStartIndex, commaIndex - firstNumberStartIndex),
                    out var firstNumber))
            {
                continue;
            }

            var secondNumberStartIndex = commaIndex + 1;
            var endParenthesesIndex = input.IndexOf(')', secondNumberStartIndex);

            if (!int.TryParse(input.AsSpan(secondNumberStartIndex, endParenthesesIndex - secondNumberStartIndex),
                    out var secondNumber))
            {
                continue;
            }

            multiplicationInstructions.Add((firstNumber, secondNumber));
        }

        return multiplicationInstructions
            .Select(pair => (long)pair.Item1 * pair.Item2)
            .Sum();
    }
}