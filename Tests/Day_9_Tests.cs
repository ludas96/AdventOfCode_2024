using Day_9;

namespace Tests;

[TestClass]
public class Day_9_Tests
{
    [TestMethod]
    public void Test_Part_One()
    {
        var input = "2333133121414131402";
        var result = Solver.Run_PartOne(input);
        var expected = 1928;
        
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void Test_Part_Two()
    {
        var input = "2333133121414131402";
        var result = Solver.Run_PartTwo(input);
        var expected = 2858;
        
        Assert.AreEqual(expected, result);
    }
}