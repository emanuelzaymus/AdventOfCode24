using AdventOfCode24.Common;

namespace AdventOfCode24.Day06;

internal class PlayGround
{
    private const char InitialPositionChar = '^';
    private const char VisitedChar = 'X';
    private const char ObstructionChar = '#';

    private readonly List<List<char>> _playGround;
    private readonly List<List<HashSet<Direction>>> _visitedWithDirections;
    private readonly int _height;
    private readonly int _width;
    internal readonly Position InitialPosition;
    internal readonly Direction InitialDirection = Direction.Up;

    public PlayGround(string playGroundString)
    {
        _playGround = playGroundString
            .Split(Environment.NewLine)
            .Select(row => row.ToList())
            .ToList();
        _visitedWithDirections = _playGround
            .Select(row => row
                .Select(_ => new HashSet<Direction>())
                .ToList())
            .ToList();
        _height = _playGround.Count;
        _width = _playGround[0].Count;

        var initialRowIndex = _playGround.FindIndex(it => it.Contains(InitialPositionChar));
        var initialColumnIndex = _playGround[initialRowIndex].IndexOf(InitialPositionChar);
        InitialPosition = new Position(initialRowIndex, initialColumnIndex);
    }

    public bool Contains(Position position)
    {
        return position.RowIndex >= 0
               && position.RowIndex < _height
               && position.ColumnIndex >= 0
               && position.ColumnIndex < _width;
    }

    public void SetVisited(Position position)
    {
        _playGround[position.RowIndex][position.ColumnIndex] = VisitedChar;
    }

    public void SetVisited(Position position, Direction direction)
    {
        SetVisited(position);
        _visitedWithDirections[position.RowIndex][position.ColumnIndex].Add(direction);
    }

    public void SetObstruction(Position position)
    {
        _playGround[position.RowIndex][position.ColumnIndex] = ObstructionChar;
    }

    public int GetCountOfVisitedPositions()
    {
        return _playGround.Sum(row => row.Count(c => c == VisitedChar));
    }

    public bool IsVisited(Position position, Direction direction)
    {
        return _playGround[position.RowIndex][position.ColumnIndex] == VisitedChar
               && _visitedWithDirections[position.RowIndex][position.ColumnIndex].Contains(direction);
    }

    public bool IsObstruction(Position position)
    {
        return _playGround[position.RowIndex][position.ColumnIndex] == ObstructionChar;
    }

    public IEnumerable<Position> GetVisitedPositions()
    {
        for (var row = 0; row < _height; row++)
        {
            for (var col = 0; col < _width; col++)
            {
                if (_playGround[row][col] == VisitedChar)
                {
                    yield return new Position(row, col);
                }
            }
        }
    }

    public override string ToString()
    {
        return string.Join(string.Empty, _playGround
            .Select(row => string.Join(string.Empty, row) + Environment.NewLine)
        );
    }
}