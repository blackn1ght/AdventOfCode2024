using AdventOfCode2024.Day06.Models;

namespace AdventOfCode2024.Day06;

public static class StringArrayExtensions
{
    private const char Obstacle = '#';

    public static void InsertObstacle(this string[] grid, Position position)
    {
        grid[position.Y] = grid[position.Y].Remove(position.X, 1);
        grid[position.Y] = grid[position.Y].Insert(position.X, Obstacle.ToString());
    }
    
    public static PositionType GetPositionType(this string[] grid, Position position)
    {
        var (x, y) = position;
        if (x < 0 || y < 0 || y >= grid.Length || x >= grid[0].Length) return PositionType.OutsideOfMap;

        return grid[y][x] == Obstacle 
            ? PositionType.Obstacle 
            : PositionType.FreeSpace;
    }
}