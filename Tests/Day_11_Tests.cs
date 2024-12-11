using Day_11;

namespace Tests;

[TestClass]
public class Day_11_Tests
{
    [TestMethod]
    public void Test_Part_One()
    {
        var input = "125 17";
        var result = Solver.Run_PartOne(input);
        var expected = 55312;
        
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void Test_Part_Two()
    {
        var input = "125 17";
        var result = Solver.Run_PartTwo(input);
        var expected = 55312;
        
        Assert.AreEqual(expected, result);
    }
}