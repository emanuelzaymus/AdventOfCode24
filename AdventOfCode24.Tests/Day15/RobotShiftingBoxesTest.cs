using AdventOfCode24.Day15;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode24.Tests.Day15;

[TestClass]
[TestSubject(typeof(RobotShiftingBoxes))]
public class RobotShiftingBoxesTest
{
    private const string ExampleInput = """
                                        ##########
                                        #..O..O.O#
                                        #......O.#
                                        #.OO..O.O#
                                        #..O@..O.#
                                        #O#..O...#
                                        #O..O..O.#
                                        #.OO.O.OO#
                                        #....O...#
                                        ##########

                                        <vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
                                        vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
                                        ><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
                                        <<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
                                        ^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
                                        ^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
                                        >^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
                                        <><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
                                        ^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
                                        v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
                                        """;

    private const string SmallerExampleInput = """
                                               ########
                                               #..O.O.#
                                               ##@.O..#
                                               #...O..#
                                               #.#.O..#
                                               #...O..#
                                               #......#
                                               ########

                                               <^^>>>vv<v>>v<<
                                               """;

    private const string SmallerExampleInputForTask2 = """
                                                       #######
                                                       #...#.#
                                                       #.....#
                                                       #..OO@#
                                                       #..O..#
                                                       #.....#
                                                       #######

                                                       <vv<<^^<<^^
                                                       """;


    [TestMethod]
    public void CalculateSumOfBoxesPositions_ExampleInput_ShouldReturnCorrectResult()
    {
        var result = RobotShiftingBoxes.CalculateSumOfBoxesPositions(ExampleInput);

        Assert.AreEqual(10092, result);
    }

    [TestMethod]
    public void CalculateSumOfBoxesPositions_SmallerExampleInput_ShouldReturnCorrectResult()
    {
        var result = RobotShiftingBoxes.CalculateSumOfBoxesPositions(SmallerExampleInput);

        Assert.AreEqual(2028, result);
    }

    [TestMethod]
    public void CalculateSumOfBoxesPositionsTwiceAsWide_ExampleInput_ShouldReturnCorrectResult()
    {
        var result = RobotShiftingBoxes.CalculateSumOfBoxesPositions(ExampleInput, twiceAsWide: true);

        Assert.AreEqual(9021, result);
    }

    [TestMethod]
    public void CalculateSumOfBoxesPositionsTwiceAsWide_SmallerExampleInputForTask2_ShouldReturnCorrectResult()
    {
        var result =
            RobotShiftingBoxes.CalculateSumOfBoxesPositions(SmallerExampleInputForTask2, twiceAsWide: true,
                printOutput: true);

        Assert.AreEqual((100 * 1 + 5) + (100 * 2 + 7) + (100 * 3 + 6), result);
    }
}