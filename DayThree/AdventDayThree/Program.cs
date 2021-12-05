using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventDayThree
{
    class Program
    {
        // Part One
        static int PartOne(string filePath)
        {
            int[] gammaRate = new int[12];
            int[] epsilonRate = new int[12];
            // You don't need this. Stop it. Just use the totals and change the gamma rate.
            int[] bitTotals = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int counter = 0;

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            bitTotals[i] += line[i] == '1' ? 1 : -1;
                        }
                    }
                }

                for (int j = 0; j < bitTotals.Length; j++)
                {
                    if (bitTotals[j] >= 0)
                    {
                        gammaRate[j] = 1;
                        epsilonRate[j] = 0;
                    }
                    else
                    {
                        gammaRate[j] = 0;
                        epsilonRate[j] = 1;
                    }
                }

                var gammaValue = string.Join(string.Empty, gammaRate);
                var epsilonValue = string.Join(string.Empty, epsilonRate);

                var powerConsumption = Convert.ToInt32(gammaValue, 2) * Convert.ToInt32(epsilonValue, 2);

                Console.WriteLine("Total items: {0}", counter);
                Console.WriteLine("gammaValue: {0}", gammaValue);
                Console.WriteLine("epsilonValue: {0}", epsilonValue);

                return powerConsumption;

            }
            catch (Exception e)
            {
                return -1;
            }
        }


        static int DetermineCommonValue(List<string> values, int position)
        {
            int totalValue = 0;
            foreach (var value in values)
            {
                int[] bits = value.Select(c => int.Parse(c.ToString())).ToArray();
                totalValue += bits[position];
            }

            if (totalValue > values.Count - totalValue)
            {
                // 1 is more common. Return 1
                return 1;
            }
            else if (totalValue == values.Count - totalValue)
            {
                // 1 and 0 are equally common. Return 1
                return 1;
            }
            else
            {
                // 0 is more common. Return 0
                return 0;
            }
        }

        static int DetermineLeastCommonValue(List<string> values, int position)
        {
            int totalValue = 0;
            foreach (var value in values)
            {
                int[] bits = value.Select(c => int.Parse(c.ToString())).ToArray();
                totalValue += bits[position];
            }

            if (totalValue > values.Count - totalValue)
            {
                // 1 is more common. Return 0
                return 0;
            }
            else if (totalValue == values.Count - totalValue)
            {
                // 1 and 0 are equally common. Return 0
                return 0;
            }
            else
            {
                // 0 is more common. Return 1
                return 1;
            }
        }

        // Part Two
        static int PartTwo(string filePath)
        {
            var file = File.ReadAllLines(filePath);
            var oxyList = new List<string>(file);
            var co2List = new List<string>(file);

            // get length of first element... it's always 12, but I don't want magic numbers.
            var firstElement = oxyList.First();

            for (int i = 0; i < firstElement.Length; i++)
            {
                var removeOxyList = new List<string>();
                var commonValue = DetermineCommonValue(oxyList, i);

                foreach (var element in oxyList)
                {
                    int[] bits = element.Select(c => int.Parse(c.ToString())).ToArray();
                    if (bits[i] != commonValue)
                    {
                        removeOxyList.Add(element);
                    }

                }

                oxyList.RemoveAll(x => removeOxyList.Contains(x));

                if (oxyList.Count == 1)
                {
                    // No need to keep going
                    break;
                }

            }
            for (int j = 0; j < firstElement.Length; j++)
            {
                var removeCo2List = new List<string>();
                var leastValue = DetermineLeastCommonValue(co2List, j);

                foreach (var element in co2List)
                {
                    int[] bits = element.Select(c => int.Parse(c.ToString())).ToArray();

                    if (bits[j] != leastValue)
                    {
                        removeCo2List.Add(element);
                    }
                }

                co2List.RemoveAll(x => removeCo2List.Contains(x));

                if (co2List.Count == 1)
                {
                    break;
                }
            }

            return Convert.ToInt32(co2List.First(), 2) * Convert.ToInt32(oxyList.First(), 2);

        }

        static void Main(string[] args)
        {

            string path = @"C:\Projects\AdventCode2021\DayThree\input.txt";

            var partOne = PartOne(path);
            var partTwo = PartTwo(path);

            Console.WriteLine("Part One: " + partOne);
            Console.WriteLine("Part Two: " + partTwo);
        }
    }
}
