namespace AdventOfCode24.Day06;

internal readonly record struct Position(int RowIndex, int ColumnIndex)
{
    public Position Move(Direction direction) => new(
        RowIndex + direction.RowOffset,
        ColumnIndex + direction.ColumnOffset);
}