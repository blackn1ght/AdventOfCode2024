namespace AdventOfCode2024.Day07;

public class BridgeRepair(string[] data) : ChallengeBase<long>
{
    private readonly IEnumerable<Line> _lines = data.Select(ParseLine).ToList();
    
    protected override long Part1()
    {
        long answer = 0;

        foreach (var line in _lines)
        {
            var combinations = Combinations(line.Values.Length);
            var hasAnswer = false;

            foreach (var combination in combinations)
            {
                var localAnswer = line.Values[0];
                
                for (var i = 0; i < line.Values.Length - 1; i++)
                {
                    localAnswer = combination[i] == '+'
                        ? localAnswer + line.Values[i + 1]
                        : localAnswer * line.Values[i + 1];
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
    
    protected override long Part2()
    {
        long answer = 0;

        foreach (var line in _lines)
        {
            var combinations = Combinations(line.Values.Length);
            var hasAnswer = false;

            foreach (var combination in combinations)
            {
                var localAnswer = line.Values[0];
                
                for (var i = 0; i < line.Values.Length - 1; i++)
                {
                    localAnswer = combination[i] == '+'
                        ? localAnswer + line.Values[i + 1]
                        : localAnswer * line.Values[i + 1];
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

    private static List<string> Combinations(int noOfValues)
    {
        var noOfCombinations = GetNoOfCombinations(noOfValues);
        List<string> results = [];

        for (var i = 0; i < noOfCombinations; i++)
        {
            results.Add(Convert.ToString(i, 2));
        }
        
        var maxLength = results.Max(x => x.Length);

        return results
            .Select(x => x.PadLeft(maxLength, '0').Replace("0", "+").Replace("1", "*"))
            .ToList();
    }

    private static int GetNoOfCombinations(int arraySize)
        => arraySize switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 4,
            4 => 8,
            5 => 16,
            6 => 32,
            7 => 64,
            8 => 128,
            9 => 256,
            10 => 512,
            11 => 1024,
            12 => 2048,
            13 => 4096,
        };

    private static Line ParseLine(string dataLine)
    {
        var parts = dataLine.Split(":");
        var testValue = long.Parse(parts[0]);
        var values = parts[1].Trim().Split(" ").Select(long.Parse).ToArray();

        return new Line(testValue, values);
    }
}

public record Line(long TestValue, long[] Values);