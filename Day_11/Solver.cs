using System.Collections.Concurrent;

namespace Day_11;

public static class Solver
{
    private static long GetNumberOfDigits(long n) => n > 0 ? (long)Math.Log10(n) + 1 : 1;

    private static RuleResult ExecuteRules(long number)
    {
        if (number == 0)
            return new RuleResult(1, -1);

        var numberOfDigits = GetNumberOfDigits(number);
        if (numberOfDigits % 2 != 0) return new RuleResult(number * 2024, -1);
        
        var divisor = (long)Math.Pow(10, numberOfDigits * 0.5);
        return new RuleResult(number / divisor, number % divisor);
    }

    private static Dictionary<long, long> GetNewValues(Dictionary<long, long> prevValues,
        Dictionary<long, RuleResult> cachedRuleValues)
    {
        var newValues = new Dictionary<long, long>();
        foreach (var kvp in prevValues)
        {
            if (!cachedRuleValues.TryGetValue(kvp.Key, out var val))
            {
                val = ExecuteRules(kvp.Key);
                cachedRuleValues[kvp.Key] = val;
            }

            if (newValues.TryGetValue(val.A, out _))
                newValues[val.A] += kvp.Value;
            else
                newValues[val.A] = kvp.Value;


            if (val.B == -1) continue;

            if (newValues.TryGetValue(val.B, out _))
                newValues[val.B] += kvp.Value;
            else
                newValues[val.B] = kvp.Value;
        }

        return newValues;
    }

    private static long GenerateStones(List<int> numbers, int iterations)
    {
        var dict = numbers.GroupBy(n => (long)n)
            .ToDictionary(group => group.Key, group => (long)group.Count());

        var cache = new Dictionary<long, RuleResult>();

        for (var i = 0; i < iterations; i++)
        {
            var output = GetNewValues(dict, cache);
            dict = output;
        }

        return dict.Values.Sum();
    }

    public static long Run_PartOne(string input)
    {
        var numbers = input.Split(" ").Select(int.Parse).ToList();

        return GenerateStones(numbers, 25);
    }

    public static long Run_PartTwo(string input)
    {
        var numbers = input.Split(" ").Select(int.Parse).ToList();

        return GenerateStones(numbers, 75);
    }

    private readonly struct RuleResult(long a, long b)
    {
        public long A { get; } = a;
        public long B { get; } = b;
    }
}