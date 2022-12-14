﻿internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("------------- Part 1 -------------");
        Solve1();
        Console.WriteLine("------------- Part 2 -------------");
        Solve2();
    }

    static List<Vector2d> Directions = new List<Vector2d> { new Vector2d(0, 1), new Vector2d(-1, 1), new Vector2d(1, 1) };

    public static void Solve1()
    {
        var rocks = ParseRocks();

        var grid = CreateGrid(rocks);
        PlaceRocks(grid, rocks);
        // PrintGrid(grid);

        int unitsOfSand = PourSand(grid, rocks, false);

        Console.WriteLine("Units of sand: " + unitsOfSand);

    }

    public static void Solve2()
    {
        var rocks = ParseRocks();
        var grid = CreateGrid(rocks);
        PlaceRocks(grid, rocks);
        // PrintGrid(grid);

        int unitsOfSand = PourSand(grid, rocks, true);

        Console.WriteLine("Units of sand: " + unitsOfSand);
    }

    private static int PourSand(List<List<string>> grid, List<Rock> rocks, bool part2)
    {
        var xMin = part2 ? 0 : rocks.SelectMany(x => x.paths.Select(y => y.X)).Min();
        var xMax = rocks.SelectMany(x => x.paths.Select(y => y.X)).Max() + 1000;
        var yMax = part2 ? grid.Count : rocks.SelectMany(x => x.paths.Select(y => y.Y)).Max() + 1;

        var currentSand = new Vector2d(500, 0);
        var unitsOfSand = 0;
        while (true)
        {
            var temp = currentSand;
            foreach (var direction in Directions)
            {
                var newY = currentSand.Y + direction.Y;
                var newX = currentSand.X + direction.X;
                if (!IsInside(yMax, xMin, xMax, new Vector2d(newX, newY)) || grid[newY][newX] == AIR)
                {
                    temp = new Vector2d(newX, newY);
                    break;
                }
            }

            // outside of grid
            if (!IsInside(yMax, xMin, xMax, temp) || temp == new Vector2d(500, 0))
            {
                grid[temp.Y][temp.X] = SAND;
                if (part2)
                {
                    unitsOfSand++;
                }
                break;
            }

            // At rest
            if (currentSand == temp)
            {
                grid[temp.Y][temp.X] = SAND;
                currentSand = new Vector2d(500, 0);
                // PrintGrid(grid, xMin, xMax, yMax);
                unitsOfSand++;
                continue;
            }


            currentSand = temp;

        }

        return unitsOfSand;
    }

    private static bool IsInside(int yMax, int xMin, int xMax, Vector2d currentSand)
    {
        return currentSand.Y < yMax && currentSand.X > xMin && currentSand.X < xMax;
    }

    private static List<Rock> ParseRocks()
    {
        return File.ReadAllLines("input")
                .Select(l => new Rock(
                    l.Split(" -> ")
                    .Select(part =>
                    {
                        var split = part.Split(",");
                        return new Vector2d(int.Parse(split[0]), int.Parse(split[1]));
                    })
                    .ToList()
                ))
                .ToList();
    }

    const string AIR = ".";
    const string ROCK = "#";
    const string SAND = "O";

    private static void PrintGrid(List<List<string>> grid)
    {
        var rocks = ParseRocks();
        var xMin = rocks.SelectMany(x => x.paths.Select(y => y.X)).SkipLast(1).Min() - 4;
        var xMax = rocks.SelectMany(x => x.paths.Select(y => y.X)).SkipLast(1).Max() + 4;
        var yMax = grid.Count;
        Console.WriteLine("-----------------");
        foreach (var y in Enumerable.Range(0, yMax))
        {
            var row = "";
            foreach (var x in Enumerable.Range(xMin, xMax - xMin))
            {
                row += grid[y][x];
            }
            Console.WriteLine(row);
        }
        Console.WriteLine("-----------------");

    }

    private static void PlaceRocks(List<List<string>> grid, List<Rock> rocks)
    {
        foreach (var rock in rocks)
        {
            for (var i = 0; i < rock.paths.Count; i++)
            {
                if (i + 1 < rock.paths.Count)
                {
                    // right or left
                    var xCount = Math.Abs(rock.paths[i].X - rock.paths[i + 1].X) + 1;
                    var xStart = rock.paths[i].X <= rock.paths[i + 1].X ? rock.paths[i].X : rock.paths[i + 1].X;
                    // up or down
                    var yCount = Math.Abs(rock.paths[i].Y - rock.paths[i + 1].Y) + 1;
                    var yStart = rock.paths[i].Y <= rock.paths[i + 1].Y ? rock.paths[i].Y : rock.paths[i + 1].Y;
                    foreach (var x in Enumerable.Range(xStart, xCount))
                    {
                        foreach (var y in Enumerable.Range(yStart, yCount))
                        {
                            grid[y][x] = ROCK;
                        }

                    }
                }
            }
        }
    }

    private static List<List<string>> CreateGrid(List<Rock> rocks)
    {
        var xMax = rocks.SelectMany(x => x.paths.Select(y => y.X)).Max() + 1000;
        var rockYMax = rocks.SelectMany(x => x.paths.Select(y => y.Y)).Max() + 1;
        var grid = new List<List<string>>();
        for (var y = 0; y < rockYMax; y++)
        {
            var row = new List<string>();
            for (var x = 0; x < xMax; x++)
            {
                row.Add(AIR);
            }
            grid.Add(row);
        }

        var emptyRow = new List<string>();
        for (var i = 0; i < grid[0].Count; i++)
        {
            emptyRow.Add(AIR);
        }
        var floor = new List<string>();
        for (var i = 0; i < grid[0].Count; i++)
        {
            floor.Add(ROCK);
        }

        grid.Add(emptyRow);
        grid.Add(floor);

        return grid;
    }

    record Vector2d(int X, int Y);
    record Rock(List<Vector2d> paths);
}