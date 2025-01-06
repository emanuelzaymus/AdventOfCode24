using AdventOfCode24.Common;

namespace AdventOfCode24.Day13;

public static class ClawMachines
{
    private const long PrizePositionCoefficient = 10000000000000;

    private static string Input => File.ReadAllText("Data/day13.txt");

    public static void RunTask1()
    {
        var numberOfTokens = CalculateFewestNumberOfTokensToWinAllPossibleClawMachines(Input);

        Console.WriteLine(numberOfTokens); // 27157
    }

    public static void RunTask2()
    {
        var numberOfTokens = CalculateFewestNumberOfTokensToWinAllPossibleClawMachines(Input, withCoefficient: true);

        Console.WriteLine(numberOfTokens); // 104015411578548
    }

    public static long CalculateFewestNumberOfTokensToWinAllPossibleClawMachines(string input,
        bool withCoefficient = false)
    {
        var clawMachines = ParseClawMachines(input, withCoefficient);

        return clawMachines
            .Select(CalculateRoots)
            .Sum(roots => roots is { } r ? r.a * 3 + r.b : 0);
    }

    private static (long a, long b)? CalculateRoots(ClawMachine machine)
    {
        var b = machine.CalculateB();
        if (!IsRoundNumber(b))
        {
            return null;
        }

        var a = machine.CalculateA(b);
        if (!IsRoundNumber(a))
        {
            return null;
        }

        return ((long)a, (long)b);
    }

    private static bool IsRoundNumber(double number)
    {
        var truncated = (long)number;
        return Math.Abs(number - truncated) < 0.000001;
    }

    private static List<ClawMachine> ParseClawMachines(string input, bool withCoefficient)
    {
        return input
            .Split("\n\n")
            .Select(machine =>
            {
                var lines = machine.SplitLines();

                var x1 = long.Parse(lines[0].SubstringAfter("X+").SubstringBefore(", "));
                var y1 = long.Parse(lines[0].SubstringAfter("Y+"));
                var x2 = long.Parse(lines[1].SubstringAfter("X+").SubstringBefore(", "));
                var y2 = long.Parse(lines[1].SubstringAfter("Y+"));
                var x3 = long.Parse(lines[2].SubstringAfter("X=").SubstringBefore(", "));
                var y3 = long.Parse(lines[2].SubstringAfter("Y="));

                return withCoefficient
                    ? new ClawMachine(x1, x2, x3 + PrizePositionCoefficient, y1, y2, y3 + PrizePositionCoefficient)
                    : new ClawMachine(x1, x2, x3, y1, y2, y3);
            })
            .ToList();
    }

    /// <summary>
    /// X1*a + X2*b = X3
    /// Y1*a + Y2*b = Y3
    /// </summary>
    private record ClawMachine(long X1, long X2, long X3, long Y1, long Y2, long Y3)
    {
        /// <summary>
        ///                  Y1*a + Y2*b = Y3
        /// (Y1*(X3 - X2*b)) / X1 + Y2*b = Y3
        ///    Y1*X3 - Y1*X2*b + Y2*X1*b = Y3*X1
        ///            Y2*X1*b - Y1*X2*b = Y3*X1 - Y1*X3
        ///            b*(Y2*X1 - Y1*X2) = Y3*X1 - Y1*X3
        ///                            b = (Y3*X1 - Y1*X3) / (Y2*X1 - Y1*X2)
        /// </summary>
        public double CalculateB() => (Y3 * X1 - Y1 * X3) / (double)(Y2 * X1 - Y1 * X2);

        /// <summary>
        /// X1*a + X2*b = X3
        ///           a = (X3 - X2*b) / X1
        /// </summary>
        public double CalculateA(double b) => (X3 - X2 * b) / X1;
    };
}