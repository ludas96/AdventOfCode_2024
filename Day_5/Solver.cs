namespace Day_5;

public class Solver
{
    private static bool IsValidLine(Dictionary<string, List<string>> rulesInput, string line)
    {
        var pageNumbers = line.Split(',').ToList();
        for (var i = 0; i < pageNumbers.Count; i++)
        {
            var pageNumber = pageNumbers[i];
            if (!rulesInput.TryGetValue(pageNumber, out var ruleValues)) continue;
            if (ruleValues.Any(mustBeLessThan => pageNumbers.Where((t, j) => t == mustBeLessThan && j <= i).Any()))
            {
                return false;
            }
        }
        return true;
    }

    private static string MakeLineValid(Dictionary<string, List<string>> rulesInput, string line)
    {
        var newLine = line;
        do
        {
            var pageNumbers = newLine.Split(',').ToList();
            
            for (var i = 0; i < pageNumbers.Count; i++)
            {
                var pageNumber = pageNumbers[i];
                if (!rulesInput.TryGetValue(pageNumber, out var ruleValues)) continue;
                foreach (var mustBeLessThan in ruleValues)
                {
                    for (var j = 0; j < pageNumbers.Count; j++)
                    {
                        if (pageNumbers[j] != mustBeLessThan) continue;
                        if (j > i) continue;
                        var temp = pageNumbers[i].ToString();
                        pageNumbers[i] = pageNumbers[j];
                        pageNumbers[j] = temp;
                    }
                }
            }

            newLine = string.Join(",", pageNumbers);
        } while (!IsValidLine(rulesInput, newLine));
        
        return newLine;
    }

    private static Dictionary<string, List<string>>  ParseInputRules(List<string> inputRules)
    {
        inputRules = inputRules.OrderBy(i => i.Split('|')[0]).ThenBy(i => i.Split('|')[1]).ToList();

        var rulesDict = new Dictionary<string, List<string>>();
        foreach (var rule in inputRules)
        {
            var key = rule.Split('|')[0];
            var value = rule.Split('|')[1];
            if(rulesDict.ContainsKey(key)) rulesDict[key].Add(value);
            else rulesDict.Add(key, [value]);
        }

        return rulesDict;
    }
    public static int Run_PartOne(string input)
    {
        var split = input.Split("\r\n\r\n");
        var rules = split[0].Split("\r\n").ToList();
        var updates = split[1].Split("\r\n").ToList();
        
        var rulesDict = ParseInputRules(rules);

        return updates
            .Where(line => IsValidLine(rulesDict, line))
            .Select(line => line.Split(',').ToList())
            .Select(numbers => int.Parse(numbers[(numbers.Count - 1) / 2]))
            .Sum();
    }
    public static int Run_PartTwo(string input)
    {
        var split = input.Split("\r\n\r\n");
        var rules = split[0].Split("\r\n").ToList();
        var updates = split[1].Split("\r\n").ToList();
        
        var rulesDict = ParseInputRules(rules);

        return updates
            .Where(line => !IsValidLine(rulesDict, line))
            .Select(line => MakeLineValid(rulesDict, line).Split(',').ToList())
            .Select(numbers => int.Parse(numbers[(numbers.Count - 1) / 2]))
            .Sum();
    }
}