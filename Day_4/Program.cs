// See https://aka.ms/new-console-template for more information

using Day_4;

var input = System.IO.File.ReadAllLines("input.txt").ToList();
var partOneResult = Solver.Run_PartOne(input);
Console.WriteLine(partOneResult);

var partTwoResult = Solver.Run_PartTwo(input);
Console.WriteLine(partTwoResult);