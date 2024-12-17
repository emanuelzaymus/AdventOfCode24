namespace AdventOfCode24.Day10;

public static class HikingTrails
{
    private static string Input => File.ReadAllText("Data/day10.txt");

    public static void RunTask1()
    {
        var count = CountOfHikingTrails(Input);

        Console.WriteLine(count); // 717
    }

    public static int CountOfHikingTrails(string input)
    {
        var topographicMap = new TopographicMap(input);

        var trailStarts = topographicMap.GetAllTrailStartLocations();

        var reachedTrailEnds = trailStarts.ToDictionary(
            trailStart => trailStart,
            _ => new HashSet<TopographicMap.Location>());

        foreach (var (trailStart, reachedEnds) in reachedTrailEnds)
        {
            FindAllReachableTrailEnd(topographicMap, trailStart, reachedEnds);
        }

        return reachedTrailEnds.Values
            .Sum(reachedEnds => reachedEnds.Count);
    }

    private static void FindAllReachableTrailEnd(TopographicMap topographicMap, TopographicMap.Location currentLocation,
        HashSet<TopographicMap.Location> reachedEnds)
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