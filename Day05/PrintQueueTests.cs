namespace AdventOfCode2024.Day05;

public class PrintQueueTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 143)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 5374)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 123)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 4260)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(5, inputType);
        
        var answer = new PrintQueue(data).GetAnswerForPart(challengePart);
        
        Assert.Equal(expectedAnswer, answer);
    }

    [Theory]
    [InlineData("75,47,61,53,29", 61)]
    [InlineData("97,61,53,29,13", 53)]
    [InlineData("75,29,13", 29)]
    [InlineData("75,97,47,61,53", 0)]
    [InlineData("61,13,29", 0)]
    [InlineData("97,13,75,29,47", 0)]
    public void Part1_IndividualUpdate_ShouldGiveCorrectAnswer(string row, int expectedValue)
    {
        var data = ExampleRules.Union([row]).ToArray();
        var queue = new PrintQueue(data);
        var answer = queue.GetAnswerForPart(ChallengePart.Part1);

        Assert.Equal(expectedValue, answer);
    }
    
    [Theory]
    [InlineData("75,47,61,53,29", 0)]
    [InlineData("97,61,53,29,13", 0)]
    [InlineData("75,29,13", 0)]
    [InlineData("75,97,47,61,53", 47)]
    [InlineData("61,13,29", 29)]
    [InlineData("97,13,75,29,47", 47)]
    public void Part2_IndividualUpdate_ShouldGiveCorrectAnswer(string row, int expectedValue)
    {
        var data = ExampleRules.Union([row]).ToArray();
        var queue = new PrintQueue(data);
        var answer = queue.GetAnswerForPart(ChallengePart.Part2);

        Assert.Equal(expectedValue, answer);
    }

    private static string[] ExampleRules =>
    [
        "47|53",
        "97|13",
        "97|61",
        "97|47",
        "75|29",
        "61|13",
        "75|53",
        "29|13",
        "97|29",
        "53|29",
        "61|53",
        "97|53",
        "61|29",
        "47|13",
        "75|47",
        "97|75",
        "47|61",
        "75|61",
        "47|29",
        "75|13",
        "53|13",
        "",
    ];
}