internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("------------- Part 1 -------------");
        Solve1();
        Console.WriteLine("------------- Part 2 -------------");
        Solve2();

    }

    public record Coordinate(int X, int Y);
    private static Dictionary<string, Coordinate> Directions = new Dictionary<string, Coordinate> {
        {"R", new Coordinate(1, 0)},
        {"L", new Coordinate(-1, 0)},
        {"D", new Coordinate(0, -1)},
        {"U", new Coordinate(0, 1)},
    };

    public static void Solve1()
    {
        var head = new Coordinate(0, 0);
        var tailHistory = new List<Coordinate>() { new Coordinate(0, 0) };
        foreach (var line in File.ReadAllLines("input"))
        {
            var parts = line.Split(" ");
            var direction = Directions[parts[0]];
            var steps = int.Parse(parts[1]);

            for (var i = 1; i <= steps; i++)
            {
                head = new Coordinate(head.X + direction.X, head.Y + direction.Y);
                var tail = tailHistory.Last();
                if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
                {
                    tail = GetNewKnotPosition(head, tail);
                    tailHistory.Add(tail);
                }
            }


        }

        Console.WriteLine(new HashSet<Coordinate>(tailHistory).Count);
    }

    public static void Solve2()
    {
        var head = new Coordinate(0, 0);
        var tails = Enumerable.Range(0, 9)
        .Select(_ => new Coordinate(0, 0))
        .ToList();
        var tailHist = new List<Coordinate>() { new Coordinate(0, 0) };

        foreach (var line in File.ReadAllLines("input"))
        {
            var parts = line.Split(" ");
            var direction = Directions[parts[0]];
            var steps = int.Parse(parts[1]);

            for (var i = 1; i <= steps; i++)
            {
                head = new Coordinate(head.X + direction.X, head.Y + direction.Y);

                var prevKnot = head;
                for (var j = 0; j < tails.Count; j++)
                {
                    if (Math.Abs(prevKnot.X - tails[j].X) > 1 || Math.Abs(prevKnot.Y - tails[j].Y) > 1)
                    {
                        tails[j] = GetNewKnotPosition(prevKnot, tails[j]);
                    }
                    prevKnot = tails[j];
                }

                tailHist.Add(tails[tails.Count - 1]);
            }
        }

        Console.WriteLine(new HashSet<Coordinate>(tailHist).Count);

    }

    private static Coordinate GetNewKnotPosition(Coordinate prevKnot, Coordinate tail)
    {
        if (prevKnot.X != tail.X && prevKnot.Y != tail.Y)
        {
            var moveX = prevKnot.X < tail.X ? -1 : 1;
            var moveY = prevKnot.Y < tail.Y ? -1 : 1;
            return new Coordinate(tail.X + moveX, tail.Y + moveY);
        }
        else if (prevKnot.X != tail.X)
        {
            var moveX = prevKnot.X < tail.X ? -1 : 1;
            return new Coordinate(tail.X + moveX, tail.Y);
        }
        else if (prevKnot.Y != tail.Y)
        {
            var moveY = prevKnot.Y < tail.Y ? -1 : 1;
            return new Coordinate(tail.X, tail.Y + moveY);
        }
        else
        {
            throw new Exception("dont go here");
        }
    }
}