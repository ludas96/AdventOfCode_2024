// See https://aka.ms/new-console-template for more information


using Day_6;

var input = System.IO.File.ReadAllText("input.txt");
var partOneResult = Solver.Run_PartOne(input);
Console.WriteLine(partOneResult);

var partTwoResult = Solver.Run_PartTwo(input);
Console.WriteLine(partTwoResult);