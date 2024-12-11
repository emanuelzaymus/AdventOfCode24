namespace AdventOfCode24.Day06;

internal class Direction
{
    public static readonly Direction Up = new(0, -1);
    public static readonly Direction Down = new(0, 1);
    public static readonly Direction Left = new(-1, 0);
    public static readonly Direction Right = new(1, 0);

    private readonly int _offsetX;
    private readonly int _offsetY;

    private Direction(int offsetX, int offsetY)
    {
        _offsetX = offsetX;
        _offsetY = offsetY;
    }

    public Position Move(Position currentPosition) => new(_offsetX + currentPosition.X, _offsetY + currentPosition.Y);

    public Direction GetDirectionTurnedRight()
    {
        if (this == Up) return Right;
        if (this == Right) return Down;
        if (this == Down) return Left;
        if (this == Left) return Up;
        throw new InvalidOperationException();
    }
}