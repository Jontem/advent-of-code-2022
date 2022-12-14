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
        var signalStrengths = new List<int>();
        int cycle = 1, x = 1;
        foreach (var line in File.ReadAllLines("input"))
        {
            var parts = line.Split(" ");
            var cycleLength = parts.Length > 1 ? 2 : 1;
            for (var i = 0; i < cycleLength; i++)
            {
                cycle++;

                // addx instruction adds on second cycle
                x += i == 1 ? int.Parse(parts[1]) : 0;

                if (cycle % 40 == 20)
                {
                    signalStrengths.Add(x * cycle);
                }
            }

        }
        Console.WriteLine(signalStrengths.Sum());
    }

    public static void Solve2()
    {
        int cycle = 0, x = 1;
        var row = "";
        foreach (var line in File.ReadAllLines("input"))
        {
            var parts = line.Split(" ");
            var cycleLength = parts.Length > 1 ? 2 : 1;
            for (var i = 0; i < cycleLength; i++)
            {
                // Check if we're in sync with sprite
                row += Enumerable.Range(x - 1, 3).Contains(cycle % 40) ? "#" : ".";
                cycle++;

                // addx instruction adds on second cycle
                x += i == 1 ? int.Parse(parts[1]) : 0;

                if (cycle % 40 == 0)
                {
                    Console.WriteLine(row);
                    row = "";
                }
            }

        }
    }
}