using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day08.Models;

internal class Grid(string[] data)
{
    private readonly int _maxY = data.Length - 1;
    private readonly int _maxX = data[0].Length - 1;
    
    public Dictionary<string, List<Coord>> GetAntennaPositions()
    {
        var antennaPositions = new Dictionary<string, List<Coord>>();

        for (var y = 0; y <= _maxY; y++)
        {
            for (var x = 0; x <= _maxX; x++)
            {
                var charAtPosition = data[y][x].ToString();
                if (Regex.IsMatch(charAtPosition, @"[a-zA-Z0-9]"))
                {
                    if (antennaPositions.ContainsKey(charAtPosition) == false)
                    {
                        antennaPositions.Add(charAtPosition, []);
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

    public void InsertAntinodesPart2(List<Coord> coords)
    {
        for (var i = 0; i < coords.Count - 1; i++)
        {
            var (thisX, thisY) = coords[i];

            for (var k = i + 1; k < coords.Count; k++)
            {
                var (nextX, nextY) = coords[k];

                var xDiff = nextX - thisX;
                var yDiff = nextY - thisY;

                var thisAntenna = new Coord(thisX - xDiff, thisY - yDiff);
                while (IsInBounds(thisAntenna))
                {
                    InsertAntinode(thisAntenna);
                    thisAntenna = new Coord(thisAntenna.X - xDiff, thisAntenna.Y - yDiff);
                }

                var nextAntenna = new Coord(nextX + xDiff, nextY + yDiff);
                while (IsInBounds(nextAntenna))
                {
                    InsertAntinode(nextAntenna);
                    nextAntenna = new Coord(nextAntenna.X + xDiff, nextAntenna.Y + yDiff);
                }
            }
        }
    }

    public int CountNonBlankPoints()
        => data.Sum(c => c.Count(x => x != '.'));
    
    private bool IsInBounds(Coord coord)
        => coord.X >= 0 && coord.X <= _maxX
                        && coord.Y >= 0 && coord.Y <= _maxY;
    
    private bool InsertAntinode(Coord coord)
    {
        if (IsInBounds(coord) == false) return false;

        var (x, y) = coord;
        var currentValue = data[y][x];

        if (currentValue == '#') return false;
        if (currentValue == '.')
        {
            data[y] = data[y].Remove(x, 1);
            data[y] = data[y].Insert(x, "#");
        }
        return true;
    }
}