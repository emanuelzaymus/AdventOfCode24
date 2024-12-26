namespace AdventOfCode24.Day11;

/// <summary>
/// https://adventofcode.com/2024/day/11
/// </summary>
public static class ChangingStones
{
    private const long InvalidStoneNumber = -1;

    private static string Input => File.ReadAllText("Data/day11.txt");

    private static readonly Dictionary<(long stone, int numberOfBlinksLeft), long> Cache = new();

    public static void RunTask1()
    {
        var numberOfStones = NumberOfStonesAfterBlinks(Input, 25);

        Console.WriteLine(numberOfStones); // 216996
    }

    public static void RunTask2()
    {
        var numberOfStones = NumberOfStonesAfterBlinks(Input, 75);

        Console.WriteLine(numberOfStones); // 257335372288947
    }

    public static long NumberOfStonesAfterBlinks(string input, int numberOfBlinks)
    {
        return ParseStones(input)
            .Sum(stone => NumberOfStonesAfterBlinks(stone, numberOfBlinks));
    }

    private static List<long> ParseStones(string input)
    {
        return input
            .Split(' ')
            .Select(long.Parse)
            .ToList();
    }

    private static long NumberOfStonesAfterBlinks(long stone, int numberOfBlinksLeft)
    {
        if (numberOfBlinksLeft == 0)
        {
            return 1;
        }

        if (Cache.TryGetValue((stone, numberOfBlinksLeft), out var foundNumberOfStonesAfterBlinks))
        {
            return foundNumberOfStonesAfterBlinks;
        }

        var (firstStone, secondStone) = PerformBlink(stone);

        var result = NumberOfStonesAfterBlinks(firstStone, numberOfBlinksLeft - 1);

        result += secondStone != InvalidStoneNumber
            ? NumberOfStonesAfterBlinks(secondStone, numberOfBlinksLeft - 1)
            : 0;

        Cache.Add((stone, numberOfBlinksLeft), result);

        return result;
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
        var secondHalf = GetSecondHalfOfNumber(number, splittingCoefficient);
        return (firstHalf, secondHalf);
    }

    private static long CalculateSplittingCoefficient(long numberOfDigits) => (long)Math.Pow(10, numberOfDigits / 2.0);

    private static long GetFirstHalfOfNumber(long number, long splittingCoefficient) => number / splittingCoefficient;

    private static long GetSecondHalfOfNumber(long number, long splittingCoefficient) => number % splittingCoefficient;
}