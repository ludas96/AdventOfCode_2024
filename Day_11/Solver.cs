namespace Day_11;

public class Solver
{

    private static long GetNumberOfDigits(long n)
    {
        return n > 0 ? (long)Math.Log10((double)n) + 1 : 1;
    }
    
    private static (long a, long b) ExecuteRules(long number)
    {
        if(number == 0)
            return (1, -1);
        
        var numberOfDigits = GetNumberOfDigits(number);
        if (numberOfDigits % 2 == 0)
        {
            var divisor = (int)Math.Pow(10, numberOfDigits * 0.5);
            var firstHalf = number / divisor;
            var secondHalf = number % divisor;
            return (firstHalf, secondHalf);
        }
            
        return (number * 2024, -1);
    }

    private static void AddOrIncrement(Dictionary<long, long> dict, long key, long value)
    {
        if (dict.TryGetValue(key, out var val))
        {
            dict[key] = val + value;
        }
        else
        {
            dict.Add(key, value);
        }
    }

    private static Dictionary<long, long> GetNewValues(Dictionary<long, long> prevValues, Dictionary<long, (long a, long b)> cachedRuleValues)
    {
        var newValues = new Dictionary<long, long>();
        foreach (var kvp in prevValues)
        {
            if (cachedRuleValues.TryGetValue(kvp.Key, out var val))
            {
                if (val.b == -1)
                {
                    AddOrIncrement(newValues, val.a, kvp.Value);
                }
                else
                {
                    AddOrIncrement(newValues, val.a, kvp.Value);
                    AddOrIncrement(newValues, val.b, kvp.Value);
                }
            }
            else
            {
                var res = ExecuteRules(kvp.Key);
                if (res.b == -1)
                {
                    AddOrIncrement(newValues, res.a, kvp.Value);
                }
                else
                {
                    AddOrIncrement(newValues, res.a, kvp.Value);
                    AddOrIncrement(newValues, res.b, kvp.Value);
                }
                cachedRuleValues.Add(kvp.Key, res);
            }
            
        }
        return newValues;
    }

    private static long GenerateStones(List<int> numbers, int iterations)
    {
        var dict = new Dictionary<long, long>();
        var cache = new Dictionary<long, (long, long)>();

        foreach (var number in numbers)
        {
            if (dict.TryGetValue(number, out var value))
            {
                dict[number] = value + 1;
            }
            else
            {
                dict.Add(number, 1);
            }
        }

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

        var numberOfStones = GenerateStones(numbers, 25);

        return numberOfStones;
    }
    
    public static long Run_PartTwo(string input)
    {
        var numbers = input.Split(" ").Select(int.Parse).ToList();

        var numberOfStones = GenerateStones(numbers, 75);

        return numberOfStones;
    }
}