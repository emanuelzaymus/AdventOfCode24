namespace AdventOfCode24.Day05;

/// <summary>
/// https://adventofcode.com/2024/day/5
/// </summary>
public static class PagePrinter
{
    private static string Input => File.ReadAllText("Data/day05.txt");

    public static void RunTask1()
    {
        var sum = SumOfMiddlePagesInCorrectUpdates(Input);

        Console.WriteLine(sum); // 6260
    }

    public static int SumOfMiddlePagesInCorrectUpdates(string input)
    {
        var (rules, updates) = ParseRulesAndUpdates(input);

        var correctUpdates = FindUpdates(rules, updates, true);

        return correctUpdates
            .Sum(u =>
            {
                var middleIndex = u.Length / 2;
                return u[middleIndex];
            });
    }

    public static void RunTask2()
    {
        var sum = SumOfMiddlePagesInCorrectedIncorrectUpdates(Input);

        Console.WriteLine(sum); // 5346
    }

    public static int SumOfMiddlePagesInCorrectedIncorrectUpdates(string input)
    {
        var (rules, updates) = ParseRulesAndUpdates(input);

        var incorrectUpdates = FindUpdates(rules, updates, false);

        foreach (var incorrectUpdate in incorrectUpdates)
        {
            Array.Sort(incorrectUpdate, (first, second) => CompareBasedOnRules(rules, first, second));
        }

        return incorrectUpdates
            .Sum(u =>
            {
                var middleIndex = u.Length / 2;
                return u[middleIndex];
            });
    }

    private static List<int[]> FindUpdates(Dictionary<int, HashSet<int>> rules, int[][] updates, bool correct)
    {
        return updates
            .Where(update => correct ? IsCorrectUpdate(rules, update) : !IsCorrectUpdate(rules, update))
            .ToList();
    }

    private static bool IsCorrectUpdate(Dictionary<int, HashSet<int>> rules, int[] update)
    {
        for (var i = 0; i < update.Length; i++)
        {
            var currentPage = update[i];
            if (!rules.TryGetValue(currentPage, out var currentRules))
            {
                continue;
            }

            for (var j = 0; j < i; j++)
            {
                var precedingPage = update[j];
                if (currentRules.Contains(precedingPage))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static int CompareBasedOnRules(Dictionary<int, HashSet<int>> rules, int first, int second)
    {
        if (rules.TryGetValue(first, out var firstRules) && firstRules.Contains(second))
        {
            return -1;
        }

        if (rules.TryGetValue(second, out var secondRules) && secondRules.Contains(first))
        {
            return 1;
        }

        return 0;
    }

    private static (Dictionary<int, HashSet<int>> rules, int[][] updates) ParseRulesAndUpdates(string input)
    {
        var split = input.Split("\n\n");
        var rules = ParseRules(split[0]);
        var updates = ParseUpdates(split[1]);
        return (rules, updates);
    }

    private static Dictionary<int, HashSet<int>> ParseRules(string rulesString) =>
        rulesString
            .Split("\n")
            .Select(rule => rule.Split("|"))
            .Aggregate(new Dictionary<int, HashSet<int>>(), (rules, rulePair) =>
            {
                var first = int.Parse(rulePair[0]);
                var second = int.Parse(rulePair[1]);

                var success = rules.TryAdd(first, [second]);
                if (!success)
                {
                    rules[first].Add(second);
                }

                return rules;
            });

    private static int[][] ParseUpdates(string pagesString) =>
        pagesString
            .Split("\n")
            .Select(line => line.Split(',')
                .Select(int.Parse)
                .ToArray()
            )
            .ToArray();
}