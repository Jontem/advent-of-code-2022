﻿internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("------------- Part 1 -------------");
        Solve1();
        Console.WriteLine("------------- Part 2 -------------");
        Solve2();
    }

    private static Char End = Char.Parse("E");
    private static Char Start = Char.Parse("S");

    public static void Solve1()
    {
        var grid = File.ReadAllLines("input")
        .Select(l => l.ToList())
        .ToList();
        Console.WriteLine("Steps: " + BFS(grid, GetStartNode(grid)));
    }

    public static void Solve2()
    {
        var grid = File.ReadAllLines("input")
        .Select(l => l.ToList())
        .ToList();

        var min = int.MaxValue;
        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[i].Count; j++)
            {
                if (GetValue(grid[i][j]) == 'a')
                {
                    var steps = BFS(grid, new Node(new Vector2d(j, i), null));
                    if (steps > 0)
                    {
                        min = Math.Min(min, steps);
                    }
                }
            }
        }

        Console.WriteLine("Steps: " + min);
    }

    private static int BFS(List<List<char>> grid, Node start)
    {
        var queue = new Queue<Node>(new List<Node> { start });
        var visited = new HashSet<Vector2d>();
        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();
            var currValue = GetValue(grid[currNode.Pos.Y][currNode.Pos.X]);
            if (grid[currNode.Pos.Y][currNode.Pos.X] == End)
            {
                var val = CalculatePath(grid, currNode);
                return val;
            }
            var neighbours = GetAdjacents(grid, currNode.Pos);
            foreach (var neighbourPos in neighbours)
            {
                var neighbourValue = GetValue(grid[neighbourPos.Y][neighbourPos.X]);
                if (visited.Contains(neighbourPos))
                {
                    continue;
                }

                if (neighbourValue <= currValue + 1)
                {
                    queue.Enqueue(new Node(neighbourPos, currNode));
                    visited.Add(neighbourPos);
                }
            }

        }

        return -1;
    }

    public record Node(Vector2d Pos, Node? Parent);
    public record Vector2d(int X, int Y);

    private static List<Vector2d> Directions = new List<Vector2d> {
        new Vector2d(1, 0),
        new Vector2d(-1, 0),
        new Vector2d(0, -1),
        new Vector2d(0, 1),
    };

    private static Char GetValue(Char value)
    {
        return value == Start ? 'a' : value == End ? 'z' : value;
    }

    private static Node GetStartNode(List<List<Char>> grid)
    {
        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[i].Count; j++)
            {
                if (grid[i][j] == Start)
                {
                    return new Node(new Vector2d(j, i), null);
                }
            }

        }

        throw new Exception("Couldn't find start node");
    }

    private static List<Vector2d> GetAdjacents(List<List<Char>> grid, Vector2d current)
    {
        var adjacents = new List<Vector2d>();
        foreach (var dir in Directions)
        {
            var pos = new Vector2d(current.X + dir.X, current.Y + dir.Y);
            if ((pos.Y >= 0 && pos.Y < grid.Count) && (pos.X >= 0 && pos.X < grid[0].Count))
            {
                adjacents.Add(pos);
            }
        }

        return adjacents;
    }

    private static int CalculatePath(List<List<Char>> grid, Node? node)
    {
        if (node == null)
        {
            return -1;
        }

        return 1 + (CalculatePath(grid, node.Parent));
    }

}