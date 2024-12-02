namespace AdventOfCode24.Day01;

public static class LocationIds
{
    public static void Run()
    {
        var input = File.ReadAllText("Data/day01.txt");
        var result = CalculateDistanceSum(input);
        
        Console.WriteLine(result); // 2285373
    }

    public static int CalculateDistanceSum(string input) 
    {
        var firstColumnNumber = input
            .Split('\n')
            .Select(line => line.Split("   ").First())
            .Select(int.Parse)
            .Order()
            .ToList();

        var secondColumnNumber = input
            .Split('\n')
            .Select(line => line.Split("   ").Last())
            .Select(int.Parse)
            .Order()
            .ToList();

        return firstColumnNumber.Zip(secondColumnNumber)
            .Select(pair => Math.Abs(pair.First - pair.Second))
            .Sum();
    }
}