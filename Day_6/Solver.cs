using System.Collections.Concurrent;

namespace Day_6;

public class Solver
{
    private enum Direction { Up, Right, Down, Left };
    private record struct Point(int X, int Y) { }
    private record struct VisitedPointWithDirections(Point Point, Direction IncomingDirection, Direction OutgoingDirection);
    
    private static Point Move(Point p, Direction dir) => dir switch
    {
        Direction.Up => p with { Y = p.Y - 1 },
        Direction.Down => p with { Y = p.Y + 1 },
        Direction.Left => p with { X = p.X - 1 },
        Direction.Right => p with { X = p.X + 1 },
        _ => p
    };
    
    private static bool IsPosInBounds(Point pos, int maxWidth, int maxHeight) 
        => pos.X >= 0 && pos.Y >= 0 && pos.X < maxWidth && pos.Y < maxHeight;

    private static Direction RotateDirection(Direction dir) => dir switch
    {
        Direction.Up => Direction.Right,
        Direction.Down => Direction.Left,
        Direction.Left => Direction.Up,
        Direction.Right => Direction.Down,
        _ => Direction.Up
    };
    
    private static Dictionary<Point, int> GetGuardVisitedPositions(string[,] grid, Point startPos)
    {
        var visitedPositions = new Dictionary<Point, int>();
        var currentDir = Direction.Up;
        var currentPos = startPos;
        while (true)
        {
            var newPos = Move(currentPos, currentDir);
            if (!IsPosInBounds(newPos, grid.GetLength(0), grid.GetLength(1)))
            {
                visitedPositions[currentPos] = 1;
                break;
            }
            
            if (grid[newPos.Y, newPos.X] == "#")
            {
                currentDir = RotateDirection(currentDir);
            }
            else
            {
                visitedPositions[currentPos] = 1;
                currentPos = newPos;
            }
        }

        return visitedPositions;
    }
    
    private static int GetNumberOfLoopedRoutes(string[,] grid, Dictionary<Point, int> visitedPositions, Point startPos)
    {
        //int numberOfLoopedRoutes = 0;
        var concurrentBag = new ConcurrentBag<int>();
        Parallel.For(0, visitedPositions.Count, (index, state) =>
        {
            var gridCopy = (string[,])grid.Clone();
            var currentDir = Direction.Up;
            var currentPos = startPos;
            var currentIterPos = visitedPositions.ElementAt(index);
            if (currentIterPos.Key == startPos) return;
            
            gridCopy[currentIterPos.Key.Y, currentIterPos.Key.X] = "O";
            
            var pointsWithDirections = new Dictionary<VisitedPointWithDirections, int>();
            while (true)
            {
                var newPos = Move(currentPos, currentDir);
                if (!IsPosInBounds(newPos, gridCopy.GetLength(0), gridCopy.GetLength(1)))
                    break;
            
                if (gridCopy[newPos.Y, newPos.X] == "#" || gridCopy[newPos.Y, newPos.X] == "O")
                {
                    var newDir = RotateDirection(currentDir);
                    var pointWithDirection = new VisitedPointWithDirections(currentPos, currentDir, newDir);
                    if (!pointsWithDirections.TryAdd(pointWithDirection, 1))
                    {
                        concurrentBag.Add(1);
                        break;
                    }

                    currentDir = RotateDirection(currentDir);
                }
                else
                {
                    currentPos = newPos;
                }
            }
            
            gridCopy[currentIterPos.Key.Y, currentIterPos.Key.X] = ".";
        });
       
       return concurrentBag.Sum();
        //return numberOfLoopedRoutes;
    }
    
    private static string[,] GetGrid(string input)
    {
        string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        int height = lines.Length;
        int width = lines[0].Length;
        string[,] grid = new string[width, height];
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[y, x] = lines[y][x].ToString();
            }
        }

        return grid;
    }
    
    public static int Run_PartOne(string input)
    {
        // Something in front; Turn 90 degrees to the right
        // Move forward
        // Add each position (including starting position) to a dictionary.

        var grid = GetGrid(input);
        var startPos = new Point();
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x] == "^") startPos = new Point(x, y);
            }
        }
      
        var visitedPositions = GetGuardVisitedPositions(grid, startPos);
        var distinctPositions = visitedPositions.Keys.Count;
        return distinctPositions;
    }
    
    public static int Run_PartTwo(string input)
    {
        // Start by running PartOne to get all the positions visited.
        // Place a new obstacle in any of the visited positions (except the startPos) and run the loop detection:
        //
        // Record the direction, position and new direction every time you need to change direction.
        // If the data already exists, you know you've been here before and is thus in a loop.
        // Repeat until every visited position has been tried at least once (except startPos)
        
        var grid = GetGrid(input);
        var startPos = new Point();
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x] == "^") startPos = new Point(x, y);
            }
        }
      
        var visitedPositions = GetGuardVisitedPositions(grid, startPos);
        var loopedRoutes = GetNumberOfLoopedRoutes(grid, visitedPositions, startPos);
        return loopedRoutes;
    }
}