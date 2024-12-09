using AdventOfCode2024.Day06.Models;
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
            var nextPositionType = data.GetPositionType(nextPosition);

            if (nextPositionType == PositionType.Obstacle)
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

        return guard.GetNumberOfUniquePositionsVisited();
    }
    
    protected override int Part2()
    {
        var guard = CreateGuard();

        var hasGuardLeftMappedArea = false;
        HashSet<string> positionOfObstructions = new();

        do
        {
            var nextPosition = guard.GetNextPotentialPosition();
            var nextPositionType = data.GetPositionType(nextPosition);
            
            if (nextPositionType == PositionType.Obstacle)
            {
                guard.ChangeDirection();
            } 
            else if (nextPositionType == PositionType.FreeSpace)
            {
                if (WillEnterLoopIfObstructionIsPlacedIntoPosition(guard, nextPosition))
                {
                    positionOfObstructions.Add(nextPosition.ToString());
                }
                
                guard.Move();
            }
            else if (nextPositionType == PositionType.OutsideOfMap)
            {
                hasGuardLeftMappedArea = true;
            }

        } while (hasGuardLeftMappedArea == false);
        
        return positionOfObstructions.Count;
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
    
    private bool WillEnterLoopIfObstructionIsPlacedIntoPosition(Guard guard, Position nextPosition)
    {
        var copyOfGuard = guard.Copy();
        var copyOfGrid = (string[])data.Clone();
        
        copyOfGrid.InsertObstacle(nextPosition);
        copyOfGuard.ChangeDirection();

        do
        {
            var nextPosition2 = copyOfGuard.GetNextPotentialPosition();
            var nextPositionType = copyOfGrid.GetPositionType(nextPosition2);

            if (copyOfGuard.HasBeenOnPathBefore(nextPosition2))
            {
                return true;
            }

            switch (nextPositionType)
            {
                case PositionType.FreeSpace:
                    copyOfGuard.Move();
                    break;
                default:
                    return false;
            }
            
        } while (true);
    }
}

public class Guard(int x, int y, GuardDirection direction)
{
    public override string ToString() => $"{X},{Y}";
    public GuardDirection Direction { get; private set;} = direction;
    public int NumberOfDirectionChanges { get; private set; } = 0;
    private int X {get; set; } = x;
    private int Y {get; set; } = y;
    private List<PositionAndDirection> _previousPositions { get; set; } = new()
    {
        { new(new (x, y), direction)}
    };
    
    public int GetNumberOfUniquePositionsVisited() => _previousPositions
        .DistinctBy(x => x.Position)
        .Count();

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
    
    public bool HasBeenOnPathBefore(Position position)
        => _previousPositions.Any(p => 
            p.Position.X == position.X
            && p.Position.Y == position.Y
            && p.Direction == Direction);

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

    public Guard Copy() => new (X, Y, Direction)
    {
        _previousPositions = _previousPositions
            .Select(p => new PositionAndDirection(p.Position, p.Direction))
            .ToList()
    };
    
}