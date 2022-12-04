using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
    public class Part2
    {
        public static void Solve()
        {
            var overlapping = File.ReadAllLines("input")
            .Select(line =>
            {
                var section = line.Split(",");
                return (GetPair(section[0]), GetPair(section[1]));
            })
            .Where(section =>
            {
                var (firstPair, secondPair) = section;
                return firstPair.Any(fp => secondPair.Contains(fp)) || secondPair.Any(x => firstPair.Contains(x));
            });

            Console.WriteLine(overlapping.Count());
        }

        private static List<int> GetPair(string pair)
        {
            var split = pair.Split("-");
            var start = int.Parse(split[0]);
            var end = int.Parse(split[1]);
            return Enumerable
            .Range(start, end - start + 1)
            .ToList();
        }
    }
}