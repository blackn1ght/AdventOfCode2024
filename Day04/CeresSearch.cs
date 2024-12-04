namespace AdventOfCode2024.Day04;

public class CeresSearch(string[] data) : ChallengeBase<int>
{
    protected override int Part1()
    {
        var answer = 0;

        for (var y = 0; y < data.Length; y++)
        {
            answer += data[y]
                .Select((c, xIndex) => c == 'X' ? xIndex : -1)
                .Where(x => x > -1)
                .Sum(x => GetNumberOfXmasFromPoint(y, x));
        }

        return answer;
    }

    protected override int Part2()
    {
        var answer = 0;

        for (var y = 1; y < data.Length - 1; y++)
        {
            answer += data[y]
                .Select((c, x) => c == 'A' && x > 0 && x < data[y].Length - 1 ? x : -1)
                .Where(x => x > -1)
                .Count(x => HasXmasShapeFromPointA(y, x));
        }

        return answer;
    }

    private bool HasXmasShapeFromPointA(int y, int x)
    {
        /*
         * M S  S S  M M  S M
         *  A    A    A    A
         * M S  M M  S S  S M
         */

        var nw = data[y - 1][x - 1];
        var ne = data[y - 1][x + 1];
        var sw = data[y + 1][x - 1];
        var se = data[y + 1][x + 1];

        return (nw) switch
        {
            'M' when sw == 'M' && ne == 'S' && se == 'S' => true,
            'S' when sw == 'M' && ne == 'S' && se == 'M' => true,
            'M' when sw == 'S' && ne == 'M' && se == 'S' => true,
            'S' when sw == 'S' && ne == 'M' && se == 'M' => true,
            _ => false
        };
    }

    private int GetNumberOfXmasFromPoint(int y, int x)
    {
        var minY = y > 0 ? y - 1 : 0;
        var maxY = y < data.Length - 2 ? y + 1 : y;
        var minX = x > 0 ? x - 1 : 0;
        var maxX = x < data[0].Length - 2 ? x + 1 : x;

        var count = 0;
        
        for (var yPos = minY; yPos <= maxY; yPos++)
        {
            for (var xPos = minX; xPos <= maxX; xPos++)
            {
                if (data[yPos][xPos] == 'M' && (y == yPos && x == xPos) == false)
                {
                    var directionOfTravel = GetDirection(x, y, xPos, yPos);
                    if (HasXmasLine(yPos, xPos, ['A', 'S'], directionOfTravel)) count++;
                }
            }
        }

        return count;
    }

    private bool HasXmasLine(int y, int x, char[] remainingChars, GridDirection directionOfTravel)
    {
        if (remainingChars.Length == 0) return true;

        var nextChar = remainingChars[0];
        remainingChars = remainingChars.Skip(1).ToArray();
        
        if (directionOfTravel == GridDirection.North && IsInBounds(y - 1, x) && data[y - 1][x] == nextChar) 
        {
            return HasXmasLine(y - 1, x, remainingChars, directionOfTravel);
        }

        if (directionOfTravel == GridDirection.East && IsInBounds(y, x + 1) && data[y][x + 1] == nextChar) 
        {
            return HasXmasLine(y, x + 1, remainingChars, directionOfTravel);
        }

        if (directionOfTravel == GridDirection.South && IsInBounds(y + 1, x) && data[y + 1][x] == nextChar)
        {
            return HasXmasLine(y + 1, x, remainingChars, directionOfTravel);
        }

        if (directionOfTravel == GridDirection.West && IsInBounds(y, x - 1) && data[y][x - 1] == nextChar)
        {
            return HasXmasLine(y, x - 1, remainingChars, directionOfTravel);
        }

        if (directionOfTravel == GridDirection.NorthEast && IsInBounds(y - 1, x + 1) && data[y - 1][x + 1] == nextChar)
        {
            return HasXmasLine(y - 1, x + 1, remainingChars, directionOfTravel);
        }

        if (directionOfTravel == GridDirection.SouthEast && IsInBounds(y + 1, x + 1) && data[y + 1][x + 1] == nextChar)
        {
            return HasXmasLine(y + 1, x + 1, remainingChars, directionOfTravel);
        }

        if (directionOfTravel == GridDirection.SouthWest && IsInBounds(y + 1, x - 1) && data[y + 1][x - 1] == nextChar)
        {
            return HasXmasLine(y + 1, x - 1, remainingChars, directionOfTravel);
        }

        if (directionOfTravel == GridDirection.NorthWest && IsInBounds(y - 1, x - 1) && data[y - 1][x - 1] == nextChar)
        {
            return HasXmasLine(y - 1, x - 1, remainingChars, directionOfTravel);
        }

        return false;
    }

    private bool IsInBounds(int y, int x)
        => (y < 0 || x < 0 || y >= data.Length || x >= data[0].Length) == false;
    
    private static GridDirection GetDirection(int currX, int currY, int x, int y)
    {
        if (x == currX)
        {
            if ((currY - 1) == y) return GridDirection.North;
            if ((currY + 1) == y) return GridDirection.South;
        }

        if (y == currY)
        {
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

        throw new InvalidOperationException($"Unable to work out direction for {x},{y} to {currX},{currY}");
    }
}