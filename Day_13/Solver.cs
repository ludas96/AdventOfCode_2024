namespace Day_13;

public static class Solver
{
    private static long CalculateMinimumTokenCost(List<Machine> machines, bool isLimited)
    {
        var cost = 0L;
        foreach (var machine in machines)
        {
            var ax = machine.A.X * machine.B.Y;
            var ay = machine.B.X * machine.A.Y;

            var a = ax - ay;
            
            var aPrice = (machine.Price.X * machine.B.Y) - (machine.Price.Y * machine.B.X);
            
            var costA = (aPrice / a);

            var bPrice = (machine.Price.X - (machine.A.X * costA));
            var costB = bPrice / machine.B.X;

            if (isLimited && (costA > 100 || costB > 100))
                continue;
            
            if (((machine.A.X * costA) + (machine.B.X * costB)) != machine.Price.X || ((machine.A.Y * costA) + (machine.B.Y * costB)) != machine.Price.Y)
                continue;
            
            cost += ((costA * machine.TokensA) + (costB * machine.TokensB));
        }

        return cost;
    }

    private static Point ParseMachineButtonLine(string line)
    {
        var x = line.Substring(line.IndexOf(':') + 2, line.IndexOf(',') - line.IndexOf(':') - 2).Replace("X+", "");
        var y = line[(line.IndexOf(',') + 2)..].Replace("Y+", "");
        
        return new Point { X = int.Parse(x), Y = int.Parse(y) };
    }
    private static Point ParseMachinePrizeLine(string line)
    {
        var x = line.Substring(line.IndexOf(':') + 2, line.IndexOf(',') - line.IndexOf(':') - 2).Replace("X=", "");
        var y = line[(line.IndexOf(',') + 2)..].Replace("Y=", "");
        
        return new Point() { X = int.Parse(x), Y = int.Parse(y) };
    }
    private static List<Machine> ParseMachinesInput(string input)
    {
        var machines = new List<Machine>();
        var lines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

        for (var i = 0; i < lines.Count; i++)
        {
            var machineIndex = i / 3;
            var machine = machines.ElementAtOrDefault(machineIndex);
            if (machine == null)
            {
                machines.Add(new Machine(){ TokensA = 3, TokensB = 1});
                machine = machines.ElementAt(machineIndex);
            }
            
            if (lines[i].Contains('A'))
                machine.A = ParseMachineButtonLine(lines[i]);
            else if (lines[i].Contains('B'))
                machine.B = ParseMachineButtonLine(lines[i]);
            else
                machine.Price = ParseMachinePrizeLine(lines[i]);
            
        }
        return machines;
    }
    
    public static long Run_PartOne(string input)
    {
        var machines = ParseMachinesInput(input);
        var cost = CalculateMinimumTokenCost(machines, true);
        return cost;
    }

    public static long Run_PartTwo(string input)
    {
        var machines = ParseMachinesInput(input);
        foreach (var machine in machines)
        {
            machine.Price = new Point() { X = machine.Price.X + 10000000000000, Y =  machine.Price.Y + 10000000000000 };
        }
        var cost = CalculateMinimumTokenCost(machines, false);
        return cost;
    }
    
    private struct Point
    {
        public long X { get; init; }
        public long Y { get; init; }
    }
    private class Machine
    {
        public int TokensA { get; init; }
        public int TokensB { get; init; }
        
        public Point A { get; set; }
        public Point B { get; set; }
        
        public Point Price { get; set; }
    }
}