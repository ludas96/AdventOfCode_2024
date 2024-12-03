namespace Day_2;

public class Solver
{
    private static bool AllNumbersAreAscOrDesc(List<int> numbers)
    {
        return numbers.SequenceEqual(numbers.Order()) || numbers.SequenceEqual(numbers.OrderDescending());
    }

    private static bool CheckAllAdjacentNumberDistances(List<int> numbers, int minDistance, int maxDistance)
    {
        bool isValid = true;
        for (int i = 0; i < numbers.Count - 1; i++)
        {
            var distance = Math.Abs(numbers[i] - numbers[i + 1]);
            if (distance < minDistance || distance > maxDistance)
            {
                isValid = false;
                break;
            }
        }
        return isValid;
    }

    private static bool ValidateNumbers(List<int> numbers)
    {
        return AllNumbersAreAscOrDesc(numbers) && CheckAllAdjacentNumberDistances(numbers, 1, 3);
    }

    public static int Run_PartOne(List<string> input)
    {
        // Example line: "7 6 4 2 1"
        // Each line must validate against the following rules:
        //  1.    All increasing or all decreasing
        //  2.    Any two adjacent numbers differs by at least one and at most three

        int safeLines = 0;
        foreach (var line in input)
        {
            var numbers = line.Split(" ").Select(x => int.Parse(x)).ToList();
            
            if(ValidateNumbers(numbers))
                safeLines++;
        }
        
        return safeLines;
    }
    
    public static int Run_PartTwo(List<string> input)
    {
        // Example line: "7 6 4 2 1"
        // Each line must validate against the following rules:
        //  1.    All increasing or all decreasing
        //  2.    Any two adjacent numbers differs by at least one and at most three
        // If the line does not validate, try ignoring one number at a time, until every number has been ignored once. 
        // If the line validates while ignoring a number, count it as valid.

        int safeLines = 0;
        foreach (var line in input)
        {
            var numbers = line.Split(" ").Select(x => int.Parse(x)).ToList();
            
            var isValid = numbers
                .Index()
                .Any(item => ValidateNumbers(numbers.Index().Where(t => t.Index != item.Index).Select(t => t.Item).ToList()));
            
           if(isValid) 
               safeLines++;
        }
        
        return safeLines;
    }
}