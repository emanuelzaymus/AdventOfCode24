namespace AdventOfCode24.Common;

internal class Direction
{
    public static readonly Direction Up = new(0, -1, 0);
    public static readonly Direction Right = new(1, 0, 1);
    public static readonly Direction Down = new(2, 1, 0);
    public static readonly Direction Left = new(3, 0, -1);

    public static readonly List<Direction> AllDirections = [Up, Right, Down, Left];

    private readonly int _code;
    public readonly int RowOffset;
    public readonly int ColumnOffset;

    private Direction(int code, int rowOffset, int columnOffset)
    {
        _code = code;
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

    public static implicit operator int(Direction direction) => direction._code;

    public static implicit operator Direction(int code) => FromCode(code);

    private static Direction FromCode(int code) =>
        AllDirections.SingleOrDefault(direction => direction == code)
        ?? throw new ArgumentOutOfRangeException(nameof(code), "Value must be between 0 and 3.");

    public override int GetHashCode() => _code;

    public override string ToString()
    {
        if (this == Up) return nameof(Up);
        if (this == Right) return nameof(Right);
        if (this == Down) return nameof(Down);
        if (this == Left) return nameof(Left);
        throw new InvalidOperationException();
    }
}