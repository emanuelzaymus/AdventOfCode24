using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

internal class WideWarehouse(string input) : Warehouse(input)
{
    private const char BoxLeft = '[';
    private const char BoxRight = ']';

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
            BoxLeft => CanRobotMoveWideBox(),
            BoxRight => CanRobotMoveWideBox(),
            _ => throw new InvalidOperationException($"Unknown character {this[nextPosition]}")
        };

        bool CanRobotMoveWideBox()
        {
            if (direction == Direction.Left || direction == Direction.Right)
            {
                return CanRobotMove(nextPosition, direction);
            }

            return this[nextPosition] switch
            {
                BoxLeft => CanRobotMove(nextPosition, direction)
                           && CanRobotMove(nextPosition.Move(Direction.Right), direction),
                BoxRight => CanRobotMove(nextPosition, direction)
                            && CanRobotMove(nextPosition.Move(Direction.Left), direction),
                _ => throw new InvalidOperationException(
                    $"Unknown character {this[nextPosition]}. Expected are only {BoxLeft} and {BoxRight}.")
            };
        }
    }

    private void Move(Position currentPosition, Direction direction, char previousCharacter)
    {
        if (direction == Direction.Left || direction == Direction.Right)
        {
            MoveLeftRight(currentPosition, direction, previousCharacter);
        }
        else
        {
            MoveUpDown(currentPosition, direction, previousCharacter);
        }
    }

    private void MoveLeftRight(Position currentPosition, Direction direction, char previousCharacter)
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

    private void MoveUpDown(Position currentPosition, Direction direction, char previousCharacter)
    {
        var currentCharacter = this[currentPosition];
        this[currentPosition] = previousCharacter;

        if (currentCharacter == Empty)
        {
            return;
        }

        MoveUpDown(currentPosition.Move(direction), direction, currentCharacter);

        switch (currentCharacter)
        {
            case BoxLeft: MoveUpDown(currentPosition.Move(Direction.Right), direction, Empty); break;
            case BoxRight: MoveUpDown(currentPosition.Move(Direction.Left), direction, Empty); break;
            case Robot: break;
            default: throw new InvalidOperationException($"Unknown currentCharacter {currentCharacter}");
        }
    }

    public override int CalculateSumOfBoxesPositions() => CalculateSumOfBoxesPositions(BoxLeft);
}