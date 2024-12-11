// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Common;
using Day_11;

var sw = new Stopwatch();
sw.Start();
sw.Stop();
var input = System.IO.File.ReadAllText("input.txt");

for (int i = 0; i < 10; i++)
{
    sw.Restart();
    var partOneResult = Solver.Run_PartOne(input);
    sw.Stop();
    
    if (i > 0)
    {
        Console.WriteLine($"{partOneResult} in {StopWatchHelpers.TicksToMs(sw.ElapsedTicks)} ms");
    }
    
    sw.Restart();
    var partTwoResult = Solver.Run_PartTwo(input);
    sw.Stop();
    if (i > 0)
    {
        Console.WriteLine($"{partTwoResult} in {StopWatchHelpers.TicksToMs(sw.ElapsedTicks)} ms");
    }
}
