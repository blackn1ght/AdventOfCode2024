namespace AdventOfCode2024.Day06.Models;

public record Position(int X, int Y)
{
    public override string ToString() => $"{X},{Y}";

    public bool IsTheSameAs(Position other) => other.X == X && other.Y == Y;
}
