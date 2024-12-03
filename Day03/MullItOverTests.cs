namespace AdventOfCode2024.Day03;

public class MullItOverTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 161)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 167650499)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 48)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 95846796)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(3, inputType);
        
        var answer = new MullItOver(data).GetAnswerForPart(challengePart);
        
        Assert.Equal(expectedAnswer, answer);
    }
}