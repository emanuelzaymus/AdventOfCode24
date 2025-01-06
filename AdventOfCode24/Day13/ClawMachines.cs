using AdventOfCode24.Common;

namespace AdventOfCode24.Day13;

public static class ClawMachines
{
    private static string Input => File.ReadAllText("Data/day13.txt");

    public static void RunTask1()
    {
        var numberOfTokens = CalculateFewestNumberOfTokensToWinAllPossibleClawMachines(Input);

        Console.WriteLine(numberOfTokens); // 27157
    }

    public static int CalculateFewestNumberOfTokensToWinAllPossibleClawMachines(string input)
    {
        var clawMachines = ParseClawMachines(input);

        return clawMachines
            .Select(CalculateRoots)
            .Sum(roots => roots is { } r ? r.a * 3 + r.b : 0);
    }

    private static (int a, int b)? CalculateRoots(ClawMachine machine)
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

        return ((int)a, (int)b);
    }

    private static bool IsRoundNumber(double number)
    {
        var integer = (int)number;
        return Math.Abs(number - integer) < 0.000001;
    }

    private static List<ClawMachine> ParseClawMachines(string input)
    {
        return input
            .Split("\n\n")
            .Select(machine =>
            {
                var lines = machine.SplitLines();

                var x1 = int.Parse(lines[0].SubstringAfter("X+").SubstringBefore(", "));
                var y1 = int.Parse(lines[0].SubstringAfter("Y+"));
                var x2 = int.Parse(lines[1].SubstringAfter("X+").SubstringBefore(", "));
                var y2 = int.Parse(lines[1].SubstringAfter("Y+"));
                var x3 = int.Parse(lines[2].SubstringAfter("X=").SubstringBefore(", "));
                var y3 = int.Parse(lines[2].SubstringAfter("Y="));

                return new ClawMachine(x1, x2, x3, y1, y2, y3);
            })
            .ToList();
    }


    private record ClawMachine(int X1, int X2, int X3, int Y1, int Y2, int Y3)
    {
        public double CalculateB() => (Y3 * X1 - X3 * Y1) / (double)(Y2 * X1 - X2 * Y1);
        public double CalculateA(double b) => (X3 - X2 * b) / X1;
    };
}