using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3
{
    public class Part2
    {
        public static void Solve()
        {
            var priorities = Enumerable.Range(97, 26)
            .Concat(Enumerable.Range(65, 26))
            .Select(i => (char)i)
            .ToList();

            var sums = File.ReadAllLines("input")
            .Chunk(3)
            .Select((elfGroup) =>
            {
                var elf1 = elfGroup[0].ToList();
                var elf2 = elfGroup[1].ToList();
                var elf3 = elfGroup[2].ToList();
                return elf1
                .Intersect(elf2)
                .Intersect(elf3)
                .Select((c) => priorities.IndexOf(c) + 1)
                .Sum();
            });

            Console.WriteLine(sums.Sum());
        }
    }
}