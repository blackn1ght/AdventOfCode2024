using Xunit.Abstractions;

namespace AdventOfCode2024.Day02;

public class RedNosedReports(ITestOutputHelper testOutputHelper, string[] data) : ChallengeBase<int>
{
    protected override int Part1()
    {
        return data
            .Select(report => report.Split(" ").Select(int.Parse).ToArray())
            .Select(levels => GetLevelStatus(levels).All(x => x.IsSafe) ? 1 : 0)
            .Sum();
    }
    
    protected override int Part2()
    {
        var safeReports = 0;
        var noOfUnsafe = 0;
        int reports = 0;

        foreach (var report in data)
        {
            var levels = report.Split(" ").Select(int.Parse).ToArray();

            var levelStatus = GetLevelStatus(levels);
            
            if (levelStatus.All(x => x.IsSafe))
            {
                safeReports++;
            }
            else if (levelStatus.Count(x => x.IsSafe == false) <= 2)
            {
                var falsePositions = levelStatus
                    .Select((s, i) => s.IsSafe == false ? i : -1)
                    .Where(i => i != -1);
                var hasSafe = false;
                foreach (var falsePosition in falsePositions)
                {
                    var anotherLevelStatus = levelStatus
                        .Where((l, i) => i != falsePosition)
                        .Select(l => l.Value)
                        .ToArray();
                    
                    var result = GetLevelStatus(anotherLevelStatus);
                    if (result.All(x => x.IsSafe))
                    {
                        safeReports++;
                        hasSafe = true;
                        break;
                    }
                }

                if (hasSafe == false)
                {
                    noOfUnsafe++;
                    //testOutputHelper.WriteLine($"{string.Join(" ", levelStatus.Select(x => x.Value))}"); 
                }
            }
            else
            {
                if (levelStatus.All(x => x.IsSafe == false) == false)
                {
                    testOutputHelper.WriteLine($"{string.Join(" ", levelStatus.Select(x => x.Value))}");
                }
                
                noOfUnsafe++;
            }

            reports++;
        }

        testOutputHelper.WriteLine($"Reports: {reports}, Unsafe: {noOfUnsafe}, Safe: {safeReports}");
        
        return safeReports;
    }

    private static List<Level> GetLevelStatus(int[] levels)
    {
        var levelDiffs = new List<Level>();

        if (levels.Length == 0) return [];
        if (levels.Length == 1) return [new(levels[0], false)];
        
        var isIncreasing = levels[^1] == levels[0] 
            ? levels[^1] > levels[1] 
            : levels[^1] > levels[0];

        if (isIncreasing == false)
        {
            levels = levels.Reverse().ToArray();
        }
        
        for (var i = 0; i < levels.Length - 1; i++)
        {
            if (levels[i + 1] - levels[i] > 3) return ReturnAllUnsafe(levels);
        }

        var hasDupes = levels
            .GroupBy(l => l)
            .Select(l => l.Count())
            .Any(l => l > 2);

        if (hasDupes) return ReturnAllUnsafe(levels);
        
        var len = levels.Length;

        levelDiffs.Add(new (levels[0], IsSafe(levels[1], levels[0])));
        for (var i = 1; i < levels.Length - 1; i++)
        {
            var isSafe = levels[i] != levels[i + 1] 
                         && levels[i] != levels[i-1] 
                         && (IsSafe(levels[i + 1], levels[i]) 
                             && IsSafe(levels[i], levels[i-1]));
            
            levelDiffs.Add(new (levels[i], isSafe));
        }
        levelDiffs.Add(new (levels[^1], IsSafe(levels[len-1], levels[len-2])));
        
        return levelDiffs;
    }
    
    private static List<Level> ReturnAllUnsafe(int[] levels) 
        => levels.Select(l => new Level(l, false)).ToList();
    
    private static bool IsSafe(int val0, int val1) => (val0 - val1) > 0;
}

public record Level(int Value, bool IsSafe);