using AdventOfCode24.Common;

namespace AdventOfCode24.Day12;

public static class GardenFences
{
    private static string Input => File.ReadAllText("Data/day12.txt");

    public static void RunTask1()
    {
        var price = CalculatePriceOfFencesForAllRegions(Input);

        Console.WriteLine(price);
    }

    public static int CalculatePriceOfFencesForAllRegions(string input)
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
            .Select(r => r.Area * r.Perimeter)
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

        plantRegion.Area++;
        plantRegion.Perimeter += garden.GetNumberOfSidesTouchingDifferentPlantType(position);

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
    }
}