using System.Text.RegularExpressions;

namespace AdventOfCode24.Day4;

public static class WordSearch
{
    private static string Input => File.ReadAllText("Data/day04.txt");

    public static void RunTask1()
    {
        var count = CountWordXmas(Input);

        Console.WriteLine(count); // 2646
    }

    public static int CountWordXmas(string input)
    {
        const string word = "XMAS";
        const string wordBackwards = "SAMX";

        var grid = input.Split('\n');

        var horizontally = FindWordsHorizontally(grid, word, wordBackwards);
        var vertically = FindWordsVertically(grid, word, wordBackwards);
        var descendingDiagonally = FindWordsDescendingDiagonally(grid, word, wordBackwards);
        var risingDiagonally = FindWordsRisingDiagonally(grid, word, wordBackwards);

        return horizontally + vertically + descendingDiagonally + risingDiagonally;
    }

    public static int FindWordsHorizontally(string[] grid, string word, string wordBackwards)
    {
        return grid.Sum(row =>
            FindWordCount(row, word) + FindWordCount(row, wordBackwards)
        );
    }

    private static int FindWordCount(ReadOnlySpan<char> chars, string word)
    {
        return Regex.Matches(chars.ToString(), word).Count;
    }

    public static int FindWordsVertically(string[] grid, string word, string wordBackwards)
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

            result += FindWordCount(column, word) + FindWordCount(column, wordBackwards);
        }

        return result;
    }

    public static int FindWordsDescendingDiagonally(string[] grid, string word, string wordBackwards)
    {
        var verticalLenght = grid.Length - word.Length + 1;
        var horizontalLenght = grid[0].Length - word.Length + 1;

        Span<char> diagonal = stackalloc char[word.Length];

        var result = 0;
        for (var i = 0; i < verticalLenght; i++)
        {
            for (var j = 0; j < horizontalLenght; j++)
            {
                for (var k = 0; k < word.Length; k++)
                {
                    diagonal[k] = grid[i + k][j + k];
                }

                if (diagonal.SequenceEqual(word) || diagonal.SequenceEqual(wordBackwards))
                {
                    result++;
                }
            }
        }

        return result;
    }

    public static int FindWordsRisingDiagonally(string[] grid, string word, string wordBackwards)
    {
        var verticalLenght = grid.Length - word.Length + 1;
        var horizontalLenghtReal = grid[0].Length;

        Span<char> diagonal = stackalloc char[word.Length];

        var result = 0;
        for (var i = 0; i < verticalLenght; i++)
        {
            for (var j = word.Length - 1; j < horizontalLenghtReal; j++)
            {
                for (var k = 0; k < word.Length; k++)
                {
                    diagonal[k] = grid[i + k][j - k];
                }

                if (diagonal.SequenceEqual(word) || diagonal.SequenceEqual(wordBackwards))
                {
                    result++;
                }
            }
        }

        return result;
    }
}