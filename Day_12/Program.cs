// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Common;
using Day_12;

var sw = new Stopwatch();
sw.Start();
var input = System.IO.File.ReadAllText("input.txt");
sw.Stop();


for (int i = 0; i < 2; i++)
{
    sw.Restart();
    var partOneResult = Solver.Run_PartOne(input);
    sw.Stop();
    if(i > 0)
        Console.WriteLine($"{partOneResult} in {StopWatchHelpers.TicksToMs(sw.ElapsedTicks)} ms");

    sw.Restart();
    var partTwoResult = Solver.Run_PartTwo(input);
    sw.Stop();
    if(i > 0)
        Console.WriteLine($"{partTwoResult} in {StopWatchHelpers.TicksToMs(sw.ElapsedTicks)} ms");
}
