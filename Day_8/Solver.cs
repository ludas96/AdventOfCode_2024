using System.Collections.Concurrent;

namespace Day_8;

public class Solver
{
    private struct Vector() : IEquatable<Vector>
    {
        public Vector(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public Vector(Vector v) : this()
        {
            X = v.X;
            Y = v.Y;
        }
        
        public int X { get; private set; }
        public int Y { get; private set; }
        
        public static Vector operator - (Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }
        public static Vector operator + (Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return a == b == false;
        }

        public bool Equals(Vector other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
    private struct Node(Vector position, string value)
    {
        public Vector Position { get; set; } = position;
        public string Value { get; set; } = value;
    }
    
    private static Node[,] GetGrid(string input)
    {
        string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        int height = lines.Length;
        int width = lines[0].Length;
        var grid = new Node[width, height];
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[y, x] =  new Node(new Vector(x, y), lines[y][x].ToString());
            }
        }

        return grid;
    }
    private static Vector GetDistanceBetweenNodes(Node a, Node b)
    {
        return new Vector(a.Position - b.Position);
    }
    private static Node? GetNodeAtDistance(Vector distance, Node otherNode, Node[,] grid)
    {
        var pos = otherNode.Position + distance;
        if (pos.X < 0 || pos.X >= grid.GetLength(1))
        {
            return null;
        }
        if (pos.Y < 0 || pos.Y >= grid.GetLength(0))
        {
            return null;
        }
        
        return grid[pos.Y, pos.X];
    }

    private static List<Node> GetAllNodesInDirectionTowardsB(Node a, Node b, Node[,] grid)
    {
        var delta = b.Position - a.Position;
        
        var gcd = GCD(Math.Abs(delta.X), Math.Abs(delta.Y));
        int stepX = delta.X / gcd;
        int stepY = delta.Y / gcd;
        
        int x = a.Position.X;
        int y = a.Position.Y;
        var currentNode = a;
        
        var nodes = new List<Node>();
        while (true)
        {
            nodes.Add(currentNode);
            x += stepX;
            y += stepY;
            if ((x < 0 || x >= grid.GetLength(1)) ||
                (y < 0 || y >= grid.GetLength(0)))
                break;
            
            currentNode = grid[y, x];
        }
        return nodes;
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
    
    public static int Run_PartOne(string input)
    {
        var grid = GetGrid(input);
        var distinctNodes = new Dictionary<string, List<Node>>();
        var antiNodes = new List<Node>();
        
        
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                var node = grid[y, x];
                if (node.Value == ".") continue;
                
                if (!distinctNodes.TryAdd(node.Value, [node]))
                {
                    distinctNodes[node.Value].Add(node);
                }
            }
        }

        foreach (var kvp in distinctNodes)
        {
            foreach (var node in kvp.Value)
            {
                foreach (var node2 in kvp.Value)
                {
                    if (node.Position == node2.Position)
                        continue;
                    
                    var distance = GetDistanceBetweenNodes(node, node2);
                    var node3 = GetNodeAtDistance(distance, node, grid);
                    if (node3.HasValue)
                    {
                        antiNodes.Add(node3.Value);
                    }
                }
            }
        }
        
        return antiNodes.DistinctBy(x => x.Position).Count();
    }
    
    public static int Run_PartTwo(string input)
    {
        var grid = GetGrid(input);
        var distinctNodes = new Dictionary<string, List<Node>>();
        var antiNodes = new List<Node>();
        
        
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                var node = grid[y, x];
                if (node.Value == ".") continue;
                
                if (!distinctNodes.TryAdd(node.Value, [node]))
                {
                    distinctNodes[node.Value].Add(node);
                }
            }
        }

        foreach (var kvp in distinctNodes)
        {
            foreach (var node in kvp.Value)
            {
                foreach (var node2 in kvp.Value)
                {
                    if (node.Position == node2.Position)
                        continue;

                    var nodes = GetAllNodesInDirectionTowardsB(node, node2, grid);
                    foreach (var node3 in nodes)
                    {
                        antiNodes.Add(node3);
                    }
                }
            }
        }

     
        return antiNodes.DistinctBy(x => x.Position).Count();
    }
}