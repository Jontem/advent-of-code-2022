using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day5
{
    public class Part1
    {
        public static void Solve()
        {
            var inputs = File.ReadAllText("input")
            .Split("\n\n");
            var stacks = ParseStacks(inputs[0]);
            var moves = ParseMoves(inputs[1]);
            PrintStacks(stacks);

            foreach (var move in moves)
            {
                var fromStack = stacks[move.From - 1];
                var toStack = stacks[move.To - 1];
                for (var i = 0; i < move.Count; i++)
                {
                    var from = fromStack.Pop();
                    toStack.Push(from);
                }
                PrintStacks(stacks);
            }

            var top = "";
            foreach (var stack in stacks)
            {
                top += stack.Peek();
            }
            Console.WriteLine(top);
        }


        private static IList<Stack<string>> ParseStacks(string input)
        {
            var stacks = new List<Stack<string>>();
            var lines = input.Split("\n");
            foreach (var line in lines.Reverse())
            {
                var parts = line.ToList().Chunk(4).Select(x => string.Join("", x)).ToList();

                for (var i = 0; i < parts.Count; i++)
                {
                    var part = parts[i];
                    if (stacks.ElementAtOrDefault(i) == null)
                    {
                        stacks.Add(new Stack<string>());
                    }
                    var stack = stacks.ElementAt(i);
                    if (!string.IsNullOrWhiteSpace(part) && !int.TryParse(part.Trim(), out _))
                    {
                        stack.Push(part.Trim().Replace("[", "").Replace("]", ""));
                    }

                }
            }

            return stacks;
        }

        private record Move(int Count, int From, int To);
        private static IList<Move> ParseMoves(string input)
        {
            var regex = new Regex("move (\\d+) from (\\d+) to (\\d+)");
            return input.Split("\n").Select(line =>
            {
                var matches = regex.Match(line);
                var count = int.Parse(matches.Groups[1].Value);
                var from = int.Parse(matches.Groups[2].Value);
                var to = int.Parse(matches.Groups[3].Value);
                return new Move(count, from, to);
            })
            .ToList();
        }

        private static void PrintStacks(IList<Stack<string>> stacks)
        {
            Console.WriteLine("---- new stack ---");
            var lines = new List<string>();
            var stackMax = stacks.Max(x => x.Count);
            for (var i = 1; i <= stackMax; i++)
            {
                var line = "";
                for (var j = 0; j < stacks.Count(); j++)
                {
                    var stack = stacks[j];
                    var val = stack.ElementAtOrDefault(stack.Count - i);

                    var emptyStr = "    ";
                    line += val != null ? $"[{val}] " : emptyStr;
                }

                lines.Add(line);
            }
            lines.Reverse();
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}