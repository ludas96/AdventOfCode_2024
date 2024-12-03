using Day_3;

namespace Tests;

[TestClass]
public class Day_3_Tests
{
    [TestMethod]
    public void Test_Day_3_Part_One()
    {
       // Run the multiplications (mul(X,Y)) from the input
       // Only run the instructions defined as; mul(number,number) and ignore anything that does not match this pattern
       // Sum the results from each instruction

       var input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
       var expected = 161;

       var result = Day_3.Solver.Run_PartOne(input);
       
       Assert.AreEqual(expected, result);
    }
    
    [TestMethod]
    public void Test_Day_3_Part_Two()
    {
        // Run the multiplications (mul(X,Y)) from the input
        // Only run the instructions defined as; mul(number,number) and ignore anything that does not match this pattern
        // instructions following a "don't()" is ignored until a "do()" is reached - where it is enabled again.
        // Sum the results from each instruction

        var input = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"; 
        var expected = 48;

        var result = Day_3.Solver.Run_PartTwo(input);
       
        Assert.AreEqual(expected, result);
    }
}