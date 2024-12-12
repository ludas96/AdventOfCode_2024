using Day_12;

namespace Tests;

[TestClass]
public class Day_12_Tests
{
    [TestMethod]
    public void Test_Part_One()
    {
        var input = """
                    RRRRIICCFF
                    RRRRIICCCF
                    VVRRRCCFFF
                    VVRCCCJFFF
                    VVVVCJJCFE
                    VVIVCCJJEE
                    VVIIICJJEE
                    MIIIIIJJEE
                    MIIISIJEEE
                    MMMISSJEEE
                    """;
        var result = Solver.Run_PartOne(input);
        var expected = 1930;
        
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void Test_Part_Two()
    {
        var input = """
                    RRRRIICCFF
                    RRRRIICCCF
                    VVRRRCCFFF
                    VVRCCCJFFF
                    VVVVCJJCFE
                    VVIVCCJJEE
                    VVIIICJJEE
                    MIIIIIJJEE
                    MIIISIJEEE
                    MMMISSJEEE
                    """;
        var result = Solver.Run_PartTwo(input);
        var expected = 1206;
        
        Assert.AreEqual(expected, result);
    }
}