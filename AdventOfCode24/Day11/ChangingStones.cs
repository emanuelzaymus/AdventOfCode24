namespace AdventOfCode24.Day11;

public static class ChangingStones
{
    private static string Input => File.ReadAllText("Data/day11.txt");

    public static void RunTask1()
    {
        var numberOfStones = NumberOfStonesAfterBlinks3(Input, 25);

        Console.WriteLine(numberOfStones); // 216996
    }

    public static void RunTask2()
    {
        var numberOfStones = NumberOfStonesAfterBlinks3(Input, 75);

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

    public static int NumberOfStonesAfterBlinks2(string input, int numberOfBlinks)
    {
        return NumberOfStonesAfterBlinks2(ParseStones(input), numberOfBlinks);
    }

    private static int NumberOfStonesAfterBlinks2(LinkedList<long> stones, int numberOfBlinksLeft)
    {
        if (numberOfBlinksLeft == 0)
        {
            return stones.Count;
        }

        var numberOfStones = 0;
        foreach (var s in stones)
        {
            var currentStones = new LinkedList<long>();
            currentStones.AddFirst(s);

            var currentNode = currentStones.First ?? throw new Exception("No stone found");
            int numberOfDigits;

            if (currentNode.Value == 0)
            {
                currentNode.Value = 1;
            }
            else if ((numberOfDigits = NumberOfDigits(currentNode.Value)) % 2 == 0)
            {
                var (firstHalf, secondHalf) = SplitNumber(currentNode.Value, numberOfDigits);
                currentNode.Value = firstHalf;
                currentStones.AddAfter(currentNode, secondHalf);
            }
            else
            {
                currentNode.Value *= 2024;
            }

            numberOfStones +=
                NumberOfStonesAfterBlinks2(currentStones, numberOfBlinksLeft - 1);
        }

        return numberOfStones;
    }

    public static int NumberOfStonesAfterBlinks3(string input, int numberOfBlinks)
    {
        Console.WriteLine($"numberOfBlinks: {numberOfBlinks}");
        var stones = ParseStones(input);
        return stones.Sum(s =>
        {
            var numberOfStonesAfterBlinks3 = NumberOfStonesAfterBlinks3(s, numberOfBlinks);
            Console.WriteLine($"stone: {s} numberOfStonesAfterBlinks3: {numberOfStonesAfterBlinks3}");
            return numberOfStonesAfterBlinks3;
        });
    }

    private static int NumberOfStonesAfterBlinks3(long stone, int numberOfBlinksLeft)
    {
        int numberOfDigits;
        long secondStone = -1;

        if (stone == 0)
        {
            stone = 1;
        }
        else if ((numberOfDigits = NumberOfDigits(stone)) % 2 == 0)
        {
            var (firstHalf, secondHalf) = SplitNumber(stone, numberOfDigits);
            stone = firstHalf;
            secondStone = secondHalf;
        }
        else
        {
            stone *= 2024;
        }

        if (numberOfBlinksLeft == 1)
        {
            return secondStone == -1 ? 1 : 2;
        }

        return NumberOfStonesAfterBlinks3(stone, numberOfBlinksLeft - 1)
               + (secondStone != -1
                   ? NumberOfStonesAfterBlinks3(secondStone, numberOfBlinksLeft - 1)
                   : 0);
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