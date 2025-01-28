namespace AdventOfCode24.Common;

internal abstract class MutableMapBase<T>(string input, Func<char, T> convert)
{
    protected readonly List<List<T>> RowList = input
        .SplitLines()
        .Select(row => row
            .Select(convert)
            .ToList()
        )
        .ToList();

    protected int Height => RowList.Count;

    protected int Width => RowList[0].Count;

    protected T this[Position position]
    {
        get => RowList[position.RowIndex][position.ColumnIndex];
        set => RowList[position.RowIndex][position.ColumnIndex] = value;
    }
}