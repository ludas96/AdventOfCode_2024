namespace Tests;

[TestClass]
public sealed class Day_1_Tests
{
    [TestMethod]
    public void Test_Day_1_Part_One()
    {
        var input = new List<string>()
        {
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3"
        };
        
        var res = Day_1.Solver.Run_PartOne(input);
        Assert.AreEqual(11, res);
    }

    [TestMethod]
    public void Test_Day_1_Part_Two()
    {
        var input = new List<string>()
        {
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3"
        };
        
        var res = Day_1.Solver.Run_PartTwo(input);
        Assert.AreEqual(31, res);
    }
}