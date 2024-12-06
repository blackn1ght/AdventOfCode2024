using Xunit.Abstractions;

namespace AdventOfCode2024.Day02;

public class RedNosedReportsTests(ITestOutputHelper testOutputHelper)
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 2)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 639)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 4)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(2, inputType);
        
        var answer = new RedNosedReports(testOutputHelper, data).GetAnswerForPart(challengePart);
        
        if (challengePart == ChallengePart.Part2 && inputType == InputTypes.Input)
        {
            AssertPart2InputIsNot(answer);
        }
        Assert.Equal(expectedAnswer, answer);
    }

    public static IEnumerable<object[]> UnsuafeRows()
    {
        yield return ["1 2 7 8 9"];
        
        yield return ["53 56 53 55 54"];
        yield return ["60 62 59 62 62"];
        yield return ["27 30 28 31 32 35 39"];
        yield return ["64 67 68 71 74 71 74 81"];
        
        yield return ["4 4 4 7 9 11 13 19"];
    }

    public static IEnumerable<object[]> SafeRows()
    {
        yield return ["84 87 89 88"];
        yield return ["27 24 24 21"];
        yield return ["22 25 27 28 30 31 32 29"];
        yield return ["62 63 62 64 65"];
        
        yield return ["1 3 2 4 5"];
        yield return ["65 68 67 68 71 73 76 77"];
        yield return ["63 62 65 66 69 70 72"];
        yield return ["5 3 4 6 8 10"];
        yield return ["10 8 6 4 3 5"];
        yield return ["63 62 64 65 66 69 70 72"];

        yield return ["14 15 16 18 21 23 26"];
        yield return ["39 37 35 34 33 30"];
        yield return ["34 31 30 29 28"];
        yield return ["12 15 18 21 23 26 28"];
        yield return ["21 20 17 15 13"];
        yield return ["87 88 90 93 95"];
        yield return ["2 3 4 7 9 10"];
        yield return ["43 46 49 52 55 56 57 58"];
        yield return ["41 44 47 48 50 53"];
        yield return ["33 31 28 27 24 22 19"];
        yield return ["36 35 32 31 28"];
        yield return ["93 91 90 89 86 85 82 80"];
    }

    [Theory]
    [MemberData(nameof(UnsuafeRows))]
    public void Part2_TheesShouldNotBeSafe(string unsafeRow)
    {
        var answer = new RedNosedReports(testOutputHelper, [unsafeRow]).GetAnswerForPart(ChallengePart.Part2);
        
        Assert.True(answer == 0);
    }
    
    [Theory]
    [MemberData(nameof(SafeRows))]
    public void Part2_TheesShouldBeSafe(string row)
    {
        var answer = new RedNosedReports(testOutputHelper, [row]).GetAnswerForPart(ChallengePart.Part2);
        
        Assert.True(answer == 1);
    }

    private static void AssertPart2InputIsNot(int result)
    {
        int[] incorrectAnswers =
        [
            654,
            656,
            657,
            658,
            659,
            660,
            661,
            662,
            663,
            664,
            665,
            668,
            723,
            857,
            904,
            989
        ];
        
        Assert.DoesNotContain(incorrectAnswers, x => x == result);
    }
}