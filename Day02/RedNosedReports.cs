namespace AdventOfCode2024.Day02;

public class RedNosedReports(string[] data) : ChallengeBase<int>
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

        foreach (var report in data)
        {
            var levels = report.Split(" ").Select(int.Parse).ToArray();

            var levelStatus = GetLevelStatus(levels);

            if (levelStatus.All(x => x.IsSafe))
            {
                safeReports++;
            }
            else if (levelStatus.Count(x => x.IsSafe == false) == 1)
            {
                var okLevels = levelStatus.Where(x => x.IsSafe).Select(x => x.Value).ToArray();

                var newStatuses = GetLevelStatus(okLevels);

                safeReports += newStatuses.All(x => x.IsSafe) ? 1 : 0;
            }
        }
        
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
        
        var isFirstValueSafe = isIncreasing 
            ? levels[0] < levels[1] 
            : levels[1] < levels[0];

        if (isFirstValueSafe == false)
        {
            isFirstValueSafe = isIncreasing
                ? levels[0] < levels[2]
                : levels[2] < levels[0];
            
            if (isFirstValueSafe == false)
            {
                levels = levels.Skip(1).ToArray();
            }
        }
        
        levelDiffs.Add(new Level(levels[0], isFirstValueSafe));

        for (var i = 1; i < levels.Length; i++)
        {
            var isSafe = false;
            if (levels[i - 1] == levels[i])
            {
                isSafe = false;
            }
            else if (isIncreasing)
            {
                isSafe = IsSafe(levels[i], levels[i-1]);
            }
            else if (!isIncreasing)
            {
                isSafe = IsSafe(levels[i-1], levels[i]);
            }
                
            levelDiffs.Add(new (levels[i], isSafe));
        }

        return levelDiffs;
    }

    private static bool IsSafe(int val0, int val1) => (val0 - val1) is <= 3 and > 0;
}

public record Level(int Value, bool IsSafe);