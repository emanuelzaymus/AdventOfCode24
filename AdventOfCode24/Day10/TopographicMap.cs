namespace AdventOfCode24.Day10;

internal class TopographicMap(string input)
{
    private const int TrailStart = 0;
    private const int TrailEnd = 9;

    private readonly List<List<int>> _rows = input
        .Split(Environment.NewLine)
        .Select(row => row
            .Select(ConvertCharToInt)
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

    private static int ConvertCharToInt(char digit) => digit - '0';

    public readonly record struct Location(int RowIndex, int ColumnIndex)
    {
        public Location Move(Direction direction) => new(
            RowIndex + direction.RowOffset,
            ColumnIndex + direction.ColumnOffset);
    }

    public class Direction
    {
        public static readonly Direction Up = new(-1, 0);
        public static readonly Direction Down = new(1, 0);
        public static readonly Direction Left = new(0, -1);
        public static readonly Direction Right = new(0, 1);

        public static List<Direction> AllDirections = [Up, Right, Down, Left];

        public readonly int RowOffset;
        public readonly int ColumnOffset;

        private Direction(int rowOffset, int columnOffset)
        {
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
        }
    }
}