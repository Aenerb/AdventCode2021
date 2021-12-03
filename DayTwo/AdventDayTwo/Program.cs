using System;
using System.Collections.Generic;
using System.IO;

namespace AdventDayTwo
{
    class Program
    {
        // Part One
        static int FinalPositionA(string filePath)
        {
            int hPos = 0;
            int vPos = 0;
            string path = filePath;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] movements = line.Split(' ');

                        var distance = int.Parse(movements[1]);

                        switch (movements[0])
                        {
                            case "forward":
                                hPos += distance;
                                break;
                            case "up":
                                vPos -= distance;
                                break;
                            case "down":
                                vPos += distance;
                                break;
                        }
                    }
                }

                return hPos * vPos;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        // Part Two
        static int FinalPositionB(string filePath)
        {
            int hPos = 0;
            int vPos = 0;
            int aim = 0;
            string path = filePath;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] movements = line.Split(' ');

                        var distance = int.Parse(movements[1]);

                        switch (movements[0])
                        {
                            case "forward":
                                hPos += distance;
                                vPos += (aim * distance);
                                break;
                            case "up":
                                aim -= distance;
                                break;
                            case "down":
                                aim += distance;
                                break;
                        }
                    }
                }

                return hPos * vPos;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        static void Main(string[] args)
        {

            string path = @"C:\Projects\AdventCode2021\DayTwo\Input.txt";

            var finalPositionA = FinalPositionA(path);
            var finalPositionB = FinalPositionB(path);

            Console.WriteLine("Part One: " + finalPositionA);
            Console.WriteLine("Part Two: " + finalPositionB);
        }
    }
}
