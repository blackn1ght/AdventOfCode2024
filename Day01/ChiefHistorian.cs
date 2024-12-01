namespace AdventOfCode2024.Day01;

public class ChiefHistorian(string[] data) : ChallengeBase<int>(data)
{
    protected override int Part1()
    {
        var (col1, col2) = ParseData();
        
        col1.Sort();
        col2.Sort();

        var answer = 0;

        for (var i = 0; i < col1.Count; i++)
        {
            answer += Math.Abs(col1[i] - col2[i]);
        }

        return answer;
    }

    protected override int Part2()
    {
        var (col1, col2) = ParseData();

        var cache = new Dictionary<int, int>();
        var answer = 0;

        foreach (var col1Val in col1)
        {
            if (!cache.TryGetValue(col1Val, out int value))
            {
                var count = col2.Count(col2Val => col2Val == col1Val);
                value = col1Val * count;
                cache.Add(col1Val, value);
            }
            
            answer += value;
        }

        return answer;
    }

    private (List<int> col1, List<int> col2) ParseData()
    {
        List<int> col1 = [];
        List<int> col2 = [];
        foreach (var line in ChallengeDataRows)
        {
            var numbers = line.Split("   ").Select(int.Parse).ToArray();
            col1.Add(numbers[0]);
            col2.Add(numbers[1]);
        }

        return (col1, col2);
    }
}