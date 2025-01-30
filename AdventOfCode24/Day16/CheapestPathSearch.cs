namespace AdventOfCode24.Day16;

public static class CheapestPathSearch
{
    private static string Input => File.ReadAllText("Data/day16.txt");

    public static void RunTask1()
    {
        var result = FindCheapestPathCost(Input);

        Console.WriteLine(result);
    }

    public static int FindCheapestPathCost(string input)
    {
        return 0;
    }
}