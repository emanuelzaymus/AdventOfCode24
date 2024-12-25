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

    public IEnumerable<Position> GetAllUncategorizedPositions()
    {
        for (var row = 0; row < Height; row++)
        {
            for (var col = 0; col < Width; col++)
            {
                if (_plantTypeIds[row][col] == InvalidPlantTypeId)
                {
                    yield return new Position(row, col);
                }
            }
        }
    }

    public int GetPlantTypeId(Position position) => _plantTypeIds[position.RowIndex][position.ColumnIndex];

    public void SetPlantTypeId(Position position, int plantTypeId)
    {
        _plantTypeIds[position.RowIndex][position.ColumnIndex] = plantTypeId;
    }

    public int GetNumberOfSidesTouchingDifferentPlantType(Position position)
    {
        return Direction.AllDirections
            .Count(direction => !HasSamePlantTypeInDirection(position, direction));
    }

    private bool HasSamePlantTypeInDirection(Position position, Direction direction)
    {
        var currentPlantType = RowList[position.RowIndex][position.ColumnIndex];

        var newPosition = position.Move(direction);
        var plantType = Contains(newPosition)
            ? RowList[newPosition.RowIndex][newPosition.ColumnIndex]
            : InvalidPlantType;

        return plantType == currentPlantType;
    }
}