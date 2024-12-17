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

    public List<Location> GetAllTrailStartLocations()
    {
        var result = new List<Location>();

        for (var i = 0; i < Height; i++)
        for (var j = 0; j < Width; j++)
        {
            if (_rows[i][j] == TrailStart)
            {
                result.Add(new Location(i, j));
            }
        }

        return result;
    }

    public bool IsTrailEnd(Location location) => _rows[location.RowIndex][location.ColumnIndex] == TrailEnd;

    public bool Contains(Location location)
    {
        return location.RowIndex >= 0
               && location.RowIndex < Height
               && location.ColumnIndex >= 0
               && location.ColumnIndex < Width;
    }

    public bool IsReachable(Location fromLocation, Location toLocation)
    {
        var fromHeight = _rows[fromLocation.RowIndex][fromLocation.ColumnIndex];
        var toHeight = _rows[toLocation.RowIndex][toLocation.ColumnIndex];
        return toHeight - fromHeight == 1;
    }
}