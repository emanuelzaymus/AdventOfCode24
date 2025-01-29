using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

internal abstract class Warehouse : MutableMapBase<char>
{
    protected const char Empty = '.';
    protected const char Wall = '#';
    protected const char Box = 'O';
    private const char Robot = '@';

    protected Position RobotPosition;

    protected Warehouse(string input) : base(input, c => c)
    {
        var rowIndex = RowList.FindIndex(row => row.Contains(Robot));
        var colIndex = RowList[rowIndex].FindIndex(c => c == Robot);
        RobotPosition = new Position(rowIndex, colIndex);
    }

    public abstract void MoveRobot(Direction direction);

    public abstract int CalculateSumOfBoxesPositions();
}