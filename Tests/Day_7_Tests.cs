using Day_7;

namespace Tests;

[TestClass]
public class Day_7_Tests
{
    [TestMethod]
    public void Test_Day_7_Part_One()
    {
        var input = """
                    190: 10 19
                    3267: 81 40 27
                    83: 17 5
                    156: 15 6
                    7290: 6 8 6 15
                    161011: 16 10 13
                    192: 17 8 14
                    21037: 9 7 18 13
                    292: 11 6 16 20
                    """.Split('\n').ToList();
        var result = Solver.Run_PartOne(input);
        var expected = 3749;
        
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void Test_Day_7_Part_Two()
    {
        var input = """
                    190: 10 19
                    3267: 81 40 27
                    83: 17 5
                    156: 15 6
                    7290: 6 8 6 15
                    161011: 16 10 13
                    192: 17 8 14
                    21037: 9 7 18 13
                    292: 11 6 16 20
                    """.Split('\n').ToList();
        var result = Solver.Run_PartTwo(input);
        var expected = 11387;
        
        Assert.AreEqual(expected, result);
    }
}