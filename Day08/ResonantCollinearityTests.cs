using Xunit.Abstractions;

namespace AdventOfCode2024.Day08;

public class ResonantCollinearityTests(ITestOutputHelper output)
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 14)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 299)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 34)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 1032)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer, params int[] incorrectAnswers)
    {
        var data = ChallengeDataReader.GetDataForDay(8, inputType);
        
        var answer = new ResonantCollinearity(output, data).GetAnswerForPart(challengePart);
        
        Assert.DoesNotContain(incorrectAnswers, i => answer == i);
        Assert.Equal(expectedAnswer, answer);
    }
}