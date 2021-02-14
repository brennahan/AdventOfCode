using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day09
    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day09_Input.txt");

            //Timer Start
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Part1(inputList);

            //Timer Stop
            sw.Stop();

            Console.WriteLine("Duration: " + sw.Elapsed);

            Console.ReadLine();
        }

        public static void Part1(string[] inputList)
        {
            long[] inputListConverted = inputList.Select(il => Int64.Parse(il)).ToArray();
            int index = 25;
            bool foundMatch;

            //Identify the Part 1 Number
            do
            {
                foundMatch = false;
                for(int i = 1; i <= 24 && !foundMatch; i++)
                {
                    for(int j = i + 1; j <= 25 && !foundMatch; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (inputListConverted[index-i] + inputListConverted[index-j] == inputListConverted[index])
                        {
                            foundMatch = true;
                        }
                    }
                }

                if (foundMatch)
                {
                    index++;
                }
                else
                {
                    break;
                }
            } while (true);

            Console.WriteLine("Index: " + index + ", Number: " + inputList[index]);

            //Identify the Part 2 Number

            long currentSum;
            int currentIndex = -1;
            int additionalCount;

            //Some Sort of loop
            do
            {
                additionalCount = 1;
                currentIndex++;
                currentSum = inputListConverted[currentIndex] + inputListConverted[currentIndex + 1];

                while (currentSum < inputListConverted[index] && currentIndex + additionalCount - 1 < inputListConverted.Length)
                {
                    additionalCount++;
                    currentSum += inputListConverted[currentIndex + additionalCount];
                }
            } while (currentSum != inputListConverted[index] && currentIndex + 1 <= index);

            var subList = inputListConverted.ToList().GetRange(currentIndex, additionalCount);

            Console.WriteLine("Index range: " + currentIndex + "-" + (currentIndex + additionalCount));
            Console.WriteLine("Weakness: " + (subList.Min() + subList.Max()));
        }
    }
}
