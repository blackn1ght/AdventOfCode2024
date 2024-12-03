namespace AdventOfCode2024.Day02;

public class RedNosedReports(string[] data) : ChallengeBase<int>(data)
{
    protected override int Part1()
    {
        var answer = 0;

        foreach (var report in ChallengeDataRows)
        {
            var levels = report.Split(" ").Select(int.Parse).ToArray();

            answer += GetLevelStatus(levels).All(x => x.IsSafe) ? 1 : 0;
        }
        
        return answer;
    }

    private static List<Level> GetLevelStatus(int[] levels)
    {
        var levelDiffs = new List<Level>();
            
        var isIncreasing = levels[0] < levels[1];

        levelDiffs.Add(new Level(levels[0], isIncreasing ? IsSafe(levels[1], levels[0]) : IsSafe(levels[0], levels[1])));

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

    protected override int Part2()
    {
        var safeReports = 0;

        foreach (var report in ChallengeDataRows)
        {
            var levels = report.Split(" ").Select(int.Parse).ToArray();
            var len = levels.Length;
            var diffs = new List<(bool, int)>();

            var levelStatus = GetLevelStatus(levels);

            if (levelStatus.All(x => x.IsSafe))
            {
                safeReports++;
            }
            else if (levelStatus.Count(x => x.IsSafe == false) == 1)
            {
                var okLevels = levelStatus.Where(x => x.IsSafe == true).Select(x => x.Value).ToArray();

                var newStatuses = GetLevelStatus(okLevels);

                safeReports += newStatuses.All(x => x.IsSafe) ? 1 : 0;
            }
        }
        
        return safeReports;
    }
}

public record Level(int Value, bool IsSafe);