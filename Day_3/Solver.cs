using System.Text.RegularExpressions;

namespace Day_3;

public class Solver
{
    public static int Run_PartOne(string input)
    {
        // Run the multiplications (mul(X,Y)) from the input
        // Only run the instructions defined as; mul(number,number) and ignore anything that does not match this pattern
        // Sum the results from each instruction

        var result = 0;
        var matches = Regex.Matches(input, @"mul\((\d+)\,(\d+)\)");
        foreach (Match match in matches)
        {
            var first = int.Parse(match.Groups[1].Value);
            var second = int.Parse(match.Groups[2].Value);
            result += first * second;
        }
        return result;
    }
    
    public static int Run_PartTwo(string input)
    {
        // Run the multiplications (mul(X,Y)) from the input
        // Only run the instructions defined as; mul(number,number) and ignore anything that does not match this pattern
        // instructions following a "don't()" is ignored until a "do()" is reached - where it is enabled again.
        // Sum the results from each instruction
        
        do
        {
            var startIndex = input.IndexOf("don't()");
            var endIndex = input.Substring(startIndex).IndexOf("do()");
            if (endIndex == -1)
            {
                input = input.Substring(0, startIndex);
            }
            else
            {
                endIndex += "do()".Length + startIndex;
           
                input = input.Replace(input.Substring(startIndex, endIndex -  startIndex), "");
    
            }

        } while (input.Contains("don't()"));
            
        var result = 0;
        var matches = Regex.Matches(input, @"mul\((\d+)\,(\d+)\)");
        foreach (Match match in matches)
        {
            var first = int.Parse(match.Groups[1].Value);
            var second = int.Parse(match.Groups[2].Value);
            result += first * second;
        }
        
        return result;
    }
}