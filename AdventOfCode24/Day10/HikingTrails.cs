using AdventOfCode24.Common;

namespace AdventOfCode24.Day10;

/// <summary>
/// https://adventofcode.com/2024/day/10
/// </summary>
public static class HikingTrails
{
    private static string Input => File.ReadAllText("Data/day10.txt");

    public static void RunTask1()
    {
        var count = CountOfHikingTrails(Input);

        Console.WriteLine(count); // 717
    }

    public static void RunTask2()
    {
        var count = CountOfHikingTrails(Input, countDistinctTrails: false);

        Console.WriteLine(count); // 1686
    }

    public static int CountOfHikingTrails(string input, bool countDistinctTrails = true)
    {
        var topographicMap = new TopographicMap(input);

        var trailStarts = topographicMap.GetAllTrailStartPositions();

        var reachedTrailEnds = trailStarts.ToDictionary(
            trailStart => trailStart,
            _ => new List<Position>());

        foreach (var (trailStart, reachedEnds) in reachedTrailEnds)
        {
            FindAllReachableTrailEnd(topographicMap, trailStart, reachedEnds);
        }

        if (countDistinctTrails)
        {
            return reachedTrailEnds.Values
                .Sum(reachedEnds => reachedEnds
                    .Distinct()
                    .Count());
        }

        return reachedTrailEnds.Values
            .Sum(reachedEnds => reachedEnds.Count);
    }

    private static void FindAllReachableTrailEnd(TopographicMap topographicMap, Position currentPosition,
        List<Position> reachedEnds)
    {
        if (topographicMap.IsTrailEnd(currentPosition))
        {
            reachedEnds.Add(currentPosition);
            return;
        }

        foreach (var direction in Direction.AllDirections)
        {
            var newPossiblePosition = currentPosition.Move(direction);

            if (topographicMap.Contains(newPossiblePosition)
                && topographicMap.IsReachable(currentPosition, newPossiblePosition))
            {
                FindAllReachableTrailEnd(topographicMap, newPossiblePosition, reachedEnds);
            }
        }
    }
}