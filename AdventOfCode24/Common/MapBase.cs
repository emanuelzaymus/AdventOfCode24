using System.Collections.Immutable;

namespace AdventOfCode24.Common;

internal abstract class MapBase<T>(string input, Func<char, T> convert)
{
    protected readonly ImmutableList<ImmutableList<T>> RowList = input
        .SplitLines()
        .Select(row => row
            .Select(convert)
            .ToImmutableList()
        )
        .ToImmutableList();

    protected int Height => RowList.Count;

    protected int Width => RowList[0].Count;

    public T this[Position position] => RowList[position.RowIndex][position.ColumnIndex];

    public bool Contains(Position position)
    {
        return position.RowIndex >= 0
               && position.RowIndex < Height
               && position.ColumnIndex >= 0
               && position.ColumnIndex < Width;
    }
}