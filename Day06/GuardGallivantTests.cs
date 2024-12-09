using Xunit.Abstractions;

namespace AdventOfCode2024.Day06;

public class GuardGallivantTests(ITestOutputHelper output)
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 41)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 4977)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 6)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 0, 389, 390)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer, params int[] wrongAnswers)
    {
        var data = ChallengeDataReader.GetDataForDay(6, inputType);
        
        var answer = new GuardGallivant(output, data).GetAnswerForPart(challengePart);
        
        if (wrongAnswers is not null)
        {
            Assert.DoesNotContain(wrongAnswers, n => n == answer);
        }
        Assert.Equal(expectedAnswer, answer);
    }
}