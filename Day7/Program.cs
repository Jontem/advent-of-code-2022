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
        var (files, dirs) = ReadInput();
        var totalSize = dirs
        .Select(dir => files
            .Where(f => f.Key.StartsWith(dir))
            .Sum(x => x.Value))
        .Where(x => x <= 100000)
        .Sum();

        Console.WriteLine(totalSize);
    }

    public static void Solve2()
    {
        var total = 70000000;
        var min = 30000000;

        var (files, dirs) = ReadInput();
        var freeNow = total - files.Where(x => x.Key.StartsWith("/")).Sum(x => x.Value);
        var needed = min - freeNow;

        var minSize = dirs
        .Select(dir => files
            .Where(x => x.Key.StartsWith(dir))
            .Sum(x => x.Value))
        .Where(dirSize => dirSize >= needed)
        .Min();

        Console.WriteLine(minSize);
    }

    private static (Dictionary<string, int>, HashSet<string>) ReadInput()
    {
        var currentDir = "";
        var files = new Dictionary<string, int>();
        var dirs = new HashSet<string>();
        foreach (var line in File.ReadAllLines("input"))
        {
            if (line.StartsWith("$ cd"))
            {
                currentDir = ChangeDirectory(currentDir, line);
            }
            else if (line.StartsWith("$ ls"))
            {
            }
            else if (line.StartsWith("dir "))
            {
                dirs.Add(GetFilePath(currentDir, line.Split(" ")[1]));
            }
            else
            {
                var match = line.Split(" ");
                files.Add(GetFilePath(currentDir, match[1]), int.Parse(match[0]));
            }
        }

        return (files, dirs);
    }

    public static string ChangeDirectory(string currentDir, string command)
    {
        if (command.EndsWith(".."))
        {
            var parts = currentDir.Split("/");
            return parts.Length <= 2 ? "/" : string.Join("/", parts.SkipLast(1));
        }

        var newDir = command.Replace("$ cd ", "");
        return currentDir == "/" ? $"/{newDir}" : $"{currentDir}/{newDir}";
    }

    public static string GetFilePath(string currentDir, string fileName)
    {
        return currentDir == "/" ? currentDir + fileName : $"{currentDir}/{fileName}";
    }
}