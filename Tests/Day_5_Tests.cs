using Day_5;

namespace Tests;

[TestClass]
public class Day_5_Tests
{
    [TestMethod]
    public void Test_Day_5_Part_One()
    {
        var input = """
                    47|53
                    97|13
                    97|61
                    97|47
                    75|29
                    61|13
                    75|53
                    29|13
                    97|29
                    53|29
                    61|53
                    97|53
                    61|29
                    47|13
                    75|47
                    97|75
                    47|61
                    75|61
                    47|29
                    75|13
                    53|13
                    
                    75,47,61,53,29
                    97,61,53,29,13
                    75,29,13
                    75,97,47,61,53
                    61,13,29
                    97,13,75,29,47
                    """;
        var result = Day_5.Solver.Run_PartOne(input);
        var expected = 143;
        
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void Test_Day_5_Part_Two()
    {
        var input = """
                    47|53
                    97|13
                    97|61
                    97|47
                    75|29
                    61|13
                    75|53
                    29|13
                    97|29
                    53|29
                    61|53
                    97|53
                    61|29
                    47|13
                    75|47
                    97|75
                    47|61
                    75|61
                    47|29
                    75|13
                    53|13

                    75,47,61,53,29
                    97,61,53,29,13
                    75,29,13
                    75,97,47,61,53
                    61,13,29
                    97,13,75,29,47
                    """;
        var result = Day_5.Solver.Run_PartTwo(input);
        var expected = 123;
        
        Assert.AreEqual(expected, result);
    }
}