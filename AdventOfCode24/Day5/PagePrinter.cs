namespace AdventOfCode24.Day5;

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
        var split = input.Split("\n\n");
        var rules = ParseRules(split[0]);
        var updates = ParseUpdates(split[1]);

        var correctUpdates = FindCorrectUpdates(rules, updates);

        return correctUpdates
            .Sum(u =>
            {
                var middleIndex = u.Length / 2;
                return u[middleIndex];
            });
    }

    private static List<int[]> FindCorrectUpdates(Dictionary<int, HashSet<int>> rules, int[][] updates)
    {
        return updates
            .Where(update => IsCorrectUpdate(rules, update))
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