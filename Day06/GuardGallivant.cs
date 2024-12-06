using Xunit.Abstractions;

namespace AdventOfCode2024.Day06;

public class GuardGallivant(ITestOutputHelper output, string[] data) : ChallengeBase<int>
{
    protected override int Part1()
    {
        var guard = CreateGuard();

        var hasGuardLeftMappedArea = false;

        do
        {
            var nextPosition = guard.GetNextPotentialPosition();
            var nextPositionType = GetPositionType(data, nextPosition);

            if (nextPositionType == PositionType.Obstable)
            {
                guard.ChangeDirection();
            } 
            else if (nextPositionType == PositionType.FreeSpace)
            {
                guard.Move();
            }
            else if (nextPositionType == PositionType.OutsideOfMap)
            {
                hasGuardLeftMappedArea = true;
            }

        } while (hasGuardLeftMappedArea == false);

        return guard.PreviousMoves.DistinctBy(x => x.Position).Count();
    }
    
    protected override int Part2()
    {
        var guard = CreateGuard();

        var hasGuardLeftMappedArea = false;
        HashSet<string> positionOfObstructions = new();

        do
        {
            var nextPosition = guard.GetNextPotentialPosition();
            var nextPositionType = GetPositionType(data, nextPosition);

            if (guard.NumberOfDirectionChanges >= 3 && nextPositionType == PositionType.FreeSpace && positionOfObstructions.Contains(nextPosition.ToString()) == false)
            {
                if (WillEnterLoopIfObstructionIsPlacedIntoPosition(guard, nextPosition) )
                {
                    positionOfObstructions.Add(nextPosition.ToString());
                }
            }

            if (nextPositionType == PositionType.Obstable)
            {
                guard.ChangeDirection();
            } 
            else if (nextPositionType == PositionType.FreeSpace)
            {
                guard.Move();
            }
            else if (nextPositionType == PositionType.OutsideOfMap)
            {
                hasGuardLeftMappedArea = true;
            }

        } while (hasGuardLeftMappedArea == false);

        foreach (var positionOfObstruction in positionOfObstructions)
        {
            output.WriteLine(positionOfObstruction);
        }

        return positionOfObstructions.Count();
    }


    private Guard CreateGuard()
    {
        const string startingDirection = "^";

        for (var y = 0; y < data.Length; y++)
        {
            var xPositionOfGuard = data[y].IndexOf(startingDirection);
            if (xPositionOfGuard > -1)
            {
                return new Guard(xPositionOfGuard, y, GuardDirection.Up);
            }
        }
        
        throw new Exception("Failed to locate the guard.");
    }

    private static PositionType GetPositionType(string[] grid, Position position)
    {
        var (x, y) = position;
        if (x < 0 || y < 0 || y >= grid.Length || x >= grid[0].Length) return PositionType.OutsideOfMap;

        return grid[y][x] == '#' 
            ? PositionType.Obstable 
            : PositionType.FreeSpace;
    }


    public bool WillEnterLoopIfObstructionIsPlacedIntoPosition(Guard guard, Position position)
    {
        // play through a scenario where an obstruction is placed at poition
        // put obstruction in the next possible location
        // simulate turning
        // does the _previousPositions contain a line 
        var copyOfGuard = guard.Copy();
        var copyOfGrid = (string[])data.Clone();

        // put in obstable
        copyOfGrid.InsertObstacle(position);

        // we've pust put the obstacle down in the next position
        // so we need to immediately change direction
        copyOfGuard.ChangeDirection();

        do
        {
            var nextPosition = copyOfGuard.GetNextPotentialPosition();
            var nextPositionType = GetPositionType(copyOfGrid, nextPosition);

            if (copyOfGuard.HasBeenOnPathBefore(nextPosition, copyOfGuard.Direction))
            {
                return true;
            }
            else 
            {
                if (nextPositionType == PositionType.FreeSpace)
                {
                    copyOfGuard.Move();
                }
                else if (nextPositionType is PositionType.OutsideOfMap or PositionType.Obstable)
                {
                    return false;
                }
            }

        } while (true);
    }
}

public class Guard(int X, int Y, GuardDirection Direction)
{
    public override string ToString() => $"{X},{Y}";

    public GuardDirection Direction { get; private set;} = Direction;
    public int X {get; private set; } = X;
    public int Y {get; private set; } = Y;
    public int NumberOfDirectionChanges { get; private set; } = 0;


    private List<PositionAndDirection> _previousPositions { get; set; } = new()
    {
        { new(new (X, Y), Direction)}
    };

    public List<PositionAndDirection> PreviousMoves => _previousPositions;

    public void ChangeDirection()
    {
        Direction = Direction switch
        {
            GuardDirection.Up => GuardDirection.Right,
            GuardDirection.Right => GuardDirection.Down,
            GuardDirection.Down => GuardDirection.Left,
            GuardDirection.Left => GuardDirection.Up,
            _ => throw  new Exception($"Unknown direction {Direction}")
        };

        NumberOfDirectionChanges++;
    }

    public void Move()
    {
        if (Direction == GuardDirection.Up) Y--;
        if (Direction == GuardDirection.Right) X++;
        if (Direction == GuardDirection.Down) Y++;
        if (Direction == GuardDirection.Left) X--;

        _previousPositions.Add(new(new(X, Y), Direction));
    }

    public bool HasVisitedPositionBefore(Position position)
        => _previousPositions.Any(p => 
        p.Position.X == position.X && 
        p.Position.Y == position.Y);

    public bool HasBeenOnPathBefore(Position position, GuardDirection direction)
        => _previousPositions.Any(p => 
            p.Position.X == position.X
            && p.Position.Y == position.Y
            && p.Direction == direction);
    

    public Position GetNextPotentialPosition()
    {
        var xToMoveTo = X;
        var yToMoveTo = Y;

        if (Direction == GuardDirection.Up)
        {
            yToMoveTo--;
        }
        else if (Direction == GuardDirection.Right)
        {
            xToMoveTo++;
        }
        else if (Direction == GuardDirection.Down)
        {
            yToMoveTo++;
        }
        else
        {
            xToMoveTo--;
        }

        return new (xToMoveTo, yToMoveTo);
    }

    public Guard Copy() => new Guard(X, Y, Direction)
    {
        _previousPositions = _previousPositions
            .Select(p => new PositionAndDirection(p.Position, p.Direction))
            .ToList()
    };
    
}

public static class StringArrayExtensions
{
    public static void InsertObstacle(this string[] grid, Position position)
    {
        grid[position.Y] = grid[position.Y].Remove(position.X, 1);
        grid[position.Y] = grid[position.Y].Insert(position.X, "#");
    }
}