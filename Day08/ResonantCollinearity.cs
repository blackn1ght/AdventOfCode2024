using System.Text.RegularExpressions;
using Xunit.Abstractions;

namespace AdventOfCode2024.Day08;

public class ResonantCollinearity(ITestOutputHelper output, string[] data) : ChallengeBase<int>
{
    private readonly Grid _grid = new(data);

    protected override int Part1()
        => _grid
            .GetAntennaPositions()
            .Sum(p => _grid.InsertAntinodes(p.Value));

    protected override int Part2()
    {
        var answer = _grid
            .GetAntennaPositions()
            .Sum(p => _grid.InsertAntinodesPart2(p.Value));

        _grid.PrintGrid(output);

        return answer;
    }
}

internal class Grid(string[] data)
{
    private readonly string[] _grid = data;
    private readonly int _maxY = data.Length - 1;
    private readonly int _maxX = data[0].Length - 1;

    public bool IsInBounds(Coord coord)
        => coord.X >= 0 && coord.X <= _maxX
            && coord.Y >= 0 && coord.Y <= _maxY;

    public Dictionary<string, List<Coord>> GetAntennaPositions()
    {
        var antennaPositions = new Dictionary<string, List<Coord>>();

        for (var y = 0; y <= _maxY; y++)
        {
            for (var x = 0; x <= _maxX; x++)
            {
                var charAtPosition = _grid[y][x].ToString();
                if (Regex.IsMatch(charAtPosition, @"[a-zA-Z0-9]"))
                {
                    if (antennaPositions.ContainsKey(charAtPosition) == false)
                    {
                        antennaPositions.Add(charAtPosition, new List<Coord>());
                    }

                    antennaPositions[charAtPosition].Add(new (x, y));
                }
            }
        }

        return antennaPositions;
    }

    public int InsertAntinodes(List<Coord> coords)
    {
        var insertions = 0;

        for (var i = 0; i < coords.Count - 1; i++)
        {
            var (thisX, thisY) = coords[i];

            for (var k = i + 1; k < coords.Count; k++)
            {
                var (nextX, nextY) = coords[k];

                var xDiff = nextX - thisX;
                var yDiff = nextY - thisY;

                var first = new Coord(thisX - xDiff, thisY - yDiff);
                var second = new Coord(nextX + xDiff, nextY + yDiff);

                if (InsertAntinode(first)) insertions++;
                if (InsertAntinode(second)) insertions++;
            }
        }

        return insertions;
    }

        public int InsertAntinodesPart2(List<Coord> coords)
    {
        var insertions = 0;

        for (var i = 0; i < coords.Count - 1; i++)
        {
            var (thisX, thisY) = coords[i];

            for (var k = i + 1; k < coords.Count; k++)
            {
                var (nextX, nextY) = coords[k];

                var xDiff = nextX - thisX;
                var yDiff = nextY - thisY;

                var first = new Coord(thisX - xDiff, thisY - yDiff);
                var second = new Coord(nextX + xDiff, nextY + yDiff);

                while (IsInBounds(first))
                {
                    if (InsertAntinode(first)) insertions++;
                    first = new Coord(first.X - xDiff, first.Y - yDiff);
                }

                while (IsInBounds(second))
                {
                    if (InsertAntinode(second)) insertions++;
                    second = new Coord(second.X + xDiff, second.Y + yDiff);
                }
            }
        }

        return insertions;
    }

    public void PrintGrid(ITestOutputHelper output)
    {
        foreach (var row in _grid)
        {
            output.WriteLine(row);
        }
    }

    private bool InsertAntinode(Coord coord)
    {
        if (IsInBounds(coord) == false) return false;

        var (x, y) = coord;
        var currentValue = _grid[y][x];

        if (currentValue == '#') return false;
        if (currentValue == '.')
        {
            _grid[y] = _grid[y].Remove(x, 1);
            _grid[y] = _grid[y].Insert(x, "#");
        }
        return true;
    }
}

internal record Coord(int X, int Y);

public class ResonantCollinearityTests(ITestOutputHelper output)
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 14)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 299)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 34)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer, params long[] incorrectAnswers)
    {
        var data = ChallengeDataReader.GetDataForDay(8, inputType);
        
        var answer = new ResonantCollinearity(output, data).GetAnswerForPart(challengePart);
        
        Assert.DoesNotContain(incorrectAnswers, i => answer == i);
        Assert.Equal(expectedAnswer, answer);
    }

}