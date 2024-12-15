namespace AdventOfCode24.Day09;

/// <summary>
/// https://adventofcode.com/2024/day/9
/// </summary>
public static class DiskDefragmentation
{
    private const int InvalidBlockId = -1;

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
        var diskBlockIds = new List<int>();

        var isBlock = true;
        var currentBlockId = -1;
        foreach (var currentBlockCount in diskMap.Select(ConvertCharToInt))
        {
            var blockIdToWrite = InvalidBlockId;
            if (isBlock)
            {
                blockIdToWrite = ++currentBlockId;
            }

            for (var j = 0; j < currentBlockCount; j++)
            {
                diskBlockIds.Add(blockIdToWrite);
            }

            isBlock = !isBlock;
        }

        return diskBlockIds;
    }

    private static void OptimizeDisk(List<int> disk)
    {
        var invalidBlockIndex = 0;
        for (var i = disk.Count - 1; i >= 0; i--)
        {
            var currentBlockId = disk[i];
            if (!IsValidDiskBlock(currentBlockId)) continue;

            while (invalidBlockIndex < i && IsValidDiskBlock(disk[invalidBlockIndex]))
            {
                invalidBlockIndex++;
            }

            if (invalidBlockIndex >= i)
            {
                break;
            }

            disk[invalidBlockIndex] = currentBlockId;
            disk[i] = InvalidBlockId;
        }
    }

    private static long CalculateDiskChecksum(List<int> disk)
    {
        return disk
            .Where(IsValidDiskBlock)
            .Select((blockId, index) => blockId * (long)index)
            .Sum();
    }

    private static bool IsValidDiskBlock(int blockId) => blockId != InvalidBlockId;

    private static int ConvertCharToInt(char digit) => digit - '0';
}