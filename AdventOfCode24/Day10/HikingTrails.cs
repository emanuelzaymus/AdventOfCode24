namespace AdventOfCode24.Day10;

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

        var trailStarts = topographicMap.GetAllTrailStartLocations();

        var reachedTrailEnds = trailStarts.ToDictionary(
            trailStart => trailStart,
            _ => new List<TopographicMap.Location>());

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

    private static void FindAllReachableTrailEnd(TopographicMap topographicMap, TopographicMap.Location currentLocation,
        List<TopographicMap.Location> reachedEnds)
    {
        if (topographicMap.IsTrailEnd(currentLocation))
        {
            reachedEnds.Add(currentLocation);
            return;
        }

        foreach (var direction in TopographicMap.Direction.AllDirections)
        {
            var newPossibleLocation = currentLocation.Move(direction);

            if (topographicMap.Contains(newPossibleLocation)
                && topographicMap.IsReachable(currentLocation, newPossibleLocation))
            {
                FindAllReachableTrailEnd(topographicMap, newPossibleLocation, reachedEnds);
            }
        }
    }
}