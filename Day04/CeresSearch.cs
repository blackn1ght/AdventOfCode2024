using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;

namespace AdventOfCode2024.Day04;

public enum GridDirection
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

public class CeresSearch(string[] data) : ChallengeBase<int>
{
    protected override int Part1()
    {
        var answer = 0;

        for (var y = 0; y < data.Length; y++)
        {
            // Find positions of x
            answer += data[y]
                .Select((c, xIndex) => c == 'X' ? xIndex : -1)
                .Where(x => x > -1)
                .Sum(x => HasXmasLine(y, x, ['M', 'A', 'S'], []));
        }

        return answer;
    }

    protected override int Part2() => 0;

    private int HasXmasLine(int y, int x, char[] remainingChars, List<(int y, int x)> previousCoordinates, GridDirection? directionOfTravel = null)
    {
        previousCoordinates.Add((y, x));
        if (remainingChars.Length == 0)
        {
            Console.WriteLine($"Line: {string.Join(", ", previousCoordinates)}");
            return 1;
        }

        var nextChar = remainingChars[0];
        remainingChars = remainingChars.Skip(1).ToArray();

        if (directionOfTravel is null)
        {
            var minY = y > 0 ? y -1 : 0;
            var maxY = y < data.Length - 2 ? y + 1 : y;
            var minX = x > 0 ? x - 1 : 0;
            var maxX = x < data[0].Length - 2 ? x + 1 : x;

            List<int> results = [];

            for (var yPos = minY; y <= maxY; yPos++)
            {
                for (var xPos = minX; xPos <= maxX; xPos++)
                {
                    if (IsInBounds(yPos, xPos) && data[yPos][xPos] == nextChar && (y == yPos && x == xPos) == false)
                    {
                        directionOfTravel = GetDirection(x, y, xPos, yPos);
                        var newPreviousCoordinates = new List<(int y, int x)>();
                        newPreviousCoordinates.Add((y, x));
                        var result = HasXmasLine(yPos, xPos, remainingChars, newPreviousCoordinates, directionOfTravel);
                        results.Add(result);
                    }
                }
            }

            return results.Sum();
        }
        else
        {
            if (directionOfTravel == GridDirection.North && IsInBounds(y - 1, x) && data[y - 1][x] == nextChar) 
            {
                return HasXmasLine(y - 1, x, remainingChars, previousCoordinates, directionOfTravel);
            }

            if (directionOfTravel == GridDirection.East && IsInBounds(y, x + 1) && data[y][x + 1] == nextChar) 
            {
                return HasXmasLine(y, x + 1, remainingChars, previousCoordinates, directionOfTravel);
            }

            if (directionOfTravel == GridDirection.South && IsInBounds(y + 1, x) && data[y + 1][x] == nextChar)
            {
                return HasXmasLine(y + 1, x, remainingChars, previousCoordinates, directionOfTravel);
            }

            if (directionOfTravel == GridDirection.West && IsInBounds(y, x - 1) && data[y][x - 1] == nextChar)
            {
                return HasXmasLine(y, x - 1, remainingChars, previousCoordinates, directionOfTravel);
            }

            if (directionOfTravel == GridDirection.NorthEast && IsInBounds(y - 1, x + 1) && data[y - 1][x + 1] == nextChar)
            {
                return HasXmasLine(y - 1, x + 1, remainingChars, previousCoordinates, directionOfTravel);
            }

            if (directionOfTravel == GridDirection.SouthEast && IsInBounds(y + 1, x + 1) && data[y + 1][x + 1] == nextChar)
            {
                return HasXmasLine(y + 1, x + 1, remainingChars, previousCoordinates, directionOfTravel);
            }

            if (directionOfTravel == GridDirection.SouthWest && IsInBounds(y + 1, x - 1) && data[y + 1][x - 1] == nextChar)
            {
                return HasXmasLine(y + 1, x - 1, remainingChars, previousCoordinates, directionOfTravel);
            }

            if (directionOfTravel == GridDirection.NorthWest && IsInBounds(y - 1, x - 1) && data[y - 1][x - 1] == nextChar)
            {
                return HasXmasLine(y - 1, x - 1, remainingChars, previousCoordinates, directionOfTravel);
            }
        }

        return 0;
    }

    private bool IsInBounds(int y, int x)
        => (y < 0 || x < 0 || y >= data.Length || x >= data[0].Length) == false;
    

    private static GridDirection? GetDirection(int currX, int currY, int x, int y)
    {
        if (x == currX)
        {
            // on the same column
            if ((currY - 1) == y) return GridDirection.North;
            if ((currY + 1) == y) return GridDirection.South;
        }

        if (y == currY)
        {
            // on the same row
            if ((currX - 1) == x) return GridDirection.West;
            if ((currX + 1) == x) return GridDirection.East;
        }

        if (x == (currX + 1))
        {
            if (y == (currY + 1)) return GridDirection.SouthEast;
            if (y == (currY - 1)) return GridDirection.NorthEast;
        }

        if (x == (currX - 1))
        {
            if (y == (currY + 1)) return GridDirection.SouthWest;
            if (y == (currY - 1)) return GridDirection.NorthWest;
        }

        return null;
    }
}

public class CeresSearchTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 18)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 0)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 0)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(4, inputType);
        
        var answer = new CeresSearch(data).GetAnswerForPart(challengePart);
        
        Assert.Equal(expectedAnswer, answer);
    }
}