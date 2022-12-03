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
            .Select((rucksack) =>
            {
                var compartment1 = rucksack
                .Substring(0, rucksack.Length / 2)
                .ToList();
                var compartment2 = rucksack
                .Substring(rucksack.Length / 2)
                .ToList();

                return compartment1
                .Intersect(compartment2)
                .Select((c) => priorities.IndexOf(c) + 1)
                .Sum();
            });

            Console.WriteLine(sums.Sum());
        }
    }
}