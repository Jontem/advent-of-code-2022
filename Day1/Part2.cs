using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    public class Part2
    {
        public static void Solve()
        {
            var topThreeSum = File.ReadAllText("input")
            .Split("\n\n")
            .Select(elf => elf.Split("\n").Select(r => int.Parse(r)))
            .Select(elfCalories => elfCalories.Sum())
            .OrderBy(x => x)
            .TakeLast(3)
            .Sum();
            Console.WriteLine(string.Join(", ", topThreeSum));

        }
    }
}