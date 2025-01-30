using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

internal class WideWarehouse(string input) : Warehouse(input)
{
    private const char BoxLeft = '[';
    private const char BoxRight = ']';

    protected override bool CanRobotMove(Position currentPosition, Direction direction)
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

    protected override void Move(Position currentPosition, Direction direction, char previousCharacter)
    {
        var currentCharacter = this[currentPosition];
        this[currentPosition] = previousCharacter;

        if (currentCharacter == Empty)
        {
            return;
        }

        Move(currentPosition.Move(direction), direction, currentCharacter);

        if (direction == Direction.Left || direction == Direction.Right ||
            previousCharacter == Empty || previousCharacter == currentCharacter) return;

        switch (currentCharacter)
        {
            case BoxLeft: Move(currentPosition.Move(Direction.Right), direction, Empty); break;
            case BoxRight: Move(currentPosition.Move(Direction.Left), direction, Empty); break;
            case Robot: break;
            default: throw new InvalidOperationException($"Unknown currentCharacter {currentCharacter}");
        }
    }

    public override int CalculateSumOfBoxesPositions() => CalculateSumOfBoxesPositions(BoxLeft);
}