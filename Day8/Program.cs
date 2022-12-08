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
        var grid = ParseGrid();
        var visible = new HashSet<Coordinate>();
        for (var y = 0; y < grid.Count; y++)
        {
            for (var x = 0; x < grid[0].Count; x++)
            {
                var cord = new Coordinate(x, y);
                var (isVisable, _) = Explore(grid, cord);
                if (isVisable)
                {
                    visible.Add(cord);
                }
            }
        }
        Console.WriteLine(visible.Count);
    }

    public static void Solve2()
    {
        var grid = ParseGrid();
        var largestScore = 0;
        for (var y = 0; y < grid.Count; y++)
        {
            for (var x = 0; x < grid[0].Count; x++)
            {
                var cord = new Coordinate(x, y);
                var (_, score) = Explore(grid, cord);
                largestScore = Math.Max(largestScore, score);
            }
        }
        Console.WriteLine(largestScore);
    }

    public static List<List<int>> ParseGrid()
    {
        return File.ReadAllLines("input")
        .Select(x => x.ToList().Select(x => int.Parse(x.ToString())).ToList())
        .ToList();
    }


    public static (bool, int) Explore(List<List<int>> grid, Coordinate start)
    {
        var dirVisibility = new List<bool>();
        var scores = new List<int>();
        if (start.Y == 0 || start.Y == grid.Count - 1 || start.X == 0 || start.X == grid[0].Count - 1)
        {
            dirVisibility.Add(true);
            scores.Add(0);
        }

        var dirs = new List<Coordinate> {
            new Coordinate(0, 1),
            new Coordinate(0, -1),
            new Coordinate(1, 0),
            new Coordinate(-1, 0)
        };

        var curr = grid[start.Y][start.X];
        foreach (var dir in dirs)
        {
            var dirScore = 0;
            var next = new Coordinate(start.X + dir.X, start.Y + dir.Y);
            var visible = true;
            while ((next.Y >= 0 && next.Y < grid.Count) && (next.X >= 0 && next.X < grid[0].Count))
            {
                dirScore++;
                var nextTree = grid[next.Y][next.X];
                if (nextTree >= curr)
                {
                    visible = false;
                    break;
                }
                next = new Coordinate(next.X + dir.X, next.Y + dir.Y);
            }
            dirVisibility.Add(visible);
            scores.Add(dirScore);
        }

        return (dirVisibility.Any(x => x), scores.Aggregate((a, x) => a * x));
    }

    public record Coordinate(int X, int Y);
}