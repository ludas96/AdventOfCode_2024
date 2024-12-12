namespace Day_12;

public static class Solver
{

    private enum Direction { Up, Down, Left, Right }

    private static Direction GetOppositeDirection(Direction dir) => dir switch
    {
        Direction.Up => Direction.Down,
        Direction.Down => Direction.Up,
        Direction.Left => Direction.Right,
        Direction.Right => Direction.Left,
        _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
    };

    private readonly struct Coordinate(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }
    
    private static Coordinate GetCoordAtDirection(Coordinate coord, Direction dir) => dir switch
    {
        Direction.Up => new Coordinate(coord.X, coord.Y - 1),
        Direction.Down => new Coordinate(coord.X, coord.Y + 1),
        Direction.Left => new Coordinate(coord.X - 1, coord.Y),
        Direction.Right => new Coordinate(coord.X + 1, coord.Y),
        _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
    };

    private static bool IsDirValid(Coordinate coord, Direction dir, int width, int height) =>  dir switch
    {
        Direction.Up => coord.Y - 1 >= 0,
        Direction.Down => coord.Y + 1 < height,
        Direction.Left => coord.X - 1 >= 0,
        Direction.Right => coord.X + 1 < width,
        _ => false
    };

    private static Region? GetRegionAtDirection(List<Region> regions, Coordinate coordinate, Direction direction)
    {
        return regions.FirstOrDefault(region => region.ContainsPosition(GetCoordAtDirection(coordinate, direction)));
    }
    
    private static List<Region> GenerateRegions(string[] lines)
    {
        var height = lines.Length;
        var width = lines[0].Length;
        var dirs = Enum.GetValues<Direction>();

        int currentRegionId = 0;
        
        var regions = new List<Region>();
        // First pass to generate a rough list of regions
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var coordinate = new Coordinate(x, y);
                var type = lines[y][x];
                var foundRegion = false;
                foreach (var dir in dirs)
                {
                    if (!IsDirValid(coordinate, dir, width, height))
                        continue;
                    
                    var neighborRegion = GetRegionAtDirection(regions, coordinate, dir);
                    if (neighborRegion == null) continue;
                    if (neighborRegion.PlotType != type) continue;
                    
                    neighborRegion.AddPlot(coordinate);
                        
                    foundRegion = true;
                    break;
                }

                if (foundRegion) continue;
                
                var region = new Region(type, currentRegionId);
                currentRegionId++;
                region.AddPlot(coordinate);
                regions.Add(region);
            }
        }
        
        // Second pass to merge regions of same type
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var coordinate = new Coordinate(x, y);
                var type = lines[y][x];
                var region = regions.FirstOrDefault(region => region.ContainsPosition(coordinate));
                if (region == null)
                {
                    Console.WriteLine($"Region {coordinate} not found");
                    continue;
                }

                var isMerged = false;
                foreach (var dir in dirs)
                {
                    if (!IsDirValid(coordinate, dir, width, height))
                        continue;
                    
                    var neighborRegion = GetRegionAtDirection(regions, coordinate, dir);
                    if (neighborRegion == null) continue;
                    if (neighborRegion.PlotType != type) continue;
                    if (neighborRegion.Id == region.Id) continue;
                    
                    // Merge regions
                    foreach (var plot in region.Plots ?? [])
                    {
                        neighborRegion.AddPlot(plot.Coordinate);
                    }

                    isMerged = true;

                }
                if(!isMerged) continue;
                
                regions = regions.Where(r => r.Id != region.Id).ToList();
            }
        }

        return regions;
    }
    
    public static int Run_PartOne(string input)
    {
        var lines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        var regions = GenerateRegions(lines);
        var fenceCost = regions.Sum(r => r.FenceCost);
        return fenceCost;
    }
    
    public static int Run_PartTwo(string input)
    {
        var lines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        var regions = GenerateRegions(lines);
        var fenceCost = regions.Sum(r => r.FenceCostDiscounted);
        return fenceCost;
    }
    
    private class Plot
    {
        public Coordinate Coordinate { get; init; }
        
        public int NumEdges { get; set; }
        public List<Direction> Edges { get; set; } = [Direction.Down, Direction.Left, Direction.Right, Direction.Up];
    }

    private class Region(char type, int id)
    {
        public int Id { get; set; } = id;
        public char PlotType { get; init; } = type;

        public int NumberOfPlots => Plots?.Count ?? 0;
        
        public int FenceCost => Plots?.Count * GetPerimeter ?? 0; // Area * Edges = Fence Cost
        public int FenceCostDiscounted => Plots?.Count * GetNumberOfSides() ?? 0;
        public int GetPerimeter => Plots?.Select(p => p.NumEdges).Sum() ?? 0;
        public List<Plot>? Plots { get; private set; }
        
        public bool ContainsPosition(Coordinate coordinate) =>
            Plots?.Any(p => p.Coordinate.X == coordinate.X && p.Coordinate.Y == coordinate.Y) ?? false;
        
        public void AddPlot(Coordinate coord)
        {
            var numEdges = 0;
            var dirs = new List<Direction>();
            foreach (var dir in Enum.GetValues<Direction>())
            {
                var otherPlot = GetPlotAt(coord, dir);
                if (otherPlot == null)
                {
                   
                    numEdges++;
                    dirs.Add(dir);
                }
                else
                {
                    otherPlot.NumEdges--;
                    otherPlot.Edges.Remove(GetOppositeDirection(dir));
                }
            }
            
            Plots ??= [];
            
            Plots.Add(new Plot()
            {
                Coordinate = coord,
                NumEdges = numEdges,
                Edges = dirs
            });
        }
        
        private Plot? GetPlotAt(Coordinate coordinate, Direction direction)
        {
            var coord = GetCoordAtDirection(coordinate, direction);
            var plot = Plots?.FirstOrDefault(p => p.Coordinate.X == coord.X && p.Coordinate.Y == coord.Y);
            return plot;
        }
        
        private int GetNumberOfSides()
        {
            var sides = 0;

            foreach (var plot in Plots.Where(p => p.NumEdges > 0))
            {
                var plotSides = 0;
                var leftPlot = GetPlotAt(plot.Coordinate, Direction.Left);
                var topPlot = GetPlotAt(plot.Coordinate, Direction.Up);
                if (plot.Edges.Contains(Direction.Left))
                {
                    if (topPlot != null)
                    {
                        if (!topPlot.Edges.Contains(Direction.Left))
                            plotSides++;
                    }
                    else
                    {
                        plotSides++;
                    }
                }
                
                if (plot.Edges.Contains(Direction.Up))
                {
                    if (leftPlot != null)
                    {
                        if (!leftPlot.Edges.Contains(Direction.Up))
                            plotSides++;
                    }
                    else
                    {
                        plotSides++;
                    }
                }
                if (plot.Edges.Contains(Direction.Right))
                {
                    if (topPlot != null)
                    {
                        if (!topPlot.Edges.Contains(Direction.Right))
                            plotSides++;
                    }
                    else
                    {
                        plotSides++;
                    }
                }
                if (plot.Edges.Contains(Direction.Down))
                {
                    if (leftPlot != null)
                    {
                        if (!leftPlot.Edges.Contains(Direction.Down))
                            plotSides++;
                    }
                    else
                    {
                        plotSides++;
                    }
                }
                
                sides += plotSides;
            }
            
            return sides;
        }
    }
}