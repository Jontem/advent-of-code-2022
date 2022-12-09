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
        Console.WriteLine(CalculateTailVisits(1));
    }

    public static void Solve2()
    {
        Console.WriteLine(CalculateTailVisits(9));
    }

    private static int CalculateTailVisits(int knotSize)
    {
        var head = new Vector2d(0, 0);
        var knots = Enumerable.Range(0, knotSize)
        .Select(_ => new Vector2d(0, 0))
        .ToList();
        var tailHist = knots.Select(_ => new Vector2d(0, 0)).ToList();

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

        return new HashSet<Vector2d>(tailHist).Count;
    }

    private static Vector2d GetNewKnotPosition(Vector2d prevKnot, Vector2d knot)
    {
        var moveX = prevKnot.X.CompareTo(knot.X);
        var moveY = prevKnot.Y.CompareTo(knot.Y);
        return new Vector2d(knot.X + moveX, knot.Y + moveY);
    }
}