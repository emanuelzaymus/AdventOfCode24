using AdventOfCode24.Common;

namespace AdventOfCode24.Day15;

internal class Warehouse(string input) : MapBase<char>(input, c => c)
{
    public const char Empty = '.';
    public const char Wall = '#';
    public const char Box = 'O';
    public const char Robot = '@';
}