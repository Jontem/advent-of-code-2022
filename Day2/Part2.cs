using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day2
{
    public class Part2
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
                {"X", 0},
                {"Y", 3},
                {"Z", 6},

            };
            var roundScores = File.ReadAllLines("input")
            .Select(x =>
             {
                 var elfShape = x.Split(" ")[0];
                 var ending = x.Split(" ")[1];
                 var score = winScore[ending];

                 switch (score)
                 {
                     case 3:
                         return shapeScore[elfShape] + score;
                     case 6:
                         return shapeScore[elfShape == "A" ? "B" : elfShape == "B" ? "C" : "A"] + score;
                     default:
                         return shapeScore[elfShape == "A" ? "C" : elfShape == "B" ? "A" : "B"] + score;
                 }

             });

            Console.WriteLine(roundScores.Sum());
        }
    }
}