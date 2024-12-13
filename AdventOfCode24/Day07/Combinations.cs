namespace AdventOfCode24.Day07;

public static class Combinations
{
    /// <summary>
    /// Enumerator returns the same instance of an array.
    /// </summary>
    public static IEnumerable<T[]> AllCombinations<T>(int combinationSize, T[] elements)
    {
        var result = new T[combinationSize];

        return SetElement(result, 0, elements);
    }

    private static IEnumerable<T[]> SetElement<T>(T[] result, int index, T[] elements)
    {
        foreach (var elem in elements)
        {
            result[index] = elem;

            if (index == result.Length - 1)
            {
                yield return result;
                continue;
            }

            foreach (var res in SetElement(result, index + 1, elements))
            {
                yield return res;
            }
        }
    }
}