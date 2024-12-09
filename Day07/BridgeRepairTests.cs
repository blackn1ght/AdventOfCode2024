namespace AdventOfCode2024.Day07;

public class BridgeRepairTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 3749)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 1708857123053)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 11387)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, long expectedAnswer, params long[] incorrectAnswers)
    {
        var data = ChallengeDataReader.GetDataForDay(7, inputType);
        
        var answer = new BridgeRepair(data).GetAnswerForPart(challengePart);
        
        Assert.DoesNotContain(incorrectAnswers, i => answer == i);
        Assert.Equal(expectedAnswer, answer);
    }
}