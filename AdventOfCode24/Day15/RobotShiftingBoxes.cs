using System.Text;
using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

public static class RobotShiftingBoxes
{
    private static string Input => File.ReadAllText("Data/day15.txt");

    public static void RunTask1()
    {
        var result = CalculateSumOfBoxesPositions(Input);

        Console.WriteLine(result); // 1421727
    }

    public static void RunTask2()
    {
        var result = CalculateSumOfBoxesPositions(Input, twiceAsWide: true);

        Console.WriteLine(result); // 1463160
    }

    public static int CalculateSumOfBoxesPositions(string input, bool twiceAsWide = false, bool printOutput = false)
    {
        var (warehouse, moves) = ParseWarehouseAndMoves(input, twiceAsWide);

        foreach (var (index, direction) in moves.Index())
        {
            warehouse.MoveRobot(direction);

            if (!printOutput) continue;
            Console.WriteLine($"Move {index + 1}: {direction}");
            Console.WriteLine(warehouse.ToString());
            Console.WriteLine();
        }

        return warehouse.CalculateSumOfBoxesPositions();
    }

    private static (Warehouse warehouse, List<Direction> moves) ParseWarehouseAndMoves(string input, bool twiceAsWide)
    {
        var (warehouseStr, movesStr) = input.SplitPair("\n\n");

        if (twiceAsWide)
        {
            warehouseStr = warehouseStr
                .Aggregate(new StringBuilder(), (sb, c) => c == '\n' ? sb.Append(c) : sb.Append(MakeTwiceAsWide(c)))
                .ToString();
        }

        Warehouse warehouse = twiceAsWide ? new WideWarehouse(warehouseStr) : new NormalWarehouse(warehouseStr);
        var moves = movesStr
            .Select(c => c)
            .Where(c => c != '\n')
            .Select(Direction.FromChar)
            .ToList();

        return (warehouse, moves);
    }

    private static string MakeTwiceAsWide(char character)
    {
        return character switch
        {
            '#' => "##",
            'O' => "[]",
            '.' => "..",
            '@' => "@.",
            _ => throw new ArgumentOutOfRangeException(nameof(character),
                "Value must be between one of '#', 'O', '.' or '@'.")
        };
    }
}