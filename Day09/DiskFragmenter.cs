using System.Numerics;

namespace AdventOfCode2024.Day09;

public class DiskFragmenter(ITestOutputHelper output, string[] data) : ChallengeBase<ulong>
{
    protected override ulong Part1()
    {
        var result = CreateBlocks();

        result = Defrag(result);

        ulong answer = 0;

        for (var i = 0; i < result.Length; i++)
        {
            var id = ulong.Parse(result[i].ToString());
            answer += (ulong)i * id;
        }

        return answer;
    }

    private static string Defrag(string result)
    {
        while (result.Contains('.'))
        {
            var indexOfStop = result.IndexOf('.', StringComparison.Ordinal);
            if (result[^1] != '.')
            {
                var lastChar = result[^1];
                result = result.Remove(indexOfStop, 1);
                result = result.Insert(indexOfStop, lastChar.ToString());
            }
            
            result = result.Substring(0, result.Length - 1);
        }

        return result;
    }

    private string CreateBlocks()
    {
        var chunks = data[0]
            .Chunk(2)
            .Select(chunk => chunk.Select(ch => int.Parse(ch.ToString())).ToArray())
            .Select((arr, i) => new File(i, arr[0], arr.Length > 1 ? arr[1] : null))
            .ToList();

        var result = "";

        foreach (var chunk in chunks)
        {
            var id = string.Join("", Enumerable.Repeat(chunk.Id.ToString(), chunk.Blocks));
            var freeSpace = chunk.FreeSpace.HasValue ?
                string.Join("", Enumerable.Repeat(".", chunk.FreeSpace.Value))
                : "";
            result += $"{id}{freeSpace}";
        }

        return result;
    }

    protected override ulong Part2()
    {
        return 0;
    }
}

public record File(int Id, int Blocks, int? FreeSpace);

public class DiskFragmenterTests(ITestOutputHelper output)
{
    [Theory]
    [MemberData(nameof(TestCases))]
    public void ChallengeShouldGiveCorrectAnswers(TestCase testCase)
    {
        var answer = new DiskFragmenter(output, testCase.InputData).GetAnswerForPart(testCase.ChallengePart);
        
        //Assert.DoesNotContain(incorrectAnswers, i => answer == i);
        //Assert.DoesNotContain(testCase.LowIncorrectAnswers, i => answer == i);
        //Assert.DoesNotContain(testCase.HighIncorrectAnswers, i => answer == i);
        Assert.Equal(testCase.ExpectedAnswer, answer);
    }

    public static IEnumerable<object[]> TestCases()
    {
        yield return [TestCase.Part1Example()];
        yield return [TestCase.Part1Input()];
        yield return [TestCase.Part2Example()];
        yield return [TestCase.Part2Input()];
    }

    // [Theory]
    // [InlineData("12345", 60)]
    // [InlineData("2333133121414131402", 1928)]
    // public void Part1(string input, int expectedAnswer)
    // {
    //     var answer = new DiskFragmenter(output, [input]).GetAnswerForPart(ChallengePart.Part1);
    //     
    //     Assert.Equal(expectedAnswer, answer);
    // }

    public class TestCase(string testName)
    {
        public required ChallengePart ChallengePart { get; set; }
        public required string[] InputData { get; set; }
        public required ulong ExpectedAnswer { get; set; }
        //public BigInteger[] LowIncorrectAnswers { get; private set; } = [];
        //public BigInteger[] HighIncorrectAnswers { get; private set; } = [];
        
        public override string ToString() => testName;

        public static TestCase Part1Example() => new(nameof(Part1Example))
        {
            ChallengePart = ChallengePart.Part1,
            InputData = ChallengeDataReader.GetDataForDay(9, InputTypes.Example),
            ExpectedAnswer = (ulong)0
        };

        public static TestCase Part1Input() => new(nameof(Part1Input))
        {
            ChallengePart = ChallengePart.Part1,
            InputData = ChallengeDataReader.GetDataForDay(9, InputTypes.Input),
            ExpectedAnswer = 0,
            //LowIncorrectAnswers = [BigInteger.Parse("8994770654889947706548")]
        };
        
        public static TestCase Part2Example() => new(nameof(Part1Input))
        {
            ChallengePart = ChallengePart.Part2,
            InputData = ChallengeDataReader.GetDataForDay(9, InputTypes.Example),
            ExpectedAnswer = 0,
            //LowIncorrectAnswers = [BigInteger.Parse("8994770654889947706548")]
        };
        
        public static TestCase Part2Input() => new(nameof(Part1Input))
        {
            ChallengePart = ChallengePart.Part2,
            InputData = ChallengeDataReader.GetDataForDay(9, InputTypes.Input),
            ExpectedAnswer = 0,
            //LowIncorrectAnswers = [BigInteger.Parse("8994770654889947706548")]
        };
    }
}