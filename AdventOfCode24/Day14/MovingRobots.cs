using AdventOfCode24.Common;

namespace AdventOfCode24.Day14;

public static class MovingRobots
{
    public const int NumberOfMoves = 100;
    private const char EmptyChar = '.';
    private const char RobotChar = 'X';

    private static string Input => File.ReadAllText("Data/day14.txt");

    public static void RunTask1()
    {
        var product = ProductOfRobotCountsInQuadrantsAfterMoves(Input, 103, 101, NumberOfMoves);

        Console.WriteLine(product); // 225648864
    }

    public static void RunTask2()
    {
        var movesCount = ChristmasTreeAfterMoves(Input, 103, 101);

        Console.WriteLine(movesCount); // 7847
    }

    public static int ProductOfRobotCountsInQuadrantsAfterMoves(string input, int playgroundRowCount,
        int playgroundColumnCount, int numberOfMoves)
    {
        var robots = ParseRobots(input);

        foreach (var r in robots)
        {
            r.Move(playgroundRowCount, playgroundColumnCount, numberOfMoves);
        }

        var middleRow = playgroundRowCount / 2;
        var middleColumn = playgroundColumnCount / 2;

        var quadrant1 = robots.Count(r => r.Position.RowIndex < middleRow && r.Position.ColumnIndex < middleColumn);
        var quadrant2 = robots.Count(r => r.Position.RowIndex < middleRow && r.Position.ColumnIndex > middleColumn);
        var quadrant3 = robots.Count(r => r.Position.RowIndex > middleRow && r.Position.ColumnIndex > middleColumn);
        var quadrant4 = robots.Count(r => r.Position.RowIndex > middleRow && r.Position.ColumnIndex < middleColumn);

        return quadrant1 * quadrant2 * quadrant3 * quadrant4;
    }

    private static int ChristmasTreeAfterMoves(string input, int playgroundRowCount, int playgroundColumnCount)
    {
        var robots = ParseRobots(input);
        var playground = new char[playgroundRowCount, playgroundColumnCount];

        var i = 1;
        while (true)
        {
            robots.ForEach(r => r.Move(playgroundRowCount, playgroundColumnCount));

            SetRobotPositions(playground, robots);

            if (ContainsChristmasTree(playground))
            {
                PrintPlayground(playground);
                return i;
            }

            i++;
        }
    }

    private static List<Robot> ParseRobots(string input) => input
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

    private static bool ContainsChristmasTree(char[,] playground)
    {
        var height = playground.GetLength(0);
        var width = playground.GetLength(1);

        for (var row = 0; row < height; row++)
        for (var col = 0; col < width; col++)
        {
            if (!playground.ContainsCharNTimes(row, col, RobotChar, 10))
            {
                continue;
            }

            return true;
        }

        return false;
    }

    private static bool ContainsCharNTimes(this char[,] playground, int fromRow, int fromCol, char c, int nTimes)
    {
        for (var i = 0; i < nTimes; i++)
        {
            if (fromCol + i >= playground.GetLength(1) || playground[fromRow, fromCol + i] != c)
            {
                return false;
            }
        }

        return true;
    }

    private static void SetRobotPositions(char[,] playground, List<Robot> robots)
    {
        playground.FillArray(EmptyChar);

        robots.ForEach(r => playground[r.Position.RowIndex, r.Position.ColumnIndex] = RobotChar);
    }

    private static void FillArray(this char[,] playground, char c)
    {
        for (var row = 0; row < playground.GetLength(0); row++)
        for (var col = 0; col < playground.GetLength(1); col++)
        {
            playground[row, col] = c;
        }
    }

    private static void PrintPlayground(char[,] playground)
    {
        for (var row = 0; row < playground.GetLength(0); row++)
        {
            for (var col = 0; col < playground.GetLength(1); col++)
            {
                Console.Write(playground[row, col]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private record Robot(Position Position, Velocity Velocity)
    {
        public Position Position { get; private set; } = Position;

        public void Move(int playgroundRowCount, int playgroundColumnCount, int times = 1)
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