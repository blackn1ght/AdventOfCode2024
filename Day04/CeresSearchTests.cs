namespace AdventOfCode2024.Day04;

public class CeresSearchTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 18)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 2514)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 9)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 1888)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(4, inputType);
        
        var answer = new CeresSearch(data).GetAnswerForPart(challengePart);
        
        Assert.Equal(expectedAnswer, answer);
    }
}