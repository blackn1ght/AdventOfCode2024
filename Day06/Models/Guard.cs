using AdventOfCode2024.Day06.Models;

namespace AdventOfCode2024.Day06;

public class Guard(int x, int y, GuardDirection direction)
{
    public GuardDirection Direction { get; private set;} = direction;
    public int NumberOfDirectionChanges { get; private set; } = 0;
    private int X {get; set; } = x;
    private int Y {get; set; } = y;
    private List<PositionAndDirection> _previousPositions { get; set; } = new()
    {
        { new(new (x, y), direction)}
    };
    public Position StartingPosition => new(x, y);
    
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

        return Direction switch
        {
            GuardDirection.Up => new(xToMoveTo, --yToMoveTo),
            GuardDirection.Right => new(++xToMoveTo, yToMoveTo),
            GuardDirection.Down => new(xToMoveTo, ++yToMoveTo),
            GuardDirection.Left => new(--xToMoveTo, yToMoveTo),
            _ => throw new ArgumentOutOfRangeException(nameof(Direction))
        };
    }

    public Guard Copy() => new (X, Y, Direction)
    {
        _previousPositions = _previousPositions
            .Select(p => new PositionAndDirection(p.Position, p.Direction))
            .ToList()
    };
    
}