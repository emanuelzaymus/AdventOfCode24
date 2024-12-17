namespace AdventOfCode24.Common;

public static class CharExtensions
{
    public static int DigitToInt(this char digit)
    {
        return digit is >= '0' and <= '9'
            ? digit - '0'
            : throw new ArgumentOutOfRangeException(nameof(digit), "Invalid digit was out of range 0-9");
    }
}