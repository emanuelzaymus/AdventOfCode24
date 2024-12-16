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

        return IsSafeReport(levels);
    }

    private static bool IsSafeReport(List<int> levels)
    {
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

    public static void RunTask2()
    {
        var numberOfSafeReportsWithProblemDampener = NumberOfSafeReportsWithProblemDampener(Input);

        Console.WriteLine(numberOfSafeReportsWithProblemDampener); // 674
    }

    public static int NumberOfSafeReportsWithProblemDampener(string input) =>
        input
            .Split('\n')
            .Count(IsSafeReportWithProblemDampener);

    private static bool IsSafeReportWithProblemDampener(string report)
    {
        var levels = report
            .Split(' ')
            .Select(int.Parse)
            .ToList();

        for (var i = 0; i < levels.Count; i++)
        {
            var levelsWithDumpedSingleElement = levels.ToList();
            levelsWithDumpedSingleElement.RemoveAt(i);

            if (IsSafeReport(levelsWithDumpedSingleElement))
            {
                return true;
            }
        }

        return false;
    }
}