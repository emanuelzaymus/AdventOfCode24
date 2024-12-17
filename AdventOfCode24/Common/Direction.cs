namespace AdventOfCode24.Common;

public class Direction
{
    public static readonly Direction Up = new(-1, 0);
    public static readonly Direction Down = new(1, 0);
    public static readonly Direction Left = new(0, -1);
    public static readonly Direction Right = new(0, 1);

    public static readonly List<Direction> AllDirections = [Up, Right, Down, Left];

    public readonly int RowOffset;
    public readonly int ColumnOffset;

    private Direction(int rowOffset, int columnOffset)
    {
        RowOffset = rowOffset;
        ColumnOffset = columnOffset;
    }

    public Direction TurnRight()
    {
        if (this == Up) return Right;
        if (this == Right) return Down;
        if (this == Down) return Left;
        if (this == Left) return Up;
        throw new InvalidOperationException();
    }
}