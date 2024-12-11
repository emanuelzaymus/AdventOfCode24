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
        var playGround = new PlayGround(playGroundString);

        var currentPosition = playGround.InitialPosition;
        var currentDirection = playGround.InitialDirection;

        while (true)
        {
            if (!playGround.Contains(currentPosition))
            {
                break;
            }

            playGround.SetVisited(currentPosition);

            var nextPossiblePosition = currentDirection.Move(currentPosition);

            if (playGround.Contains(nextPossiblePosition) && playGround.IsObstruction(nextPossiblePosition))
            {
                currentDirection = currentDirection.GetDirectionTurnedRight();
                continue;
            }

            currentPosition = nextPossiblePosition;
        }

        Console.WriteLine(playGround);

        return playGround.GetCountOfVisitedPositions();
    }
}