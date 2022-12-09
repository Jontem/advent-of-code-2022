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
        var tail = new Coordinate(0, 0);
        var headPos = new List<Coordinate>() { head };
        var tailPos = new List<Coordinate>() { tail };
        foreach (var line in File.ReadAllLines("input"))
        {
            var parts = line.Split(" ");
            var direction = Directions[parts[0]];
            var steps = int.Parse(parts[1]);

            for (var i = 1; i <= steps; i++)
            {
                head = new Coordinate(head.X + direction.X, head.Y + direction.Y);
                headPos.Add(head);
                if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
                {
                    tail = headPos[headPos.Count - 2];
                    tailPos.Add(tail);
                    // Console.WriteLine("1");
                }
            }


        }

        Console.WriteLine(new HashSet<Coordinate>(tailPos).Count);
    }

    public static void Solve2()
    {

    }
}