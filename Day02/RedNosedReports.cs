namespace AdventOfCode2024.Day02;

public class RedNosedReports(string[] data) : ChallengeBase<int>(data)
{
    protected override int Part1()
    {
        var answer = 0;

        foreach (var report in ChallengeDataRows)
        {
            var levels = report.Split(" ").Select(int.Parse).ToArray();
            var levelDiffs = new List<bool>();
            
            var isIncreasing = levels[0] < levels[1];
            for (var i = 0; i < levels.Length - 1; i++)
            {
                var isSafe = false;
                if (levels[i] == levels[i + 1])
                {
                    isSafe = false;
                }
                else if (isIncreasing)
                {
                    isSafe = (levels[i + 1] - levels[i]) is <= 3 and > 0;
                }
                else if (!isIncreasing)
                {
                    isSafe = (levels[i] - levels[i + 1]) is <= 3 and > 0;
                }
                
                levelDiffs.Add(isSafe);
            }

            answer += levelDiffs.All(x => x) ? 1 : 0;
        }
        
        return answer;
    }

    protected override int Part2()
    {
        var safeReports = 0;

        foreach (var report in ChallengeDataRows)
        {
            var levels = report.Split(" ").Select(int.Parse).ToArray();
            var diffs = new List<(bool, int)>();

            for (var i = 0; i < levels.Length - 1; i++)
            {
                diffs.Add((levels[i + 1] - levels[i] is <= 3 and > 0, levels[i]));
            }
            
            /*
            List<int> levelDiffs = [levels[1] - levels[0]];


            for (var i = 1; i < levels.Length - 1; i++)
            {
                levelDiffs.Add(levels[i+1] - levels[i]);
            }

            // invert the numbers if negatives
            if (levelDiffs.Sum() < 0)
            {
                levelDiffs = levelDiffs.Select(n => n * -1).ToList();
            }

            if (levelDiffs.All(n => n <= 3) && levelDiffs.Count(n => n < 1) <= 1)
            {
                safeReports += 1;
            }
            */
   
        }
        
        return safeReports;
    }
}