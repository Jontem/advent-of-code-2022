internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("------------- Part 1 -------------");
        Solve1();
        Console.WriteLine("------------- Part 2 -------------");
        Solve2();
    }

    public static void Solve1()
    {
        var grid = File.ReadAllLines("input").Select(l => l.ToList()).ToList();
        Console.WriteLine("Steps: " + BFS(grid, GetStartNode(grid)));
    }

    public static void Solve2()
    {
        var grid = File.ReadAllLines("input").Select(l => l.ToList()).ToList();

        var min = int.MaxValue;
        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[i].Count; j++)
            {
                var val = grid[i][j];
                if (val == 'a')
                {
                    var steps = BFS(grid, new Node(val, new Vector2d(j, i), null));
                    if (steps > 0)
                    {
                        min = Math.Min(min, steps);
                    }
                }
            }
        }

        Console.WriteLine("Steps: " + min);
    }

    private static Char End = Char.Parse("E");
    private static Char Start = Char.Parse("S");

    // BFS works because the weight is always one.
    // The first time we visit a node will always be the shortest
    private static int BFS(List<List<char>> grid, Node start)
    {
        var queue = new Queue<Node>(new List<Node> { start });
        var visited = new HashSet<Vector2d>();
        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();
            if (currNode.Value == End)
            {
                return CalculatePath(grid, currNode);
            }
            var neighbours = GetNeighbours(grid, currNode.Pos);
            foreach (var neighbourPos in neighbours.Where(n => !visited.Contains(n)))
            {
                if (GetRealValue(grid[neighbourPos.Y][neighbourPos.X]) <= GetRealValue(currNode.Value) + 1)
                {
                    queue.Enqueue(new Node(grid[neighbourPos.Y][neighbourPos.X], neighbourPos, currNode));
                    visited.Add(neighbourPos);
                }
            }
        }
        return -1;
    }

    public record Node(Char Value, Vector2d Pos, Node? Parent);
    public record Vector2d(int X, int Y);

    private static List<Vector2d> Directions = new List<Vector2d> {
        new Vector2d(1, 0),
        new Vector2d(-1, 0),
        new Vector2d(0, -1),
        new Vector2d(0, 1),
    };

    private static Char GetRealValue(Char value)
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
                    return new Node(grid[i][j], new Vector2d(j, i), null);
                }
            }

        }

        throw new Exception("Couldn't find start node");
    }

    private static List<Vector2d> GetNeighbours(List<List<Char>> grid, Vector2d current)
    {
        var neighbours = new List<Vector2d>();
        foreach (var dir in Directions)
        {
            var pos = new Vector2d(current.X + dir.X, current.Y + dir.Y);
            if ((pos.Y >= 0 && pos.Y < grid.Count) && (pos.X >= 0 && pos.X < grid[0].Count))
            {
                neighbours.Add(pos);
            }
        }

        return neighbours;
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