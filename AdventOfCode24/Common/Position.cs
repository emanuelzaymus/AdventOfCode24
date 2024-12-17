namespace AdventOfCode24.Common;

public readonly record struct Position(int RowIndex, int ColumnIndex)
{
    public Position Move(Direction direction) => new(
        RowIndex + direction.RowOffset,
        ColumnIndex + direction.ColumnOffset);
}