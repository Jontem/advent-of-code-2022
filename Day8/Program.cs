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
                // Console.WriteLine(grid[y][x]);
                var cord = new Coordinate(x, y);
                if (IsVisible(grid, cord))
                {
                    visible.Add(cord);
                    // Console.WriteLine($"Visible x={x}, y={y}, value={grid[y][x]}");
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
                // Console.WriteLine(grid[y][x]);
                var cord = new Coordinate(x, y);
                largestScore = Math.Max(largestScore, CalculateScore(grid, cord));
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

    public static bool IsVisible(List<List<int>> grid, Coordinate start)
    {

        if (start.Y == 0 || start.Y == grid.Count - 1 || start.X == 0 || start.X == grid[0].Count - 1)
        {
            return true;
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
            var next = new Coordinate(start.X + dir.X, start.Y + dir.Y);
            var visible = true;
            while ((next.Y >= 0 && next.Y < grid.Count) && (next.X >= 0 && next.X < grid[0].Count))
            {
                var nextTree = grid[next.Y][next.X];
                if (nextTree >= curr)
                {
                    visible = false;
                }
                next = new Coordinate(next.X + dir.X, next.Y + dir.Y);
            }

            if (visible)
            {
                return true;
            }
        }

        // Console.WriteLine("hek");
        // Console.WriteLine(start);
        return false;
    }

    public static int CalculateScore(List<List<int>> grid, Coordinate start)
    {
        if (start.Y == 0 || start.Y == grid.Count - 1 || start.X == 0 || start.X == grid[0].Count - 1)
        {
            return 0;
        }

        var dirs = new List<Coordinate> {
            new Coordinate(0, 1),
            new Coordinate(0, -1),
            new Coordinate(1, 0),
            new Coordinate(-1, 0)
        };

        var curr = grid[start.Y][start.X];
        var scores = new List<int>();
        foreach (var dir in dirs)
        {
            var dirScore = 0;
            var next = new Coordinate(start.X + dir.X, start.Y + dir.Y);
            while ((next.Y >= 0 && next.Y < grid.Count) && (next.X >= 0 && next.X < grid[0].Count))
            {
                dirScore++;
                var nextTree = grid[next.Y][next.X];
                if (nextTree >= curr)
                {
                    break;
                }
                next = new Coordinate(next.X + dir.X, next.Y + dir.Y);
            }
            scores.Add(dirScore);
        }

        // Console.WriteLine("hek");
        // Console.WriteLine(start);
        return scores
        .Aggregate((a, x) => a * x);
    }

    public record Coordinate(int X, int Y);
}