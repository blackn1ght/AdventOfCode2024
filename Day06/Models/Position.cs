namespace AdventOfCode2024.Day06.Models;

public record Position(int X, int Y)
{
    public override string ToString() => $"{X},{Y}";
}
