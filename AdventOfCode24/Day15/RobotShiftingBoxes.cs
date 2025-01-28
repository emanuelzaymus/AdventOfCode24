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

    public static int CalculateSumOfBoxesPositions(string input)
    {
        var (warehouse, moves) = ParseWarehouseAndMoves(input);

        foreach (var direction in moves)
        {
            warehouse.MoveRobot(direction);
        }

        return warehouse.CalculateSumOfBoxesPositions();
    }

    private static (Warehouse warehouse, List<Direction> moves) ParseWarehouseAndMoves(string input)
    {
        var (warehouseStr, movesStr) = input.SplitPair("\n\n");

        var warehouse = new Warehouse(warehouseStr);
        var moves = movesStr
            .Select(c => c)
            .Where(c => c != '\n')
            .Select(Direction.FromChar)
            .ToList();

        return (warehouse, moves);
    }
}