using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03;

public class MullItOver(string[] data) : ChallengeBase<int>
{
    private const string MulRegex = @"mul\((\d*),(\d*)\)";
    private const string DoToken = "do()";
    private const string DoNotToken = "don't()";

    private readonly string _rawInstructions = string.Join("", data);

    protected override int Part1()
        => Regex
            .Matches(_rawInstructions, MulRegex)
            .Sum(Multi);
    

    private static int Multi(Match match)
        => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
    
    protected override int Part2()
    {
        var instructions = GetListOfInstructions(_rawInstructions);
        
        return ExecuteInstructions(instructions);
    }

    private static IEnumerable<string> GetListOfInstructions(string rawInstructions)
    {
        var position = 0;
        var matches = Regex
            .Matches(rawInstructions, MulRegex)
            .Select(m => new Tuple<int, string>(m.Index, m.Groups[0].Value));
        
        var multiesQueue = new Queue<Tuple<int, string>>(matches);
        
        var instrLookup = new Dictionary<int, string>();
        var canContinue = true;
        do
        {
            var doIndex = rawInstructions.IndexOf(DoToken, position, StringComparison.OrdinalIgnoreCase);
            if (doIndex > -1) instrLookup.TryAdd(doIndex, DoToken);
            
            var doNotIndex = rawInstructions.IndexOf(DoNotToken, position, StringComparison.OrdinalIgnoreCase);
            if (doNotIndex > -1) instrLookup.TryAdd(doNotIndex, DoNotToken);
            
            var (matchIndex, matchValue) = multiesQueue.Dequeue();
            instrLookup.TryAdd(matchIndex, matchValue);
            var nextMatchIndex = multiesQueue.Count > 0 ? multiesQueue.Peek().Item1 : -1;
            
            int[] positions = [
                doIndex, 
                doNotIndex, 
                nextMatchIndex
            ];
            canContinue = positions.Any(p => p > -1);
            if (canContinue)
            {
                position = positions.Where(p => p > -1).Min() + 1;
            }
            
        } while (canContinue);
        
        return instrLookup
            .OrderBy(x => x.Key)
            .Select(x => x.Value);
    }

    private static int ExecuteInstructions(IEnumerable<string> instructions)
    {
        var answer = 0;
        var enabled = true;
        foreach (var instruction in instructions)
        {
            if (enabled)
            {
                if (instruction.StartsWith("mul("))
                {
                    var match = Regex.Match(instruction, MulRegex);
                    answer += Multi(match);
                } 
                else if (instruction == DoNotToken)
                {
                    enabled = false;
                }
            }
            else if (instruction == DoToken)
            {
                enabled = true;
            }
        }

        return answer;
    }
}