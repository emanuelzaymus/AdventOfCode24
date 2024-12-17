using AdventOfCode24.Common;

namespace AdventOfCode24.Day10;

internal class TopographicMap(string input)
{
    private const int TrailStart = 0;
    private const int TrailEnd = 9;

    private readonly List<List<int>> _rows = input
        .Split(Environment.NewLine)
        .Select(row => row
            .Select(CharExtensions.DigitToInt)
            .ToList()
        )
        .ToList();

    private int Height => _rows.Count;
    private int Width => _rows[0].Count;

    public List<Position> GetAllTrailStartPositions()
    {
        var result = new List<Position>();

        for (var i = 0; i < Height; i++)
        for (var j = 0; j < Width; j++)
        {
            if (_rows[i][j] == TrailStart)
            {
                result.Add(new Position(i, j));
            }
        }

        return result;
    }

    public bool IsTrailEnd(Position position) => _rows[position.RowIndex][position.ColumnIndex] == TrailEnd;

    public bool Contains(Position position)
    {
        return position.RowIndex >= 0
               && position.RowIndex < Height
               && position.ColumnIndex >= 0
               && position.ColumnIndex < Width;
    }

    public bool IsReachable(Position fromPosition, Position toPosition)
    {
        var fromHeight = _rows[fromPosition.RowIndex][fromPosition.ColumnIndex];
        var toHeight = _rows[toPosition.RowIndex][toPosition.ColumnIndex];
        return toHeight - fromHeight == 1;
    }
}