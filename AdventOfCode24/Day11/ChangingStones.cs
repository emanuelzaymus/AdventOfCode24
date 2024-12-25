namespace AdventOfCode24.Day11;

/// <summary>
/// https://adventofcode.com/2024/day/11
/// </summary>
public static class ChangingStones
{
    private static string Input => File.ReadAllText("Data/day11.txt");

    private const int InvalidStoneNumber = -1;

    private static readonly int[][] StepsForSingleDigits =
    [
        /* 0 => */ [1], // (1 step)
        /* 1 => */ [2, 0, 2, 4], // (3 steps)
        /* 2 => */ [4, 0, 4, 8], // (3 steps)
        /* 3 => */ [6, 0, 7, 2], // (3 steps)
        /* 4 => */ [8, 0, 9, 6], // (3 steps)
        /* 5 => */ [2, 0, 4, 8, 2, 8, 8, 0], // (5 steps)
        /* 6 => */ [2, 4, 5, 7, 9, 4, 5, 6], // (5 steps)
        /* 7 => */ [2, 8, 6, 7, 6, 0, 3, 2], // (5 steps)
        /* 8 => */ [3, 2, 7, 7, 2, 6, 0, 8], // (5 steps)
        /* 9 => */ [3, 6, 8, 6, 9, 1, 8, 4], // (5 steps)
    ];

    private static readonly int[] NumberOfStepsForSingleDigits = [1, 3, 3, 3, 3, 5, 5, 5, 5, 5];

    public static void RunTask1()
    {
        var numberOfStones = NumberOfStonesAfterBlinks3(Input, 25);

        Console.WriteLine(numberOfStones); // 216996
    }

    public static void RunTask2()
    {
        var numberOfStones = NumberOfStonesAfterBlinks3(Input, 75);

        Console.WriteLine(numberOfStones);
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
        switch (stone)
        {
            case 0 when numberOfBlinksLeft > 1:
            case >= 1 and <= 4 when numberOfBlinksLeft > 3:
            case >= 5 and <= 9 when numberOfBlinksLeft > 5:
            {
                var sum = 0;
                for (var i = 0; i < StepsForSingleDigits[stone].Length; i++)
                {
                    sum += NumberOfStonesAfterBlinks3(StepsForSingleDigits[stone][i],
                        numberOfBlinksLeft - NumberOfStepsForSingleDigits[stone]);
                }

                return sum;
            }
        }

        var (firstStone, secondStone) = PerformBlink(stone);

        if (numberOfBlinksLeft == 1)
        {
            return secondStone == InvalidStoneNumber ? 1 : 2;
        }

        return NumberOfStonesAfterBlinks3(firstStone, numberOfBlinksLeft - 1)
               + (secondStone != InvalidStoneNumber
                   ? NumberOfStonesAfterBlinks3(secondStone, numberOfBlinksLeft - 1)
                   : 0);
    }

    private static LinkedList<long> ParseStones(string input)
    {
        return new LinkedList<long>(input
            .Split(' ')
            .Select(long.Parse));
    }

    private static (long first, long second) PerformBlink(long stone)
    {
        if (stone == 0)
        {
            return (1, InvalidStoneNumber);
        }

        int numberOfDigits;
        if ((numberOfDigits = NumberOfDigits(stone)) % 2 == 0)
        {
            var (firstHalf, secondHalf) = SplitNumber(stone, numberOfDigits);
            return (firstHalf, secondHalf);
        }

        return (stone * 2024, InvalidStoneNumber);
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