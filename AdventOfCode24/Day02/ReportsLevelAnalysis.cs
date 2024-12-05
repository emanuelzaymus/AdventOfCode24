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

    public static void RunTask2()
    {
        var numberOfSafeReportsWithProblemDampener = NumberOfSafeReportsWithProblemDampener(Input);

        Console.WriteLine(numberOfSafeReportsWithProblemDampener); // 672 is too low AND 679 is too high
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

        return IsGraduallyIncreasingWithProblemDampener(levels.ToList())
               || IsGraduallyDecreasingWithProblemDampener(levels.ToList());
    }

    private static bool IsGraduallyIncreasingWithProblemDampener(List<int> levels) =>
        AllHaveGradualOffsetWithProblemDampener(levels, (first, second) => second - first);

    private static bool IsGraduallyDecreasingWithProblemDampener(List<int> levels) =>
        AllHaveGradualOffsetWithProblemDampener(levels, (first, second) => first - second);

    private static bool AllHaveGradualOffsetWithProblemDampener(List<int> levels, Func<int, int, int> calculateOffset)
    {
        var wasOneWrong = false;

        for (var i = 0; i < levels.Count - 2; i++)
        {
            var first = levels[i];
            var second = levels[i + 1];
            var third = levels[i + 2];

            var isGradualDifference1 = IsGradualDifference(first, second, calculateOffset);
            var isGradualDifference2 = IsGradualDifference(second, third, calculateOffset);

            if (isGradualDifference1 && isGradualDifference2) continue;

            if (i != levels.Count - 3 && isGradualDifference1 && !isGradualDifference2) continue;

            if (wasOneWrong) return false;
            wasOneWrong = true;

            if (i == 0 && isGradualDifference2) continue;

            if (i == levels.Count - 3 && isGradualDifference1) continue;

            var isGradualDifference3 = IsGradualDifference(first, third, calculateOffset);

            if (isGradualDifference3)
            {
                levels.RemoveAt(i + 1);
                i--;
                continue;
            }

            return false;
        }

        return true;
    }

    private static bool IsGradualDifference(int first, int second, Func<int, int, int> calculateOffset) =>
        calculateOffset(first, second) is >= 1 and <= 3;
}