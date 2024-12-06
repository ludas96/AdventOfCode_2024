using Day_6;

namespace Tests;

[TestClass]
public class Day_6_Tests
{
    [TestMethod]
    public void Test_Day_6_Part_One()
    {
        var input = """
                    ....#.....
                    .........#
                    ..........
                    ..#.......
                    .......#..
                    ..........
                    .#..^.....
                    ........#.
                    #.........
                    ......#...
                    """;
        var result = Solver.Run_PartOne(input);
        var expected = 41;
        
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void Test_Day_6_Part_Two()
    {
        var input = """
                    ....#.....
                    .........#
                    ..........
                    ..#.......
                    .......#..
                    ..........
                    .#..^.....
                    ........#.
                    #.........
                    ......#...
                    """;
        var result = Solver.Run_PartTwo(input);
        var expected = 6;
        
        Assert.AreEqual(expected, result);
    }
}