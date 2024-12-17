namespace AdventOfCode24.Common;

public readonly record struct Location(int RowIndex, int ColumnIndex)
{
    public Location Move(Direction direction) => new(
        RowIndex + direction.RowOffset,
        ColumnIndex + direction.ColumnOffset);
}