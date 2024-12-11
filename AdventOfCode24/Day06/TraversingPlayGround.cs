namespace AdventOfCode24.Day06;

public static class TraversingPlayGround
{
    private static string Input => File.ReadAllText("Data/day06.txt");

    public static void RunTask1()
    {
        var visitedPositions = CountVisitedPositions(Input);

        Console.WriteLine(visitedPositions); // 5131
    }

    public static int CountVisitedPositions(string playGroundString)
    {
        return CountVisitedPositions(new PlayGround(playGroundString));
    }

    private static int CountVisitedPositions(PlayGround playGround)
    {
        var currentPosition = playGround.InitialPosition;
        var currentDirection = playGround.InitialDirection;

        while (true)
        {
            if (!playGround.Contains(currentPosition))
            {
                break;
            }

            playGround.SetVisited(currentPosition);

            var nextPossiblePosition = currentPosition.Move(currentDirection);

            if (playGround.Contains(nextPossiblePosition) && playGround.IsObstruction(nextPossiblePosition))
            {
                currentDirection = currentDirection.TurnRight();
                continue;
            }

            currentPosition = nextPossiblePosition;
        }

        Console.WriteLine(playGround);

        return playGround.GetCountOfVisitedPositions();
    }

    public static void RunTask2()
    {
        var obstructionsThatCauseLoops = ObstructionsThatCauseLoops(Input);

        Console.WriteLine(obstructionsThatCauseLoops); // 1784
    }

    public static int ObstructionsThatCauseLoops(string input)
    {
        var playGround = new PlayGround(input);
        _ = CountVisitedPositions(playGround);

        var count = 0;
        foreach (var visitedPosition in playGround.GetVisitedPositions())
        {
            if (visitedPosition.Equals(playGround.InitialPosition))
            {
                continue;
            }

            PlayGround testPlayGround = new(input);
            testPlayGround.SetObstruction(visitedPosition);

            if (ContainsLoop(testPlayGround))
            {
                count++;
            }
        }

        return count;
    }

    private static bool ContainsLoop(PlayGround playGround)
    {
        var currentPosition = playGround.InitialPosition;
        var currentDirection = playGround.InitialDirection;

        while (true)
        {
            if (!playGround.Contains(currentPosition))
            {
                return false;
            }

            if (playGround.IsVisited(currentPosition, currentDirection))
            {
                return true;
            }

            playGround.SetVisited(currentPosition, currentDirection);

            var nextPossiblePosition = currentPosition.Move(currentDirection);

            if (playGround.Contains(nextPossiblePosition) && playGround.IsObstruction(nextPossiblePosition))
            {
                currentDirection = currentDirection.TurnRight();
                continue;
            }

            currentPosition = nextPossiblePosition;
        }
    }
}