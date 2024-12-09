﻿// See https://aka.ms/new-console-template for more information

using Day_9;
using System.Diagnostics;

var sw = new Stopwatch();
sw.Start();
sw.Stop();
for (int i = 0; i < 2; i++)
{
    sw.Restart();
    var input = System.IO.File.ReadAllText("input.txt");
    var partOneResult = Solver.Run_PartOne(input);
    sw.Stop();
    if(i > 0)
        Console.WriteLine($"{partOneResult} in {sw.ElapsedMilliseconds} ms");

    sw.Restart();
    var partTwoResult = Solver.Run_PartTwo(input);
    sw.Stop();
    if(i > 0)
        Console.WriteLine($"{partTwoResult} in {sw.ElapsedMilliseconds} ms");
}
