namespace AdventOfCode24.Day09;

public class Disk(string diskMap)
{
    private const int InvalidBlockId = -1;

    private readonly List<int> _blockIds = CreateDisk(diskMap);

    public int BlockCount => _blockIds.Count;

    public ICollection<BlockSequence> GetBlockSequencesFromBack()
    {
        var diskBlockSequences = new LinkedList<BlockSequence>();

        var isBlock = true;
        var currentBlockId = 0;
        var diskIndex = 0;

        foreach (var currentBlockCount in diskMap.Select(ConvertCharToInt))
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

    public int ReadBlockId(int index) => _blockIds[index];

    public void WriteBlockId(int startIndex, int blockId) => _blockIds[startIndex] = blockId;

    public void ClearBlockId(int startIndex) => _blockIds[startIndex] = InvalidBlockId;

    public void Write(int startIndex, BlockSequence blockSequence)
    {
        WriteInternal(blockSequence.BlockId, startIndex, blockSequence.Length);
    }

    public void Clear(BlockSequence blockSequence)
    {
        WriteInternal(InvalidBlockId, blockSequence.StartIndex, blockSequence.Length);
    }

    private void WriteInternal(int blockId, int startIndex, int length)
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
        foreach (var currentBlockCount in diskMap.Select(ConvertCharToInt))
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

    private static int ConvertCharToInt(char digit) => digit - '0';

    public record struct BlockSequence(int BlockId, int StartIndex, int Length);
}