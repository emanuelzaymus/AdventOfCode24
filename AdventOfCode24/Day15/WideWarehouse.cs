using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

internal class WideWarehouse(string input) : Warehouse(input)
{
    public override void MoveRobot(Direction direction)
    {
        if (!CanRobotMove(RobotPosition, direction))
        {
            return;
        }

        Move(RobotPosition, direction, Empty);

        RobotPosition = RobotPosition.Move(direction);
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

    public override int CalculateSumOfBoxesPositions()
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