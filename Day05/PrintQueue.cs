using Xunit.Sdk;

namespace AdventOfCode2024.Day05;

public class PrintQueue(string[] data) : ChallengeBase<int>
{
    protected override int Part1()
    {
        var dataList = data.ToList();
        var rules = GetRules(dataList);
        var updates = GetUpdates(dataList);

        List<int> middleNumbers = [];

        foreach (var update in updates)
        {
            var isInOrder = true;

            for (var i = 0; i < update.Length - 1; i++)
            {
                var allInThere = rules.Any(r => r.Left == update[i] && update.Contains(r.Right));
                if (allInThere == false)
                {
                    isInOrder = false;
                    break;
                }

                // var remainingPages = update.Skip(i+1).ToArray();
                // var rulesForPage = rules.Where(r => r.Left == update[i] && update.Contains(r.Right)).ToArray();
                // if (rulesForPage.Any(r => remainingPages.Contains(r.Right)) == false)
                // {
                //     isInOrder = false;
                //     break;
                // }
            }

            if (isInOrder)
            {
                var middle = (int)Math.Floor((decimal)update.Length / 2);
                middleNumbers.Add(update[middle]);
            }
        }

        return middleNumbers.Sum();
    }

    protected override int Part2()
    {
        return 0;
    }

    private List<Rule> GetRules(List<string> dataList)
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

    private List<int[]> GetUpdates(List<string> dataList)
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

public record Rule(int Left, int Right);

public class PrintQueueTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 143)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 0)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 0)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(5, inputType);
        
        var answer = new PrintQueue(data).GetAnswerForPart(challengePart);
        
        Assert.Equal(expectedAnswer, answer);
    }

    [Theory]
    [InlineData("75,97,47,61,53")]
    [InlineData("61,13,29")]
    [InlineData("97,13,75,29,47")]
    public void Part1ShouldReturnZero(string row)
    {
        var answer = new PrintQueue([row]).GetAnswerForPart(ChallengePart.Part1);

        Assert.Equal(0, answer);
    }
}