using System;
using System.Collections.Generic;
using System.IO;

namespace AdventDayOne
{
    class Program
    {

        static int PartOne(string[] input)
        {
            int total = 0;
            for (int count = 1; count < input.Length; count++)
            {
                int previous = int.Parse(input[count - 1]);
                int current = int.Parse(input[count]);

                if (current > previous)
                {
                    total++;
                }
            }
            return total;
        }

        static int PartTwo(string[] input)
        {
            int previous = 0;
            int total = 0;
            for (int count = 3; count < input.Length; count++)
            {
                int first = int.Parse(input[count - 2]);
                int second = int.Parse(input[count - 1]);
                int third = int.Parse(input[count]);

                int current = first + second + third;
                if (current > previous)
                {
                    total++;
                }
                previous = current;
            }
            return total;
        }

        static void Main(string[] args)
        {
            string[] inputNumbers = System.IO.File.ReadAllLines(@"C:\Projects\AdventCode2021\DayOne\input.txt");

            Console.WriteLine("Total Part One: " + PartOne(inputNumbers));
            Console.WriteLine("Total Part Two: " + PartTwo(inputNumbers));
        }
    }
}
