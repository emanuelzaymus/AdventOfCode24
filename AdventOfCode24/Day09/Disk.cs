using AdventOfCode24.Common;

namespace AdventOfCode24.Day09;

public class Disk(string diskMap)
{
    private const int InvalidBlockId = -1;

    private readonly List<int> _blockIds = CreateDisk(diskMap);

    public int BlockCount => _blockIds.Count;

    public int ReadBlockId(int index) => _blockIds[index];

    public void WriteBlockId(int blockId, int index) => WriteBlockIds(blockId, index, 1);

    public void WriteBlockIds(int blockId, int startIndex, int length)
    {
        for (var i = startIndex; i < startIndex + length; i++)
        {
            if (blockId != InvalidBlockId && _blockIds[i] != InvalidBlockId)
            {
                throw new ArgumentException($"Attempt to rewrite non-empty block at position {i}");
            }

            if (blockId == InvalidBlockId && _blockIds[i] == InvalidBlockId)
            {
                throw new ArgumentException($"Attempt to clear already empty block at position {i}");
            }

            _blockIds[i] = blockId;
        }
    }

    public void ClearBlock(int index) => ClearBlocks(index, 1);

    public void ClearBlocks(int startIndex, int length) => WriteBlockIds(InvalidBlockId, startIndex, length);

    public ICollection<BlockSequence> GetBlockSequencesFromBack()
    {
        var diskBlockSequences = new LinkedList<BlockSequence>();

        var isBlock = true;
        var currentBlockId = 0;
        var diskIndex = 0;

        foreach (var currentBlockCount in diskMap.Select(CharExtensions.DigitToInt))
        {
            if (isBlock)
            {
                diskBlockSequences.AddFirst(new BlockSequence(currentBlockId, diskIndex, currentBlockCount));
                currentBlockId++;
            }

            isBlock = !isBlock;
            diskIndex += currentBlockCount;
        }

        return diskBlockSequences;
    }

    public int FindFirstIndexOfGap(int gapLength, int maxIndex)
    {
        var invalidBlockLength = 0;

        for (var i = 0; i < maxIndex; i++)
        {
            if (_blockIds[i] != InvalidBlockId)
            {
                invalidBlockLength = 0;
                continue;
            }

            invalidBlockLength++;

            if (invalidBlockLength == gapLength)
            {
                return i - invalidBlockLength + 1;
            }
        }

        return -1;
    }

    public long CalculateChecksum()
    {
        return _blockIds
            .Select((blockId, index) => IsValidDiskBlock(blockId)
                ? blockId * (long)index
                : 0)
            .Sum();
    }

    private static List<int> CreateDisk(string diskMap)
    {
        var diskBlockIds = new List<int>();

        var isBlock = true;
        var currentBlockId = 0;
        foreach (var currentBlockCount in diskMap.Select(CharExtensions.DigitToInt))
        {
            var blockIdToWrite = InvalidBlockId;
            if (isBlock)
            {
                blockIdToWrite = currentBlockId++;
            }

            for (var j = 0; j < currentBlockCount; j++)
            {
                diskBlockIds.Add(blockIdToWrite);
            }

            isBlock = !isBlock;
        }

        return diskBlockIds;
    }

    public static bool IsValidDiskBlock(int blockId) => blockId != InvalidBlockId;

    public record struct BlockSequence(int BlockId, int StartIndex, int Length);
}