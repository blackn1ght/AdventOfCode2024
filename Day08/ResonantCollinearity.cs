namespace AdventOfCode2024.Day08;

using Models;

public class ResonantCollinearity(ITestOutputHelper output, string[] data) : ChallengeBase<int>
{
    private readonly Grid _grid = new(data);

    protected override int Part1()
        => _grid
            .GetAntennaPositions()
            .Sum(p => _grid.InsertAntinodes(p.Value));

    protected override int Part2()
    {
        foreach (var position in _grid.GetAntennaPositions())
        {
            _grid.InsertAntinodesPart2(position.Value);
        }
        
        return _grid.CountNonBlankPoints();
    }
}

internal record Coord(int X, int Y);