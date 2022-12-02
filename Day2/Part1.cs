using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day2
{
    public class Part1
    {
        // A rock, B paper, C scissors,
        public static void Solve()
        {
            var shapeScore = new Dictionary<string, int> {
                {"A", 1},
                {"X", 1},
                {"B", 2},
                {"Y", 2},
                {"C", 3},
                {"Z", 3},

            };
            var winScore = new Dictionary<string, int> {
                {"A X", 3},
                {"A Y", 6},
                {"A Z", 0},
                {"B X", 0},
                {"B Y", 3},
                {"B Z", 6},
                {"C X", 6},
                {"C Y", 0},
                {"C Z", 3},

            };
            var roundScores = File.ReadAllLines("input").Select(x =>
             {
                 var myShape = x.Split(" ")[1];
                 return shapeScore[myShape] + (winScore.ContainsKey(x) ? winScore[x] : 0);
             });

            Console.WriteLine(roundScores.Sum());
        }
    }
}