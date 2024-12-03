namespace Day_1;

public class Solver
{
    public static int Run_PartOne(List<string> input)
    {
        var leftNumbers = new List<int>();
        var rightNumbers = new List<int>();
        foreach (var line in input)
        {
            var numbers = line.Split("   ");
            var left = int.Parse(numbers[0]);
            var right = int.Parse(numbers[1]);
            leftNumbers.Add(left);
            rightNumbers.Add(right);
        }

        leftNumbers = leftNumbers.OrderBy(x => x).ToList();
        rightNumbers = rightNumbers.OrderBy(x => x).ToList();
        
        var totalDistance = 0;
        for (int i = 0; i < input.Count; i++)
        {
            var left = leftNumbers[i];
            var right = rightNumbers[i];
            var distance = Math.Abs(left - right);
            totalDistance += distance;
        }
        
        return totalDistance;
    }
    public static int Run_PartTwo(List<string> input)
    {
        var leftNumbers = new List<int>();
        var rightNumbers = new List<int>();
        foreach (var line in input)
        {
            var numbers = line.Split("   ");
            var left = int.Parse(numbers[0]);
            var right = int.Parse(numbers[1]);
            leftNumbers.Add(left);
            rightNumbers.Add(right);
        }
        
        var totalSimilarityScore = 0;
        for (int i = 0; i < leftNumbers.Count; i++)
        {
            var left = leftNumbers[i];
            var leftInRightCount = rightNumbers.Count(n => n == left);
            var similarityScore = left * leftInRightCount;
            totalSimilarityScore += similarityScore;
        }
        
        return totalSimilarityScore;
    }
}