namespace AdventOfCode24.Common;

public static class StringExtensions
{
    public static string[] SplitLines(this string str) => str.Split(Environment.NewLine);

    public static (string first, string second) SplitPair(this string str, char separator)
    {
        var pair = str.Split(separator, 2);
        return (pair[0], pair[1]);
    }

    public static (T first, T second) SplitPair<T>(this string str, char separator, Func<string, T> transform)
    {
        var pair = str.Split(separator, 2);
        return (transform(pair[0]), transform(pair[1]));
    }

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