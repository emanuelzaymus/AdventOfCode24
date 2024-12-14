namespace AdventOfCode24.Day08;

internal class City(string input)
{
    private readonly List<string> _rows = input
        .Split(Environment.NewLine)
        .ToList();

    private readonly List<List<bool>> _containsAntiNode = input
        .Split(Environment.NewLine)
        .Select(line => Enumerable
            .Repeat(false, line.Length)
            .ToList())
        .ToList();

    public int Height => _rows.Count;

    public int Width => _rows[0].Length;

    public string this[int rowIndex] => _rows[rowIndex];

    public void SetAntiNode(Location location) => _containsAntiNode[location.RowIndex][location.ColumnIndex] = true;

    public int CountAllAntiNodes() => _containsAntiNode
        .Sum(row => row
            .Count(b => b));

    public bool Contains(Location location)
    {
        return location is { RowIndex: >= 0, ColumnIndex: >= 0 }
               && location.RowIndex < Height
               && location.ColumnIndex < Width;
    }
}