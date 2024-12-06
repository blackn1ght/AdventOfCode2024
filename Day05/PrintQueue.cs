namespace AdventOfCode2024.Day05;

public class PrintQueue(string[] data) : ChallengeBase<int>
{
    private readonly List<Rule> _rules = GetRules(data.ToList());
    private readonly List<int[]> _updates = GetUpdates(data.ToList());
    
    protected override int Part1()
    {
        var answer = 0;

        foreach (var update in _updates)
        {
            var isInOrder = true;

            for (var i = 0; i < update.Length - 1; i++)
            {
                var isPageOrderRuleOk = _rules.Any(r => r.Left == update[i] && r.Right == update[i + 1]);
                if (isPageOrderRuleOk) continue;
                isInOrder = false;
                break;
            }

            if (isInOrder) answer += GetMiddleNumber(update);
        }

        return answer;
    }
    
    protected override int Part2()
    {
        var answer = 0;

        foreach (var update in _updates)
        {
            var updateModified = false;
            var hasUpdatedChanged = false;
            
            do
            {
                updateModified = false;
                for (var i = 0; i < update.Length - 1; i++)
                {
                    var isPageOrderRuleOk = _rules.Any(r => r.Left == update[i] && r.Right == update[i + 1]);
                    if (isPageOrderRuleOk == false)
                    {
                        (update[i], update[i + 1]) = (update[i + 1], update[i]);
                        updateModified = true;
                        hasUpdatedChanged = true;
                    }
                } 
            } while (updateModified);
            
            if (hasUpdatedChanged) answer += GetMiddleNumber(update);
        }

        return answer;
    }
    
    private static int GetMiddleNumber(int[] array) 
        => array[(int)Math.Floor((decimal)array.Length / 2)];

    private static List<Rule> GetRules(List<string> dataList)
    {
        var blankLine = dataList.IndexOf("");
        List<Rule> rules = [];

        for (var i = 0; i < blankLine; i++)
        {
            var ruleLine = dataList[i].Split('|').Select(int.Parse).ToArray();
            rules.Add(new Rule(ruleLine[0], ruleLine[1]));
        }

        return rules
            .OrderBy(r => r.Left)
            .ThenBy(r => r.Right)
            .ToList();
    }

    private static  List<int[]> GetUpdates(List<string> dataList)
    {
        var blankLine = dataList.IndexOf("");
        List<int[]> updates = [];

        for (var i = blankLine + 1; i < dataList.Count; i++)
        {
            updates.Add(dataList[i].Split(',').Select(int.Parse).ToArray());
        }

        return updates;

    }
}

internal record Rule(int Left, int Right);