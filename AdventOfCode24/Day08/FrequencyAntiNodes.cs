namespace AdventOfCode24.Day08;

/// <summary>
/// https://adventofcode.com/2024/day/8
/// </summary>
public static class FrequencyAntiNodes
{
    private static string Input => File.ReadAllText("Data/day08.txt");

    public static void RunTask1()
    {
        var count = CountFrequencyAntiNodes(Input);

        Console.WriteLine(count); // 295
    }

    public static int CountFrequencyAntiNodes(string input)
    {
        var city = new City(input);
        var antennas = ExtractAntennas(city);

        foreach (var (_, locations) in antennas)
        {
            for (var i = 0; i < locations.Count; i++)
            {
                var firstLocation = locations[i];
                for (var j = 0; j < locations.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var secondLocation = locations[j];

                    var possibleAntiNode = new Location(
                        secondLocation.RowIndex + secondLocation.RowIndex - firstLocation.RowIndex,
                        secondLocation.ColumnIndex + secondLocation.ColumnIndex - firstLocation.ColumnIndex
                    );

                    if (city.Contains(possibleAntiNode))
                    {
                        city.SetAntiNode(possibleAntiNode);
                    }
                }
            }
        }

        return city.CountAllAntiNodes();
    }

    private static Dictionary<char, List<Location>> ExtractAntennas(City city)
    {
        var antennas = new Dictionary<char, List<Location>>();

        for (var row = 0; row < city.Height; row++)
        {
            for (var column = 0; column < city.Width; column++)
            {
                var c = city[row][column];
                if (c == '.')
                {
                    continue;
                }

                if (!antennas.TryGetValue(c, out var value))
                {
                    value = [];
                    antennas.Add(c, value);
                }

                value.Add(new Location(row, column));
            }
        }

        return antennas;
    }
}