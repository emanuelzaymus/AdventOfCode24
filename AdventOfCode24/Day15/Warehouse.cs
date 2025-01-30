using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

internal abstract class Warehouse : MutableMapBase<char>
{
    protected const char Empty = '.';
    protected const char Wall = '#';
    protected const char Robot = '@';

    protected Position RobotPosition;

    protected Warehouse(string input) : base(input, c => c)
    {
        var rowIndex = RowList.FindIndex(row => row.Contains(Robot));
        var colIndex = RowList[rowIndex].FindIndex(c => c == Robot);
        RobotPosition = new Position(rowIndex, colIndex);
    }

    public abstract void MoveRobot(Direction direction);

    public abstract int CalculateSumOfBoxesPositions();

    protected int CalculateSumOfBoxesPositions(char box)
    {
        var result = 0;

        for (var row = 0; row < Height; row++)
        for (var col = 0; col < Width; col++)
        {
            if (RowList[row][col] == box)
            {
                result += 100 * row + col;
            }
        }

        return result;
    }
}