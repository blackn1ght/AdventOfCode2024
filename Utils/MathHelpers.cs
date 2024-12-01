namespace AdventOfCode2024.Utils;

public static class MathHelpers
{
    public static long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static long LeastCommonMultiple(long a, long b)
        => a / GreatestCommonDivisor(a, b) * b;
    
    public static long LeastCommonMultiple(this IEnumerable<long> values)
        => values.Aggregate(LeastCommonMultiple);
}