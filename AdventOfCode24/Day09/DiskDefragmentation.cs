namespace AdventOfCode24.Day09;

/// <summary>
/// https://adventofcode.com/2024/day/9
/// </summary>
public static class DiskDefragmentation
{
    private static string Input => File.ReadAllText("Data/day09.txt");

    public static void RunTask1()
    {
        var checksum = CalculateOptimizedDiskChecksum(Input);

        Console.WriteLine(checksum); // 6471961544878
    }

    public static long CalculateOptimizedDiskChecksum(string diskMap)
    {
        var disk = CreateDisk(diskMap);

        OptimizeDisk(disk);

        return CalculateDiskChecksum(disk);
    }

    private static List<int> CreateDisk(string diskMap)
    {
        var result = new List<int>();

        var isBlock = true;
        var currentId = -1;
        foreach (var currentBlockLength in diskMap.Select(ConvertCharToInt))
        {
            var idToWrite = -1;
            if (isBlock)
            {
                idToWrite = ++currentId;
            }

            for (var j = 0; j < currentBlockLength; j++)
            {
                result.Add(idToWrite);
            }

            isBlock = !isBlock;
        }

        return result;
    }

    private static void OptimizeDisk(List<int> disk)
    {
        var positionToWriteIndex = 0;
        for (var i = disk.Count - 1; i >= 0; i--)
        {
            var lastDigit = disk[i];
            if (lastDigit == -1) continue;

            while (positionToWriteIndex < i && disk[positionToWriteIndex] != -1)
            {
                positionToWriteIndex++;
            }

            if (positionToWriteIndex >= i)
            {
                break;
            }

            disk[positionToWriteIndex] = lastDigit;
            disk[i] = -2;
        }
    }

    private static long CalculateDiskChecksum(List<int> disk)
    {
        return disk
            .Where(value => value >= 0)
            .Select((value, index) => value * (long)index)
            .Sum();
    }

    private static int ConvertCharToInt(char digit) => digit - '0';
}