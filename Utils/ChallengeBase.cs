namespace AdventOfCode2024.Utils;

public abstract class ChallengeBase<T>
{
    protected readonly string[] ChallengeDataRows;

    public ChallengeBase(string[] data)
    {
        ChallengeDataRows = data;
    }

    public T GetAnswerForPart(ChallengePart part)
        => part switch
        {
            ChallengePart.Part1 => Part1(),
            ChallengePart.Part2 => Part2(),
            _ => throw new ArgumentException()
        };

    protected abstract T Part1();
    protected abstract T Part2();
}