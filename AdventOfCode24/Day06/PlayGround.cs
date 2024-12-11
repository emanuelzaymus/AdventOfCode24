namespace AdventOfCode24.Day06;

internal class PlayGround
{
    private readonly List<List<char>> _playGround;
    private readonly int _height;
    private readonly int _width;
    internal readonly Position InitialPosition;
    internal readonly Direction InitialDirection = Direction.Up;

    public PlayGround(string playGroundString)
    {
        _playGround = playGroundString
            .Split('\n')
            .Select(row => row.ToList())
            .ToList();
        _height = _playGround.Count;
        _width = _playGround[0].Count;

        var initialPositionY = _playGround.FindIndex(it => it.Contains('^'));
        var initialPositionX = _playGround[initialPositionY].IndexOf('^');
        InitialPosition = new Position(initialPositionX, initialPositionY);
    }

    public bool Contains(Position position)
    {
        return position.X >= 0 && position.X < _width && position.Y >= 0 && position.Y < _height;
    }

    public void SetVisited(Position currentPosition)
    {
        _playGround[currentPosition.Y][currentPosition.X] = 'X';
    }

    public int GetCountOfVisitedPositions()
    {
        return _playGround.Sum(row => row.Count(c => c == 'X'));
    }

    public bool IsObstruction(Position position)
    {
        return _playGround[position.Y][position.X] == '#';
    }

    public override string ToString()
    {
        return string.Join(string.Empty, _playGround.Select(row => string.Join("", row) + Environment.NewLine));
    }
}