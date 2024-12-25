using AdventOfCode24.Common;

namespace AdventOfCode24.Day12;

/// <summary>
/// https://adventofcode.com/2024/day/12
/// </summary>
public static class GardenFences
{
    private static string Input => File.ReadAllText("Data/day12.txt");

    public static void RunTask1()
    {
        var price = CalculatePriceOfFencesForAllRegions(Input);

        Console.WriteLine(price); // 1457298
    }

    public static void RunTask2()
    {
        var price = CalculatePriceOfFencesForAllRegions(Input, withDiscount: true);

        Console.WriteLine(price); // 921636
    }

    public static int CalculatePriceOfFencesForAllRegions(string input, bool withDiscount = false)
    {
        var garden = new Garden(input);
        var regions = new List<PlantRegion>();

        var nextPlantTypeId = 0;
        foreach (var uncategorizedPosition in garden.GetAllUncategorizedPositions())
        {
            var plantType = garden[uncategorizedPosition];
            var newPlantRegion = new PlantRegion(nextPlantTypeId, plantType);
            regions.Insert(nextPlantTypeId, newPlantRegion);

            CategorizeAllPositionsRecursively(garden, uncategorizedPosition, newPlantRegion);

            nextPlantTypeId++;
        }

        return regions
            .Select(r => r.Area * (withDiscount ? r.CalculateNumberOfRegionSides() : r.Perimeter))
            .Sum();
    }

    private static void CategorizeAllPositionsRecursively(Garden garden, Position position, PlantRegion plantRegion)
    {
        if (!garden.Contains(position)
            || garden.GetPlantTypeId(position) != Garden.InvalidPlantTypeId
            || garden[position] != plantRegion.PlantType)
        {
            return;
        }

        garden.SetPlantTypeId(position, plantRegion.PlantTypeId);

        foreach (var touchingDirection in garden.GetSidesTouchingDifferentPlantType(position))
        {
            if (plantRegion.RegionSides.TryGetValue(touchingDirection, out var foundRegionSides))
            {
                foundRegionSides.Add(position);
            }
            else
            {
                plantRegion.RegionSides.Add(touchingDirection, [position]);
            }

            plantRegion.Perimeter++;
        }

        plantRegion.Area++;

        Direction.AllDirections
            .ForEach(direction =>
                CategorizeAllPositionsRecursively(garden, position.Move(direction), plantRegion));
    }

    private record PlantRegion(int PlantTypeId, char PlantType)
    {
        public int PlantTypeId { get; } = PlantTypeId;
        public char PlantType { get; } = PlantType;
        public int Area { get; set; }
        public int Perimeter { get; set; }
        public Dictionary<Direction, List<Position>> RegionSides { get; } = new();

        public int CalculateNumberOfRegionSides() =>
            RegionSides
                .Sum(pair =>
                {
                    var (direction, positions) = pair;

                    var groupSelector = (Position p) => p.RowIndex;
                    var numberSelector = (Position p) => p.ColumnIndex;

                    if (IsVerticalSide(direction))
                    {
                        groupSelector = p => p.ColumnIndex;
                        numberSelector = p => p.RowIndex;
                    }

                    return positions
                        .GroupBy(groupSelector)
                        .Sum(grouping => grouping
                            .Select(numberSelector)
                            .Order()
                            .CountNumberOfGaps() + 1);
                });
    }

    private static bool IsVerticalSide(Direction direction) =>
        direction == Direction.Left || direction == Direction.Right;

    private static int CountNumberOfGaps(this IOrderedEnumerable<int> numberSequence)
    {
        var numbers = numberSequence.ToList();

        return numbers
            .Zip(numbers.Skip(1), (first, second) => second - first)
            .Count(difference => difference != 1);
    }
}