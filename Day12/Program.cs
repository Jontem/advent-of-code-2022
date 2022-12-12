// Heightmap a-z lowercase nodes, a is lowest, z highest
// Start "S", end/goal "E"
// With as few steps as possible
// the next square can be at most one higher than the current
// the next square can be much lower than the current
// 

internal class Program
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

        var queue = new Queue<Node>(new List<Node> { GetStartNode(grid) });
        var visited = new HashSet<Vector2d>();
        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();
            var currValue = GetValue(grid[currNode.Pos.Y][currNode.Pos.X]);
            if (grid[currNode.Pos.Y][currNode.Pos.X] == End)
            {
                Console.WriteLine("Steps: " + CalculatePath(grid, currNode));
                break;
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
    }

    public static void Solve2()
    {
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

        Console.WriteLine(grid[node.Pos.Y][node.Pos.X]);

        return 1 + (CalculatePath(grid, node.Parent));
    }

}