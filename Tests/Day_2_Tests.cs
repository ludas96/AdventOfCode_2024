using Day_2;

namespace Tests;

[TestClass]
public class Day_2_Tests
{
    [TestMethod]
    public void Test_Day_2_Part_One()
    {
        var input = new List<string>()
        {
            "7 6 4 2 1",
            "1 2 7 8 9",
            "9 7 6 2 1",
            "1 3 2 4 5",
            "8 6 4 4 1",
            "1 3 6 7 9"
        };
        var res = Solver.Run_PartOne(input);
        Assert.AreEqual(2, res);
    }
    
    [TestMethod]
    public void Test_Day_2_Part_Two()
    {
        var input = new List<string>()
        {
            "7 6 4 2 1",
            "1 2 7 8 9",
            "9 7 6 2 1",
            "1 3 2 4 5",
            "8 6 4 4 1",
            "1 3 6 7 9"
        };
        var res = Solver.Run_PartTwo(input);
        Assert.AreEqual(4, res);
    }
}