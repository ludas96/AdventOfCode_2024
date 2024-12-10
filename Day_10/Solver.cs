using System.Collections.Concurrent;
using System.Data.Common;
using System.Diagnostics;

namespace Day_10;

public class Solver
{
    private enum Direction { Up, Down, Left, Right }
    private static (int x, int y) GetCoordAtDirection(int xPos, int yPos, Direction dir)
    {
        return dir switch
        {
            Direction.Up => (xPos, yPos - 1),
            Direction.Down => (xPos, yPos + 1),
            Direction.Left => (xPos - 1, yPos),
            Direction.Right => (xPos + 1, yPos),
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
    }

    private static bool IsDirValid(int xPos, int yPos, Direction dir, int width, int height)
    {
        return dir switch
        {
            Direction.Up => yPos - 1 >= 0,
            Direction.Down => yPos + 1 < height,
            Direction.Left => xPos - 1 >= 0,
            Direction.Right => xPos + 1 < width,
            _ => false
        };
    }
    
    private static List<(int x, int y)> GetAvailableNodes(int[,] grid, int x, int y)
    {
        var nextAvailablePositions = new List<(int x, int y)>();
        foreach (var dir in Enum.GetValues<Direction>())
        {
            if (!IsDirValid(x, y, dir, grid.GetLength(1), grid.GetLength(0))) continue;
            var coord = GetCoordAtDirection(x, y, dir);

            if (grid[coord.y, coord.x] == (grid[y, x] + 1))
            {
                nextAvailablePositions.Add((coord.x, coord.y));
            }
        }

        return nextAvailablePositions;
    }

    private static (int uniqueEnds, int numberOfPaths) GetNumberOfPaths(int[,] grid, int x, int y)
    {
        HashSet<(int x, int y)> visited = [];
        int numPaths = 0;
        
        var nextAvailablePositions = GetAvailableNodes(grid, x, y);
        TraverseGrid(nextAvailablePositions);

        return (visited.Count, numPaths);

        void TraverseGrid(List<(int x, int y)> nodesToCheck)
        {
            foreach (var node in nodesToCheck)
            {
                if (grid[node.y, node.x] == 9)
                {
                    numPaths++;
                    visited.Add((node.x, node.y));
                }
                else
                {
                    var newNodes = GetAvailableNodes(grid, node.x, node.y);
                    TraverseGrid(newNodes);
                }
            }
        }
    }
    
    private static (int, int) ParseGrid(int[,] grid)
    {
        var unique = 0;
        var num = 0;
        
        for (var y = 0; y < grid.GetLength(0); y++)
        {
            for (var x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x] != 0) continue;

                var res = GetNumberOfPaths(grid, x, y);
                unique += res.uniqueEnds;
                num += res.numberOfPaths;
            }
        }

        return (unique, num);
    }
    
    private static int[,] GetGrid(string input)
    { 
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var height = lines.Length;
        var width = lines[0].Length;
        var grid = new int[width, height];
        
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                grid[y, x] =  int.Parse(lines[y][x].ToString());
            }
        }

        return grid;
    }
    
    public static int Run_PartOne(string input)
    {
        // Parse string
        var grid = GetGrid(input);

        var sum = ParseGrid(grid).Item1;
      
        return sum;
    }
    
    public static int Run_PartTwo(string input)
    {
        var grid = GetGrid(input);

        var sum = ParseGrid(grid).Item2;
        return sum;
    }
}