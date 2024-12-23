using AdventOfCode24.Common;

namespace AdventOfCode24.Day12;

internal class Garden(string input) : MapBase<char>(input, c => c)
{
    public const int InvalidPlantTypeId = -1;
    private const char InvalidPlantType = '?';

    private readonly List<List<int>> _plantTypeIds = input
        .SplitLines()
        .Select(row => Enumerable
            .Repeat(InvalidPlantTypeId, row.Length)
            .ToList()
        )
        .ToList();

    public void SetPlantTypeId(Position position, int plantTypeId)
    {
        _plantTypeIds[position.RowIndex][position.ColumnIndex] = plantTypeId;
    }

    public bool TryGetExistingPlantTypeIdForPosition(Position position, out int foundPlantTypeId)
    {
        return TryGetExistingPlantTypeIdForPosition(position, Direction.Left, out foundPlantTypeId)
               || TryGetExistingPlantTypeIdForPosition(position, Direction.Up, out foundPlantTypeId);
    }

    private bool TryGetExistingPlantTypeIdForPosition(Position position, Direction direction, out int foundPlantTypeId)
    {
        var currentPlantType = RowList[position.RowIndex][position.ColumnIndex];

        var newPosition = position.Move(direction);
        var plantType = Contains(newPosition)
            ? RowList[newPosition.RowIndex][newPosition.ColumnIndex]
            : InvalidPlantType;
        if (plantType == currentPlantType)
        {
            foundPlantTypeId = _plantTypeIds[newPosition.RowIndex][newPosition.ColumnIndex];
            return true;
        }

        foundPlantTypeId = InvalidPlantTypeId;
        return false;
    }

    public int GetNumberOfSidesTouchingDifferentPlantType(Position position)
    {
        return Direction.AllDirections
            .Count(direction => !TryGetExistingPlantTypeIdForPosition(position, direction, out _));
    }
}