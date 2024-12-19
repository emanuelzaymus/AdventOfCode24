namespace AdventOfCode24.Day11;

public static class ChangingStones
{
    private static string Input => File.ReadAllText("Data/day11.txt");

    public static void RunTask1()
    {
        var numberOfStones = NumberOfStonesAfterBlinks(Input, 25);

        Console.WriteLine(numberOfStones); // 216996
    }

    public static void RunTask2()
    {
        var numberOfStones = NumberOfStonesAfterBlinks(Input, 75);

        Console.WriteLine(numberOfStones); //
    }

    public static int NumberOfStonesAfterBlinks(string input, int numberOfBlinks)
    {
        var stones = ParseStones(input);

        for (var i = 0; i < numberOfBlinks; i++)
        {
            Console.WriteLine($"Iteration: {i}");

            var currentNode = stones.First ?? throw new Exception("No stone found");

            var currentCountOfStones = stones.Count;
            for (var j = 0; j < currentCountOfStones; j++)
            {
                if (j != 0)
                {
                    currentNode = currentNode.Next ?? throw new Exception("No more stones found");
                }

                if (currentNode.Value == 0)
                {
                    currentNode.Value = 1;
                    continue;
                }

                var numberOfDigits = NumberOfDigits(currentNode.Value);
                if (numberOfDigits % 2 == 0)
                {
                    var (firstHalf, secondHalf) = SplitNumber(currentNode.Value, numberOfDigits);
                    currentNode.Value = firstHalf;
                    stones.AddLast(secondHalf);
                    continue;
                }

                currentNode.Value *= 2024;
            }
        }

        return stones.Count;
    }

    private static LinkedList<long> ParseStones(string input)
    {
        return new LinkedList<long>(input
            .Split(' ')
            .Select(long.Parse));
    }

    public static int NumberOfDigits(long number) => (int)Math.Log10(number) + 1;

    private static (long firstHalf, long secondHalf) SplitNumber(long number, int numberOfDigits)
    {
        var splittingCoefficient = CalculateSplittingCoefficient(numberOfDigits);
        var firstHalf = GetFirstHalfOfNumber(number, splittingCoefficient);
        var secondHalf = GetSecondHalfOfNumber(number, firstHalf, splittingCoefficient);
        return (firstHalf, secondHalf);
    }

    private static long CalculateSplittingCoefficient(long numberOfDigits)
    {
        var halfOfDigits = numberOfDigits / 2;
        return (long)Math.Pow(10, halfOfDigits);
    }

    private static long GetFirstHalfOfNumber(long number, long splittingCoefficient) => number / splittingCoefficient;

    private static long GetSecondHalfOfNumber(long number, long firstHalfOfNumber, long splittingCoefficient)
    {
        return number - firstHalfOfNumber * splittingCoefficient;
    }
}