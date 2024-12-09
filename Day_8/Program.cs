// See https://aka.ms/new-console-template for more information

using Day_8;
using System.Diagnostics;

var sw = new Stopwatch();
sw.Start();
var input = System.IO.File.ReadAllText("input.txt");
/*var partOneResult = Solver.Run_PartOne(input);
sw.Stop();
Console.WriteLine($"{partOneResult} in {sw.ElapsedMilliseconds} ms");*/

sw.Restart();
var partTwoResult = Solver.Run_PartTwo(input);
sw.Stop();
Console.WriteLine($"{partTwoResult} in {sw.ElapsedMilliseconds} ms");