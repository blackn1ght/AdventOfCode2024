namespace AdventOfCode2024.Day07;

public class BridgeRepair(string[] data) : ChallengeBase<long>
{
    private readonly IEnumerable<Line> _lines = data.Select(ParseLine).ToList();
    
    protected override long Part1() => Calculate(["+", "*"]);
    
    protected override long Part2() => Calculate(["+", "*", "||"]);


    private long Calculate(string[] operators)
    {
        long answer = 0;

        foreach (var line in _lines)
        {
            var combinations = GetPermutationsWithRept(operators, line.Values.Length - 1);
            var hasAnswer = false;

            foreach (var combination in combinations)
            {
                var localAnswer = line.Values[0];

                for (var i = 0; i < line.Values.Length - 1; i++)
                {
                    localAnswer = combination[i] switch
                    {
                        "+" => localAnswer += line.Values[i+1],
                        "*" => localAnswer *+ line.Values[i+1],
                        "||" => long.Parse($"{localAnswer}{line.Values[i+1]}"),
                        _ => throw new ArgumentOutOfRangeException($"Unknown operator '{combination[i]}")
                    };
                }

                if (localAnswer == line.TestValue)
                {
                    hasAnswer = true;
                }
            }

            if (hasAnswer) answer += line.TestValue;
        }

        return answer;
    }

    private static IEnumerable<List<T>> GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new List<T>{t});
        return GetPermutationsWithRept(list, length - 1)
            .SelectMany(t => list, (t1, t2) => t1.Concat([t2]).ToList());
    }


    private static Line ParseLine(string dataLine)
    {
        var parts = dataLine.Split(":");
        var testValue = long.Parse(parts[0]);
        var values = parts[1].Trim().Split(" ").Select(long.Parse).ToArray();

        return new Line(testValue, values);
    }
}

public record Line(long TestValue, long[] Values);