using AdventOfCode24.Common;

namespace AdventOfCode24.Day01;

/// <summary>
/// https://adventofcode.com/2024/day/1
/// </summary>
public static class LocationIds
{
    private static string Input => File.ReadAllText("Data/day01.txt");

    public static void RunTask1()
    {
        var result = CalculateDistanceSum(Input);

        Console.WriteLine(result); // 2285373
    }

    public static int CalculateDistanceSum(string input)
    {
        var firstColumnNumber = GetColumnNumberList(input, strings => strings.First());

        var secondColumnNumber = GetColumnNumberList(input, strings => strings.Last());

        return firstColumnNumber.Zip(secondColumnNumber)
            .Select(pair => Math.Abs(pair.First - pair.Second))
            .Sum();
    }

    private static List<int> GetColumnNumberList(string input, Func<string[], string> numberExtractor)
    {
        return input
            .SplitLines()
            .Select(line => line.Split("   "))
            .Select(numberExtractor)
            .Select(int.Parse)
            .Order()
            .ToList();
    }

    public static void RunTask2()
    {
        var result = CalculateSimilarityScore(Input);

        Console.WriteLine(result); // 21142653
    }

    public static int CalculateSimilarityScore(string input)
    {
        var firstColumnNumber = GetColumnNumberList(input, strings => strings.First());

        var secondColumnNumber = GetColumnNumberList(input, strings => strings.Last());

        return firstColumnNumber
            .Select(number => (number, secondColumnNumber.Count(n => n == number)))
            .Select(pair => pair.Item1 * pair.Item2)
            .Sum();
    }
}