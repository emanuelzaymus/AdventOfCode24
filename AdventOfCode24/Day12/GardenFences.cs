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

        for (var row = 0; row < garden.Height; row++)
        {
            for (var col = 0; col < garden.Width; col++)
            {
                var currentPosition = new Position(row, col);

                var currentPerimeterIncrease = garden.GetNumberOfSidesTouchingDifferentPlantType(currentPosition);

                if (garden.TryGetExistingPlantTypeIdForPosition(currentPosition, out var foundPlantTypeId))
                {
                    if (foundPlantTypeId == Garden.InvalidPlantTypeId)
                    {
                        throw new Exception("Plant type id should be set at this point.");
                    }

                    garden.SetPlantTypeId(currentPosition, foundPlantTypeId);
                    var foundPlantRegion = regions[foundPlantTypeId];
                    foundPlantRegion.Area++;
                    foundPlantRegion.Perimeter += currentPerimeterIncrease;
                    continue;
                }

                garden.SetPlantTypeId(currentPosition, nextPlantTypeId++);
                regions.Add(new PlantRegion(1, currentPerimeterIncrease));
            }
        }

        return regions
            .Select(r => r.Area * r.Perimeter)
            .Sum();
    }

    private record PlantRegion(int Area, int Perimeter)
    {
        public int Area { get; set; } = Area;
        public int Perimeter { get; set; } = Perimeter;
    }
}