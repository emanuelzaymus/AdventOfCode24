namespace AdventOfCode24.Day02;

/// <summary>
/// https://adventofcode.com/2024/day/2
/// </summary>
public static class ReportsLevelAnalysis
{
    private static string Input => File.ReadAllText("Data/day02.txt");

    public static void RunTask1()
    {
        var numberOfSafeReports = NumberOfSafeReports(Input);

        Console.WriteLine(numberOfSafeReports); // 639
    }

    public static int NumberOfSafeReports(string input) =>
        input
            .Split('\n')
            .Count(IsSafeReport);

    private static bool IsSafeReport(string report)
    {
        var levels = report
            .Split(' ')
            .Select(int.Parse)
            .ToList();

        return IsGraduallyIncreasing(levels) || IsGraduallyDecreasing(levels);
    }

    private static bool IsGraduallyIncreasing(List<int> levels) =>
        AllHaveGradualOffset(levels, (first, second) => second - first);

    private static bool IsGraduallyDecreasing(List<int> levels) =>
        AllHaveGradualOffset(levels, (first, second) => first - second);

    private static bool AllHaveGradualOffset(List<int> levels, Func<int, int, int> calculateOffset)
    {
        var first = levels.Take(..^1);
        var second = levels.Take(1..);

        return first.Zip(second)
            .All(pair =>
            {
                var offset = calculateOffset(pair.First, pair.Second);
                return offset is >= 1 and <= 3;
            });
    }
}