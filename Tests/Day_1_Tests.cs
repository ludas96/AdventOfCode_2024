namespace Tests;

[TestClass]
public sealed class Day_1_Tests
{
    [TestMethod]
    public void TestMethod1()
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
        
        var res = Day_1.Solver.Run(input);
        Assert.AreEqual(11, res);
    }
}