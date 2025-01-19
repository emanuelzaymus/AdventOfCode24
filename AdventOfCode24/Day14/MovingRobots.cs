using AdventOfCode24.Common;

namespace AdventOfCode24.Day14;

public static class MovingRobots
{
    public const int NumberOfMoves = 100;

    private static string Input => File.ReadAllText("Data/day14.txt");

    public static void RunTask1()
    {
        var product = ProductOfRobotCountsInQuadrantsAfterMoves(Input, 103, 101, NumberOfMoves);

        Console.WriteLine(product); // 225648864
    }

    public static int ProductOfRobotCountsInQuadrantsAfterMoves(string input, int playgroundRowCount,
        int playgroundColumnCount, int numberOfMoves)
    {
        var robots = ParseRobots(input);

        foreach (var r in robots)
        {
            r.Move(numberOfMoves, playgroundRowCount, playgroundColumnCount);
        }

        var middleRow = playgroundRowCount / 2;
        var middleColumn = playgroundColumnCount / 2;

        var quadrant1 = robots.Count(r => r.Position.RowIndex < middleRow && r.Position.ColumnIndex < middleColumn);
        var quadrant2 = robots.Count(r => r.Position.RowIndex < middleRow && r.Position.ColumnIndex > middleColumn);
        var quadrant3 = robots.Count(r => r.Position.RowIndex > middleRow && r.Position.ColumnIndex > middleColumn);
        var quadrant4 = robots.Count(r => r.Position.RowIndex > middleRow && r.Position.ColumnIndex < middleColumn);

        return quadrant1 * quadrant2 * quadrant3 * quadrant4;
    }

    private static List<Robot> ParseRobots(string input)
    {
        return input
            .SplitLines()
            .Select(row =>
            {
                var (positionStr, velocityStr) = row.SplitPair(' ');

                var (positionColumn, positionRow) = positionStr
                    .SubstringAfter("p=")
                    .SplitPair(',', int.Parse);
                var initialPosition = new Position(positionRow, positionColumn);

                var (velocityColumnOffset, velocityRowOffset) = velocityStr
                    .SubstringAfter("v=")
                    .SplitPair(',', int.Parse);
                var velocity = new Velocity(velocityRowOffset, velocityColumnOffset);

                return new Robot(initialPosition, velocity);
            })
            .ToList();
    }

    private record Robot(Position Position, Velocity Velocity)
    {
        public Position Position { get; private set; } = Position;

        public void Move(int times, int playgroundRowCount, int playgroundColumnCount)
        {
            var newRowIndex = (Position.RowIndex + Velocity.RowOffset * times) % playgroundRowCount;
            newRowIndex = newRowIndex < 0 ? newRowIndex + playgroundRowCount : newRowIndex;

            var newColumnIndex = (Position.ColumnIndex + Velocity.ColumnOffset * times) % playgroundColumnCount;
            newColumnIndex = newColumnIndex < 0 ? newColumnIndex + playgroundColumnCount : newColumnIndex;

            Position = new Position(newRowIndex, newColumnIndex);
        }
    }

    private record Velocity(int RowOffset, int ColumnOffset);
}