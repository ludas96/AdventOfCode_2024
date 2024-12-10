using Day_10;

namespace Tests;

[TestClass]
public class Day_10_Tests
{
    [TestMethod]
    public void Test_Part_One()
    {
        var input = """
                    89010123
                    78121874
                    87430965
                    96549874
                    45678903
                    32019012
                    01329801
                    10456732
                    """;
        var result = Solver.Run_PartOne(input);
        var expected = 36;
        
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void Test_Part_Two()
    {
        var input = """
                    89010123
                    78121874
                    87430965
                    96549874
                    45678903
                    32019012
                    01329801
                    10456732
                    """;
        var result = Solver.Run_PartTwo(input);
        var expected = 81;
        
        Assert.AreEqual(expected, result);
    }
}