using AdventOfCode24.Day4;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day4;

[TestClass]
[TestSubject(typeof(WordSearch))]
public class WordSearchTest
{
    private const string Word = "XMAS";
    private const string WordBackwards = "SAMX";

    private const string ExampleInput = """
                                        MMMSXXMASM
                                        MSAMXMSMSA
                                        AMXSXMAAMM
                                        MSAMASMSMX
                                        XMASAMXAMM
                                        XXAMMXXAMA
                                        SMSMSASXSS
                                        SAXAMASAAA
                                        MAMMMXMMMM
                                        MXMXAXMASX
                                        """;

    private readonly string[] _grid = ExampleInput.Split('\n');

    [TestMethod]
    public void CountWordXmas_ExampleInput_ShouldReturnCorrectResult()
    {
        var countWordXmas = WordSearch.CountWordXmas(ExampleInput);

        Assert.AreEqual(18, countWordXmas);
    }

    [TestMethod]
    public void FindWordsHorizontally_ExampleInput_ShouldReturnCorrectResult()
    {
        var wordsHorizontally = WordSearch.FindWordsHorizontally(_grid, Word, WordBackwards);

        Assert.AreEqual(5, wordsHorizontally);
    }

    [TestMethod]
    public void FindWordsVertically_ExampleInput_ShouldReturnCorrectResult()
    {
        var wordsVertically = WordSearch.FindWordsVertically(_grid, Word, WordBackwards);

        Assert.AreEqual(3, wordsVertically);
    }

    [TestMethod]
    public void FindWordsDescendingDiagonally_ExampleInput_ShouldReturnCorrectResult()
    {
        var wordsDescendingDiagonally = WordSearch.FindWordsDescendingDiagonally(_grid, Word, WordBackwards);

        Assert.AreEqual(5, wordsDescendingDiagonally);
    }

    [TestMethod]
    public void FindWordsRisingDiagonally_ExampleInput_ShouldReturnCorrectResult()
    {
        var wordsDescendingDiagonally = WordSearch.FindWordsRisingDiagonally(_grid, Word, WordBackwards);

        Assert.AreEqual(5, wordsDescendingDiagonally);
    }
}