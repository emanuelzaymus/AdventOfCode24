using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

internal abstract class Warehouse : MutableMapBase<char>
{
    protected const char Empty = '.';
    protected const char Wall = '#';
    protected const char Robot = '@';

    private Position _robotPosition;

    protected Warehouse(string input) : base(input, c => c)
    {
        var rowIndex = RowList.FindIndex(row => row.Contains(Robot));
        var colIndex = RowList[rowIndex].FindIndex(c => c == Robot);
        _robotPosition = new Position(rowIndex, colIndex);
    }

    public void MoveRobot(Direction direction)
    {
        if (!CanRobotMove(_robotPosition, direction))
        {
            return;
        }

        Move(_robotPosition, direction, Empty);

        _robotPosition = _robotPosition.Move(direction);
    }

    protected abstract bool CanRobotMove(Position currentPosition, Direction direction);

    protected abstract void Move(Position currentPosition, Direction direction, char previousCharacter);

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