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
            .Aggregate(new List<int>(), (acc, seed) =>
            {
                var elf1 = seed[0].ToList();
                var elf2 = seed[1].ToList();
                var elf3 = seed[2].ToList();
                var set = new HashSet<char>();
                foreach (var c1 in elf1)
                {
                    if (elf2.IndexOf(c1) > -1 && elf3.IndexOf(c1) > -1 && !set.Contains(c1))
                    {
                        set.Add(c1);
                    }
                }
                acc.Add(set.Select((c) => priorities.IndexOf(c) + 1).Sum());
                return acc;
            });

            Console.WriteLine(sums.Sum());
        }
    }
}