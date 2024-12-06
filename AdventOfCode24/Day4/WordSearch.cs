namespace AdventOfCode24.Day4;

public static class WordSearch
{
    private static string Input => File.ReadAllText("Data/day04.txt");

    public static void RunTask1()
    {
        var count = CountWordXmas(Input);

        Console.WriteLine(count); // 
    }

    public static int CountWordXmas(string input)
    {
        const string word = "XMAS";
        const string wordBackwards = "SAMX";

        var grid = input.Split('\n');

        var horizontally = FindWordsHorizontally(grid, word) + FindWordsHorizontally(grid, wordBackwards);
        var vertically = FindWordsVertically(grid, word) + FindWordsVertically(grid, wordBackwards);
        var descendingDiagonally = FindWordsDescendingDiagonally(grid, word)
                                   + FindWordsDescendingDiagonally(grid, wordBackwards);
        var risingDiagonally = FindWordsRisingDiagonally(grid, word) + FindWordsRisingDiagonally(grid, wordBackwards);

        return horizontally + vertically + descendingDiagonally + risingDiagonally;
    }

    public static int FindWordsHorizontally(string[] grid, string word) =>
        grid.AsEnumerable()
            .Sum(row => FindWordCount(row, word));

    private static int FindWordCount(ReadOnlySpan<char> chars, string word)
    {
        var index = 0;
        var result = 0;
        while ((index = chars[index..chars.Length].IndexOf(word)) != -1)
        {
            index += word.Length;
            result++;
        }

        return result;
    }

    public static int FindWordsVertically(string[] grid, string word)
    {
        var horizontalLenght = grid[0].Length;
        var verticalLenght = grid.Length;

        Span<char> column = stackalloc char[verticalLenght];

        var result = 0;
        for (var i = 0; i < horizontalLenght; i++)
        {
            for (var j = 0; j < verticalLenght; j++)
            {
                column[j] = grid[j][i];
            }

            result += FindWordCount(column, word);
        }

        return result;
    }

    public static int FindWordsDescendingDiagonally(string[] grid, string word)
    {
        var verticalLenght = grid.Length - word.Length + 1;
        var horizontalLenght = grid[0].Length - word.Length + 1;

        Span<char> diagonal = stackalloc char[word.Length];

        var result = 0;
        for (int i = 0; i < verticalLenght; i++)
        {
            for (int j = 0; j < horizontalLenght; j++)
            {
                for (var k = 0; k < word.Length; k++)
                {
                    diagonal[k] = grid[i + k][j + k];
                }

                if (diagonal.SequenceEqual(word))
                {
                    result++;
                }
            }
        }

        return result;
    }

    public static int FindWordsRisingDiagonally(string[] grid, string word)
    {
        var verticalLenght = grid.Length - word.Length + 1;
        var horizontalLenghtReal = grid[0].Length;

        var result = 0;
        for (int i = 0; i < verticalLenght; i++)
        {
            for (int j = word.Length - 1; j < horizontalLenghtReal; j++)
            {
                var found = word
                    .Select((c, index) => grid[i + index][j - index] == c)
                    .All(c => c);
                result += found ? 1 : 0;
            }
        }

        return result;
    }
}