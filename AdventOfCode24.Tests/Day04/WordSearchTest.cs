using AdventOfCode24.Common;
using AdventOfCode24.Day04;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day04;

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

    private readonly string[] _grid = ExampleInput.SplitLines();

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
        var wordsRisingDiagonally = WordSearch.FindWordsRisingDiagonally(_grid, Word, WordBackwards);

        Assert.AreEqual(5, wordsRisingDiagonally);
    }

    [TestMethod]
    public void CountWordMasInXForm_ExampleInput_ShouldReturnCorrectResult()
    {
        var wordMasInXForm = WordSearch.CountWordMasInXForm(ExampleInput);

        Assert.AreEqual(9, wordMasInXForm);
    }
}