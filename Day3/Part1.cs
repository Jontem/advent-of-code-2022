using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3
{
    public class Part1
    {
        public static void Solve()
        {
            var priorities = Enumerable.Range(97, 26)
            .Concat(Enumerable.Range(65, 26))
            .Select(i => (char)i)
            .ToList();

            var sums = File.ReadAllLines("input")
            .Aggregate(new List<int>(), (acc, seed) =>
            {
                var c1s = seed.Substring(0, seed.Length / 2)
                .ToList();
                var c2s = seed.Substring(seed.Length / 2)
                .ToList();

                var set = new HashSet<Char>();
                foreach (var c1 in c1s)
                {
                    if (c2s.IndexOf(c1) > -1 && !set.Contains(c1))
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