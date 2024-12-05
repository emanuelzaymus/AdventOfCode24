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

            var firstNumberStr = input.AsSpan(firstNumberStartIndex, commaIndex - firstNumberStartIndex);
            if (!int.TryParse(firstNumberStr, out var firstNumber))
            {
                continue;
            }

            var secondNumberStartIndex = commaIndex + 1;
            var endParenthesesIndex = input.IndexOf(')', secondNumberStartIndex);

            var secondNumberStr = input.AsSpan(secondNumberStartIndex, endParenthesesIndex - secondNumberStartIndex);
            if (!int.TryParse(secondNumberStr, out var secondNumber))
            {
                continue;
            }

            multiplicationInstructions.Add((firstNumber, secondNumber));
        }

        return multiplicationInstructions
            .Select(pair => (long)pair.Item1 * pair.Item2)
            .Sum();
    }

    public static void RunTask2()
    {
        var multiplicationSumWithDisabling = MultiplicationSumWithDisabling(Input);

        Console.WriteLine(multiplicationSumWithDisabling); // 72948684
    }

    public static long MultiplicationSumWithDisabling(string input)
    {
        var multiplicationInstructions = new List<(bool enabled, int firstNumber, int secondNumber)>();

        var instructionsEnabled = true;
        var startIndex = 0;

        (Instruction instruction, int nextInstructionStartIndex)? nextInstruction;
        while ((nextInstruction = FindNextInstruction(input, startIndex)) != null)
        {
            startIndex = nextInstruction.Value.nextInstructionStartIndex;

            switch (nextInstruction.Value.instruction)
            {
                case Instruction.Multiplication:
                {
                    var multiplicationNumbers = ExtractMultiplicationNumbers(input, startIndex);

                    if (multiplicationNumbers != null)
                    {
                        var (firstNumber, secondNumber) = multiplicationNumbers.Value;
                        multiplicationInstructions.Add((instructionsEnabled, firstNumber, secondNumber));
                    }

                    break;
                }
                case Instruction.Do:
                {
                    instructionsEnabled = true;
                    break;
                }
                case Instruction.Dont:
                {
                    instructionsEnabled = false;
                    break;
                }
                default: throw new Exception("Unknown instruction");
            }
        }

        return multiplicationInstructions
            .Where(pair => pair.enabled)
            .Select(pair => (long)pair.firstNumber * pair.secondNumber)
            .Sum();
    }

    private static (Instruction instruction, int nextInstructionStartIndex)? FindNextInstruction(string input,
        int startIndex)
    {
        var multiplicationIndex = input.IndexOf("mul(", startIndex, StringComparison.Ordinal);
        multiplicationIndex = multiplicationIndex != -1 ? multiplicationIndex : int.MaxValue;

        var doIndex = input.IndexOf("do()", startIndex, StringComparison.Ordinal);
        doIndex = doIndex != -1 ? doIndex : int.MaxValue;

        var dontIndex = input.IndexOf("don't()", startIndex, StringComparison.Ordinal);
        dontIndex = dontIndex != -1 ? dontIndex : int.MaxValue;

        if (multiplicationIndex < doIndex && multiplicationIndex < dontIndex)
        {
            return (Instruction.Multiplication, multiplicationIndex + 4);
        }

        if (doIndex < multiplicationIndex && doIndex < dontIndex)
        {
            return (Instruction.Do, doIndex + 4);
        }

        if (dontIndex < multiplicationIndex && dontIndex < doIndex)
        {
            return (Instruction.Dont, dontIndex + 7);
        }

        return null;
    }

    private static (int firstNumber, int secondNumber)? ExtractMultiplicationNumbers(string input,
        int firstNumberStartIndex)
    {
        var commaIndex = input.IndexOf(',', firstNumberStartIndex);

        var firstNumberStr = input.AsSpan(firstNumberStartIndex, commaIndex - firstNumberStartIndex);
        if (!int.TryParse(firstNumberStr, out var firstNumber))
        {
            return null;
        }

        var secondNumberStartIndex = commaIndex + 1;
        var endParenthesesIndex = input.IndexOf(')', secondNumberStartIndex);

        var secondNumberStr = input.AsSpan(secondNumberStartIndex, endParenthesesIndex - secondNumberStartIndex);
        if (!int.TryParse(secondNumberStr, out var secondNumber))
        {
            return null;
        }

        return (firstNumber, secondNumber);
    }

    private enum Instruction
    {
        Multiplication,
        Do,
        Dont
    }
}