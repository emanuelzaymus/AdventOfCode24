using AdventOfCode24.Common;

namespace AdventOfCode24.Day16;

internal class Maze : MapBase<char>
{
    private Position _startPosition;
    private Position _endPosition;

    public Maze(string input) : base(input, c => c)
    {
        _startPosition = FindFirstPosition('S');
        _endPosition = FindFirstPosition('E');
    }

    private Position FindFirstPosition(char character)
    {
        var rowIndex = RowList.FindIndex(row => row.Contains(character));
        var colIndex = RowList[rowIndex].FindIndex(c => c == character);
        return new Position(rowIndex, colIndex);
    }
}