namespace AdventOfCode24.Common;

public static class StringExtensions
{
    public static string[] SplitLines(this string str) => str.Split(Environment.NewLine);

    public static string SubstringAfter(this string str, string separator)
    {
        var startIndex = str.IndexOf(separator, StringComparison.Ordinal) + separator.Length;
        return str[startIndex..];
    }

    public static string SubstringBefore(this string str, string separator)
    {
        var endIndex = str.IndexOf(separator, StringComparison.Ordinal);
        return str[..endIndex];
    }
}