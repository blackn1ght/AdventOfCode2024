namespace AdventOfCode2024.Day01;

public class ChiefHistorianTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 11)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 1197984)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 31)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 23387399)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(1, inputType);
        
        var answer = new ChiefHistorian(data).GetAnswerForPart(challengePart);
        
        Assert.Equal(expectedAnswer, answer);
    }
}