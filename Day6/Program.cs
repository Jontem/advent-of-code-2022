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
        var markerLength = 4;
        var input = File.ReadAllText("input");
        for (var i = 0; i < input.Length; i++)
        {
            var marker = input.Substring(i, markerLength);
            if (marker.ToList().Distinct().Count() == markerLength)
            {
                Console.WriteLine(marker);
                Console.WriteLine(i + markerLength);
                break;
            }
        }

    }
    public static void Solve2()
    {
        var markerLength = 14;
        var input = File.ReadAllText("input");
        for (var i = 0; i < input.Length; i++)
        {
            var marker = input.Substring(i, markerLength);
            if (marker.ToList().Distinct().Count() == markerLength)
            {
                Console.WriteLine(marker);
                Console.WriteLine(i + markerLength);
                break;
            }
        }
    }
}