using Day_4;

namespace Tests;

[TestClass]
public class Day_4_Tests
{
    [TestMethod]
    public void Test_Day_4_Part_One()
    {
        var input = new List<string>()
        {
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"
        };
        var result = Day_4.Solver.Run_PartOne(input);
        var expected = 18;
        
        Assert.AreEqual(expected, result);
    }
    [TestMethod]
    public void Test_Day_4_Part_Two()
    {
        var input = new List<string>()
        {
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"
        };
        var result = Day_4.Solver.Run_PartTwo(input);
        var expected = 9;
        
        Assert.AreEqual(expected, result);
    }
}