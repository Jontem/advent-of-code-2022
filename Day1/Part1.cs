using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    public class Part1
    {
        public static void Solve() {
            var elfMax = File.ReadAllText("input")
            .Split("\n\n")
            .Select(elf => elf.Split("\n").Select(r => int.Parse(r)))
            .Select(elfCalories => elfCalories.Sum())
            .Max();
            Console.WriteLine(elfMax);

        }
    }
}