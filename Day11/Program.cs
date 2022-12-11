using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("------------- Part 1 -------------");
        Solve1();
        Console.WriteLine("------------- Part 2 -------------");
        Solve2();
    }



    public static void Solve1()
    {
        var monkeys = ParseMonkeys();
        var monkeyHist = monkeys.Select(_ => 0).ToList();
        for (var i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count != 0)
                {
                    var item = monkey.Items.Dequeue();
                    var worryLevel = monkey.CalculateWorryLevel(item) / 3;
                    if (worryLevel % monkey.WorryLevelDivisor == 0)
                    {
                        monkeys[monkey.TrueMonkey].Items.Enqueue(worryLevel);
                    }
                    else
                    {
                        monkeys[monkey.FalseMonkey].Items.Enqueue(worryLevel);
                    }

                    monkeyHist[monkey.id]++;
                }
            }
        }

        // foreach (var m in monkeys)
        // {
        // Console.WriteLine($"Monkey{m.id}: Times: {monkeyHist[m.id]}, Items: {string.Join(",", m.Items)}");
        // }
        Console.WriteLine(monkeyHist.OrderBy(x => x).TakeLast(2).Aggregate((a, b) => a * b));
    }

    private record Monkey(int id, Queue<int> Items, Func<int, int> CalculateWorryLevel, int WorryLevelDivisor, int TrueMonkey, int FalseMonkey);
    private static List<Monkey> ParseMonkeys()
    {
        var regex = new Regex(".+?\\n\\s\\sStarting items:(.+?)\\n\\s\\sOperation: (.+?)\\n\\s\\sTest: .+?(\\d+)\n.+?(\\d+?)\\n.+?(\\d+?)");
        var monkeys = new List<Monkey>();
        var i = 0;
        foreach (var monkey in File.ReadAllText("input").Split("\n\n"))
        {
            var match = regex.Match(monkey);
            var items = new Queue<int>(match.Groups[1].Value.Split(",").Select(int.Parse));
            var operation = ParseOperation(match.Groups[2].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            var divisebleBy = match.Groups[3].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()[0];
            var trueMonkey = match.Groups[4].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()[0];
            var falseMonkey = match.Groups[5].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()[0];
            monkeys.Add(new Monkey(i, items, operation, divisebleBy, trueMonkey, falseMonkey));
            i++;
        }

        return monkeys;
    }

    private static Func<int, int> ParseOperation(string[] operation)
    {
        var op = operation[operation.Length - 2];

        switch (op)
        {
            case "*":
                return (operand1) =>
                {
                    var operand2Str = operation[operation.Length - 1];
                    var operand2 = operand2Str == "old" ? operand1 : int.Parse(operation[operation.Length - 1]);
                    return operand1 * operand2;
                };
            case "+":
                return (operand1) =>
                {
                    var operand2Str = operation[operation.Length - 1];
                    var operand2 = operand2Str == "old" ? operand1 : int.Parse(operation[operation.Length - 1]);
                    return operand1 + operand2;
                };
            default:
                throw new Exception("No no");
        }
    }

    public static void Solve2()
    {
    }
}