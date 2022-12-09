internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("------------- Part 1 -------------");
        Solve1();
        Console.WriteLine("------------- Part 2 -------------");
        Solve2();

    }

    public record Vector2d(int X, int Y);

    private static Dictionary<string, Vector2d> Directions = new Dictionary<string, Vector2d> {
        {"R", new Vector2d(1, 0)},
        {"L", new Vector2d(-1, 0)},
        {"D", new Vector2d(0, -1)},
        {"U", new Vector2d(0, 1)},
    };

    public static void Solve1()
    {
        var head = new Vector2d(0, 0);
        var tailHistory = new List<Vector2d>() { new Vector2d(0, 0) };
        foreach (var line in File.ReadAllLines("input"))
        {
            var parts = line.Split(" ");
            var direction = Directions[parts[0]];
            var steps = int.Parse(parts[1]);

            for (var i = 1; i <= steps; i++)
            {
                head = new Vector2d(head.X + direction.X, head.Y + direction.Y);
                var tail = tailHistory.Last();
                if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
                {
                    tail = GetNewKnotPosition(head, tail);
                    tailHistory.Add(tail);
                }
            }
        }

        Console.WriteLine(new HashSet<Vector2d>(tailHistory).Count);
    }

    public static void Solve2()
    {
        var head = new Vector2d(0, 0);
        var knots = Enumerable.Range(0, 9)
        .Select(_ => new Vector2d(0, 0))
        .ToList();
        var tailHist = new List<Vector2d>() { new Vector2d(0, 0) };

        foreach (var line in File.ReadAllLines("input"))
        {
            var parts = line.Split(" ");
            var direction = Directions[parts[0]];
            var steps = int.Parse(parts[1]);

            for (var i = 1; i <= steps; i++)
            {
                head = new Vector2d(head.X + direction.X, head.Y + direction.Y);

                var prevKnot = head;
                for (var j = 0; j < knots.Count; j++)
                {
                    if (Math.Abs(prevKnot.X - knots[j].X) > 1 || Math.Abs(prevKnot.Y - knots[j].Y) > 1)
                    {
                        knots[j] = GetNewKnotPosition(prevKnot, knots[j]);
                    }
                    prevKnot = knots[j];
                }

                tailHist.Add(knots[knots.Count - 1]);
            }
        }

        Console.WriteLine(new HashSet<Vector2d>(tailHist).Count);

    }

    private static Vector2d GetNewKnotPosition(Vector2d prevKnot, Vector2d tail)
    {
        if (prevKnot.X != tail.X && prevKnot.Y != tail.Y)
        {
            var moveX = prevKnot.X < tail.X ? -1 : 1;
            var moveY = prevKnot.Y < tail.Y ? -1 : 1;
            return new Vector2d(tail.X + moveX, tail.Y + moveY);
        }
        else if (prevKnot.X != tail.X)
        {
            var moveX = prevKnot.X < tail.X ? -1 : 1;
            return new Vector2d(tail.X + moveX, tail.Y);
        }
        else if (prevKnot.Y != tail.Y)
        {
            var moveY = prevKnot.Y < tail.Y ? -1 : 1;
            return new Vector2d(tail.X, tail.Y + moveY);
        }
        else
        {
            throw new Exception("dont go here");
        }
    }
}