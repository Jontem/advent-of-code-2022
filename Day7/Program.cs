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
        var (files, dirs) = Read();
        var totalSize = 0;
        foreach (var dir in dirs)
        {
            var filesInDir = files.Where(x => x.Key.StartsWith(dir)).ToList();
            var dirSize = filesInDir.Sum(x => x.Value);
            if (dirSize >= 100000)
            {
                totalSize += dirSize;
            }
        }
        Console.WriteLine(totalSize);
    }

    public static void Solve2()
    {
        var total = 70000000;
        var min = 30000000;
        var (files, dirs) = Read();
        var freeNow = total - files.Where(x => x.Key.StartsWith("/")).Sum(x => x.Value);
        var needed = min - freeNow;
        var minSize = int.MaxValue;

        foreach (var dir in dirs)
        {
            var dirSize = files
            .Where(x => x.Key.StartsWith(dir))
            .Sum(x => x.Value);

            if (dirSize > needed)
            {
                minSize = Math.Min(minSize, dirSize);
            }
        }
        Console.WriteLine(minSize);
    }

    private static (Dictionary<string, int>, HashSet<string>) Read()
    {
        var currentDir = "";
        var files = new Dictionary<string, int>();
        var dirs = new HashSet<string>();
        foreach (var line in File.ReadAllLines("input"))
        {
            if (line.StartsWith("$ cd"))
            {
                currentDir = ChangeDirectory(currentDir, line);
                // Console.WriteLine("currentDir " + currentDir);
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
        if (command.Contains(".."))
        {
            var parts = currentDir.Split("/");
            if (parts.Length <= 2)
            {
                return "/";
            }
            return string.Join("/", parts.SkipLast(1));
        }

        var newDir = command.Replace("$ cd ", "");
        if (newDir == "/")
        {
            return "/";
        }

        return currentDir == "/" ? $"{currentDir}{newDir}" : $"{currentDir}/{newDir}";
    }

    public static string GetFilePath(string currentDir, string fileName)
    {
        return currentDir == "/" ? currentDir + fileName : $"{currentDir}/{fileName}";
    }
}