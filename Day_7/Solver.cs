using System.Collections.Concurrent;

namespace Day_7;

public class Solver
{

    private enum Operator
    {
        Add,
        Mult,
        Concat
    }

    private static List<Operator[]> GetOperatorCombinations(int count, List<Operator> operators)
    {
        var result = new List<Operator[]>();
        GenerateCombinations(operators.ToArray(), count, new Operator[count], result, 0);
        return result;
    }

    private static void GenerateCombinations(Operator[] operators, int count, Operator[] current, List<Operator[]> result, int index)
    {
        if (index == count)
        {
            result.Add((Operator[])current.Clone());
            return;
        }

        foreach (var op in operators)
        {
            current[index] = op;
            GenerateCombinations(operators, count, current, result, index + 1);
        }
    }
    
    
    private static List<(long Value, List<long> Numbers)> ParseInput(List<string> data)
    {
        List<(long val, List<long> numbers)> result = new();
        foreach (var line in data)
        {
            var value = Int64.Parse(line.Substring(0, line.IndexOf(':')));
            var numbers = line[(line.IndexOf(':') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => Int64.Parse(s.Trim())).ToList();
            result.Add((val: value, numbers: numbers));
        }

        return result;
    }
    public static long Run_PartOne(List<string> input)
    {
        var data = ParseInput(input);

        long totalValue = 0;
        foreach (var line in data)
        {
            long expectedValue = line.Value;
            var numbers = line.Numbers;

            var numberOfOperators = numbers.Count - 1;
            var combinations = GetOperatorCombinations(numberOfOperators, [Operator.Add, Operator.Mult]);
            

            for (int i = 0; i < combinations.Count; i++)
            {
                long val = 0;
                var operators = combinations.ElementAt(i);
                for (int j = 0; j < operators.Length; j++)
                {
                    var numberBefore = numbers[j];
                    var numberAfter = numbers[j + 1];
                    var currentOp = operators[j];
                    if (j == 0)
                    {
                        if (currentOp == Operator.Add)
                            val = numberBefore + numberAfter;
                        else if (currentOp == Operator.Mult)
                            val = numberBefore * numberAfter;
                    }
                    else
                    {
                        if (currentOp == Operator.Add)
                            val += numberAfter;
                        else if (currentOp == Operator.Mult)
                            val *= numberAfter;
                    }
                }

                if (val == expectedValue)
                {
                    totalValue += val;
                    break;
                }
            }

        }

        return totalValue;
    }
    
    public static long Run_PartTwo(List<string> input)
    {
        var data = ParseInput(input);
        
        long totalValue = 0;
        foreach (var line in data)
        {
            long expectedValue = line.Value;
            var numbers = line.Numbers;

            var numberOfOperators = numbers.Count - 1;
           
            var combinations = GetOperatorCombinations(numberOfOperators,
                [Operator.Add, Operator.Mult, Operator.Concat]);
            
            for (int i = 0; i < combinations.Count; i++)
            {
                long val = 0;
                var operators = combinations.ElementAt(i);
                for (int j = 0; j < operators.Length; j++)
                {
                    var numberBefore = numbers[j];
                    var numberAfter = numbers[j + 1];
                    var currentOp = operators[j];
                    if (j == 0)
                    {
                        if (currentOp == Operator.Add)
                            val = numberBefore + numberAfter;
                        else if (currentOp == Operator.Mult)
                            val = numberBefore * numberAfter;
                        else if (currentOp == Operator.Concat)
                            val = long.Parse($"{numberBefore}{numberAfter}");
                    }
                    else
                    {
                        if (currentOp == Operator.Add)
                            val += numberAfter;
                        else if (currentOp == Operator.Mult)
                            val *= numberAfter;
                        else if (currentOp == Operator.Concat)
                            val = long.Parse($"{val}{numberAfter}");
                        
                    }
                }

                if (val == expectedValue)
                {
                    totalValue += val;
                    break;
                }
            }

        }

        return totalValue;
    }
}