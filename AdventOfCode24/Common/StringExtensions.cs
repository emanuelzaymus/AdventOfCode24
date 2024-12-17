namespace AdventOfCode24.Common;

public static class StringExtensions
{
    public static string[] SplitLines(this string str) => str.Split(Environment.NewLine);
}