// how many pairs of packets are in the right order
// When comparing two values, the first value is called left and the second value is called right. Then:

// If both values are integers, the lower integer should come first. 
// If the left integer is lower than the right integer, the inputs are in the right order.
// If the left integer is higher than the right integer, the inputs are not in the right order. Otherwise, the inputs are the same integer; continue checking the next part of the input.

// If both values are lists, compare the first value of each list, then the second value, and so on.
// If the left list runs out of items first, the inputs are in the right order.
// If the right list runs out of items first, the inputs are not in the right order.
// If the lists are the same length and no comparison makes a decision about the order, continue checking the next part of the input.

// If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison.
// For example, if comparing [0,0,0] and 2, convert the right value to [2] (a list containing 2); the result is then found by instead comparing [0,0,0] and [2].

using System.Text.Json;

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
        var pairs = Parse();
        var indexes = new List<int>();
        for (var i = 0; i < pairs.Count; i++)
        {
            var idx = (i + 1);
            var (left, right) = pairs[i];
            var comparison = Compare(left, right);

            if (comparison == -1)
            {
                indexes.Add(idx);
            }
        }

        Console.WriteLine("Sum: " + indexes.Sum());
    }

    public static void Solve2()
    {
        var pairs = Parse();
        var two = new Packet.PacketList(new List<Packet> { new Packet.PacketList(new List<Packet> { new Packet.PrimitivePacket(2) }) });
        var six = new Packet.PacketList(new List<Packet> { new Packet.PacketList(new List<Packet> { new Packet.PrimitivePacket(6) }) });
        pairs.Add((two, six));

        var allPackets = pairs
        .Select(packetPair => new List<Packet> { packetPair.Item1, packetPair.Item2 })
        .SelectMany(x => x)
        .ToList();

        allPackets.Sort(Compare);

        var index2 = allPackets.FindIndex(ap => ap == two) + 1;
        var index6 = allPackets.FindIndex(ap => ap == six) + 1;

        Console.WriteLine("Product: " + index2 * index6);
    }

    record Packet
    {
        public record PrimitivePacket(int Value) : Packet;
        public record PacketList(List<Packet> Items) : Packet
        {
            public override string ToString()
            {
                var str = "[";
                foreach (var item in Items)
                {
                    if (item is Packet.PrimitivePacket p)
                    {
                        str += ", " + p.Value;
                    }
                    else
                    {
                        str += item.ToString();
                    }
                }
                str += "]";
                return str.Replace("[,", "[");
            }
        }
    }

    private static List<(Packet, Packet)> Parse()
    {
        var pairs = new List<(Packet, Packet)>();
        foreach (var pairRow in File.ReadAllText("input").Split("\n\n"))
        {
            var split = pairRow.Split("\n");
            var left = Parse(split[0]);
            var right = Parse(split[1]);
            pairs.Add((left, right));
        }

        return pairs;
    }

    private static int Compare(Packet left, Packet right)
    {
        if (left is Packet.PrimitivePacket leftPrimitive && right is Packet.PrimitivePacket righPrimitive)
        {
            if (leftPrimitive.Value < righPrimitive.Value)
            {
                return -1;
            }
            else if (leftPrimitive.Value > righPrimitive.Value)
            {
                return 1;
            }

            return 0;
        }
        else if (left is Packet.PacketList leftList && right is Packet.PacketList rightList)
        {
            for (var i = 0; i < Math.Max(leftList.Items.Count, rightList.Items.Count); i++)
            {
                if (i >= leftList.Items.Count)
                {
                    return -1;
                }
                else if (i >= rightList.Items.Count)
                {
                    return 1;
                }
                else
                {
                    var comparison = Compare(leftList.Items[i], rightList.Items[i]);
                    if (comparison != 0)
                    {
                        return comparison;
                    }
                }
            }
        }
        else if (left is Packet.PrimitivePacket leftPrimitive1 && right is Packet.PacketList rightList1)
        {
            return Compare(new Packet.PacketList(new List<Packet>(new List<Packet> { leftPrimitive1 })), rightList1);
        }
        else if (left is Packet.PacketList leftList1 && right is Packet.PrimitivePacket right1)
        {
            return Compare(leftList1, new Packet.PacketList(new List<Packet> { right1 }));
        }

        return 0;
    }

    private static Packet Parse(string packet)
    {
        var e = JsonDocument.Parse(packet).RootElement;
        if (e.ValueKind == JsonValueKind.Array)
        {
            var packets = new List<Packet>();
            foreach (var sub in e.EnumerateArray().ToList())
            {
                packets.Add(Parse(sub.ToString()));
            }
            return new Packet.PacketList(packets);
        }
        else
        {
            return new Packet.PrimitivePacket(e.GetInt32());
        }
    }
}