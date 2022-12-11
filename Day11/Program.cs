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
                    var nextMonkey = worryLevel % monkey.WorryLevelDivisor == 0 ? monkeys[monkey.TrueMonkey] : monkeys[monkey.FalseMonkey];
                    nextMonkey.Items.Enqueue(worryLevel);

                    monkeyHist[monkey.id]++;
                }
            }
        }

        Console.WriteLine(monkeyHist.OrderBy(x => x).TakeLast(2).Aggregate((a, b) => a * b));
    }

    public static void Solve2()
    {
        var monkeys = ParseMonkeys();
        var monkeyInspections = monkeys.Select(_ => (long)0).ToList();
        var commonMultiple = monkeys.Select(x => x.WorryLevelDivisor).Aggregate((a, b) => a * b);
        for (var i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    var item = monkey.Items.Dequeue();
                    long worryLevelRemainder = monkey.CalculateWorryLevel(item) % commonMultiple;
                    var nextMonkey = worryLevelRemainder % monkey.WorryLevelDivisor == 0 ? monkeys[monkey.TrueMonkey] : monkeys[monkey.FalseMonkey];
                    nextMonkey.Items.Enqueue(worryLevelRemainder);
                    monkeyInspections[monkey.id]++;
                }
            }
        }

        Console.WriteLine(monkeyInspections.OrderBy(x => x).TakeLast(2).Aggregate((a, b) => a * b));
    }

    private static void printWorryLevels(List<Monkey> monkeys, List<long> monkeyHist)
    {
        foreach (var m in monkeys)
        {
            Console.WriteLine($"Monkey{m.id}: Times: {monkeyHist[m.id]}, Items: {string.Join(",", m.Items)}");
        }
    }

    private record Monkey(int id, Queue<long> Items, Func<long, long> CalculateWorryLevel, int WorryLevelDivisor, int TrueMonkey, int FalseMonkey);
    private static List<Monkey> ParseMonkeys()
    {
        var regex = new Regex(".+?\\n\\s\\sStarting items:(.+?)\\n\\s\\sOperation: (.+?)\\n\\s\\sTest: .+?(\\d+)\n.+?(\\d+?)\\n.+?(\\d+?)");
        var monkeys = new List<Monkey>();
        var i = 0;
        foreach (var monkey in File.ReadAllText("input").Split("\n\n"))
        {
            var match = regex.Match(monkey);
            var items = new Queue<long>(match.Groups[1].Value.Split(",").Select(long.Parse));
            var operation = ParseOperation(match.Groups[2].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            var divisebleBy = match.Groups[3].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()[0];
            var trueMonkey = match.Groups[4].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()[0];
            var falseMonkey = match.Groups[5].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()[0];
            monkeys.Add(new Monkey(i, items, operation, divisebleBy, trueMonkey, falseMonkey));
            i++;
        }

        return monkeys;
    }

    private static Func<long, long> ParseOperation(string[] operation)
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
}