using Day_1;

var input = System.IO.File.ReadAllLines("input.txt");
var result = Solver.Run(input.ToList());
Console.WriteLine(result);