namespace AdventOfCode24.Day09;

/// <summary>
/// https://adventofcode.com/2024/day/9
/// </summary>
public static class DiskOptimization
{
    private static string Input => File.ReadAllText("Data/day09.txt");

    public static void RunTask1()
    {
        var checksum = CalculateOptimizedDiskChecksum(Input);

        Console.WriteLine(checksum); // 6471961544878
    }

    public static void RunTask2()
    {
        var checksum = CalculateOptimizedDiskChecksum(Input, withFragmentation: false);

        Console.WriteLine(checksum); // 6511178035564
    }

    public static long CalculateOptimizedDiskChecksum(string diskMap, bool withFragmentation = true)
    {
        var disk = new Disk(diskMap);

        if (withFragmentation) OptimizeDisk(disk);
        else OptimizeDiskWithoutFragmentation(disk);

        return disk.CalculateChecksum();
    }

    private static void OptimizeDisk(Disk disk)
    {
        var invalidBlockIndex = 0;
        for (var i = disk.BlockCount - 1; i >= 0; i--)
        {
            var currentBlockId = disk.ReadBlockId(i);
            if (!Disk.IsValidDiskBlock(currentBlockId)) continue;

            while (invalidBlockIndex < i && Disk.IsValidDiskBlock(disk.ReadBlockId(invalidBlockIndex)))
            {
                invalidBlockIndex++;
            }

            if (invalidBlockIndex >= i)
            {
                break;
            }

            disk.WriteBlockId(currentBlockId, invalidBlockIndex);
            disk.ClearBlock(index: i);
        }
    }

    private static void OptimizeDiskWithoutFragmentation(Disk disk)
    {
        foreach (var blockSequence in disk.GetBlockSequencesFromBack())
        {
            var gapStartIndex = disk.FindFirstIndexOfGap(blockSequence.Length, blockSequence.StartIndex);

            // ReSharper disable once InvertIf
            if (gapStartIndex > 0)
            {
                disk.WriteBlockIds(blockSequence.BlockId, gapStartIndex, blockSequence.Length);
                disk.ClearBlocks(blockSequence.StartIndex, blockSequence.Length);
            }
        }
    }
}