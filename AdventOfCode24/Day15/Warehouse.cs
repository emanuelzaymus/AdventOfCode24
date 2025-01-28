using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

internal class Warehouse : MutableMapBase<char>
{
    private const char Empty = '.';
    private const char Wall = '#';
    private const char Box = 'O';
    private const char Robot = '@';

    private Position _robotPosition;

    public Warehouse(string input) : base(input, c => c)
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

    private bool CanRobotMove(Position currentPosition, Direction direction)
    {
        var nextPosition = currentPosition.Move(direction);

        return this[nextPosition] switch
        {
            Wall => false,
            Empty => true,
            Box => CanRobotMove(nextPosition, direction),
            _ => throw new InvalidOperationException($"Unknown character {this[nextPosition]}")
        };
    }

    private void Move(Position currentPosition, Direction direction, char previousCharacter)
    {
        while (true)
        {
            var currentCharacter = this[currentPosition];
            this[currentPosition] = previousCharacter;

            if (currentCharacter == Empty)
            {
                return;
            }

            currentPosition = currentPosition.Move(direction);
            previousCharacter = currentCharacter;
        }
    }

    public int CalculateSumOfBoxesPositions()
    {
        var result = 0;

        for (var row = 0; row < Height; row++)
        for (var col = 0; col < Width; col++)
        {
            if (RowList[row][col] == Box)
            {
                result += 100 * row + col;
            }
        }

        return result;
    }
}