﻿// how many pairs of packets are in the right order
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
    }

    public static void Solve2()
    {
    }
}