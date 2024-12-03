using Day_1;

var input = System.IO.File.ReadAllLines("input.txt");
var partOneResult = Solver.Run_PartOne(input.ToList());
Console.WriteLine(partOneResult);



var partTwoResult = Solver.Run_PartTwo(input.ToList());
Console.WriteLine(partTwoResult);

