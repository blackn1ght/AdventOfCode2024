namespace AdventOfCode2024.Day02;

public class RedNosedReportsTests
{
    [Theory]
    [InlineData(ChallengePart.Part1, InputTypes.Example, 2)]
    [InlineData(ChallengePart.Part1, InputTypes.Input, 639)]
    [InlineData(ChallengePart.Part2, InputTypes.Example, 4)]
    [InlineData(ChallengePart.Part2, InputTypes.Input, 0)]
    public void ChallengeShouldGiveCorrectAnswers(ChallengePart challengePart, InputTypes inputType, int expectedAnswer)
    {
        var data = ChallengeDataReader.GetDataForDay(2, inputType);
        
        var answer = new RedNosedReports(data).GetAnswerForPart(challengePart);
        
        Assert.Equal(expectedAnswer, answer);
    }

    [Fact]
    public void ICannotDoPart2()
    {
        string[] data =
        [
            "22 25 27 28 30 31 32 29",  // true
            "72 74 75 77 80 81 81",     // true
            "52 53 55 58 59 63",        // false
            "14 17 19 22 27",           // false
            "65 68 67 68 71 73 76 77",  // true
            "53 56 53 55 54",           // false
            "60 62 59 62 62",           // false
            "27 30 28 31 32 35 39",     // false
            "64 67 68 71 74 71 74 81",  // false
            "29 32 32 33 36 39 40",     // true
            "72 74 74 75 74",           // false
            "11 14 14 17 17",           // false
            "64 65 67 70 72 72 76",     // false
            "77 79 80 80 82 89",        // false
            "45 47 49 53 54",           // false
            "13 16 18 22 23 25 27 24",  // false
            "16 17 18 19 20 21 25 25",  // false
            "25 26 30 32 34 35 36 40",  // false
            "45 48 52 54 56 62",        // false
            "51 52 54 56 59 64 66 68",  // false
            "14 16 17 22 23 22",        // false
            "27 28 34 35 35",           // false
            "76 77 78 80 85 88 92",     // false
            "72 74 79 80 81 88",        // false
            "63 62 65 66 69 70 72",     // true
            "87 84 87 89 88",           // false
            "41 40 42 43 45 46 46",     // false
            "7 4 7 8 12",               // false
            "86 83 85 88 90 96",        // false
            "24 21 22 24 26 28 27 28",  // false
            "25 24 22 24 25 23",        // false
            "37 36 38 39 37 39 39",     // false
            "78 77 80 79 80 82 86",     // false
            "32 30 33 35 34 35 42",     // false
            "70 68 70 73 73 76",        // false
            "60 58 59 60 61 61 59",     // false
            "85 83 86 86 86",           // false
            "3 1 1 2 4 8",              // false
            "21 18 19 19 26",           // false
            "59 56 57 59 63 64",        // false
            "48 45 47 48 52 53 50",     // false
            "43 40 44 47 49 52 52",     // false
            "48 45 47 51 55",           // false
            "54 51 52 54 57 58 62 67",  // false
            "21 19 20 22 29 31",        // false
            "32 29 31 34 39 41 44 41",  // false
            "47 46 48 49 54 57 57",     // false
            
            "1 3 2 4 5",            // true
        ];
        
        // the answer is not 657 (it's too low)
        // the answer is not 658 but is the answer for a different input
        //        "          659

        var answer = new RedNosedReports(data).GetAnswerForPart(ChallengePart.Part2);
        
        Assert.Equal(5, answer);
    }

    [Fact]
    public void IndividualRowPart2()
    {
        string[] data = ["1 3 2 4 5"];
        
        var answer = new RedNosedReports(data).GetAnswerForPart(ChallengePart.Part2);

        Assert.Equal(1, answer);
    }
}