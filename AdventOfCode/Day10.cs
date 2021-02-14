using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day10
    {
        public static int[] globalSortedList;
        public static int uniqueCount = 0;
        public static Dictionary<int, int[]> globalAllValidJumps;
        public static Dictionary<int, double> globalUniquePathsToEnd;

        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day10_Input.txt");

            //Timer Start
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //Part1(inputList);
            Part2(inputList);

            //Timer Stop
            sw.Stop();

            Console.WriteLine("Duration: " + sw.Elapsed);

            Console.ReadLine();
        }

        public static void Part1(string[] inputList)
        {
            int[] inputListConverted = inputList.Select(il => Int32.Parse(il)).ToArray();
            int diff = 0;
            Dictionary<int, int> joltCount = new Dictionary<int, int>();
            joltCount.Add(1, 0);
            joltCount.Add(2, 0);
            joltCount.Add(3, 1); //Includes final jump

            inputListConverted = inputListConverted.OrderBy(i => i).ToArray();

            //Initial jump from outlet
            joltCount[inputListConverted[0]] = joltCount[inputListConverted[0]] + 1;

            for (int i = 0; i < inputListConverted.Length - 1; i++)
            {
                diff = inputListConverted[i + 1] - inputListConverted[i];

                joltCount[diff] = joltCount[diff] + 1;
            }

            Console.WriteLine("1-jolt x 3-jolt: " + (joltCount[1] * joltCount[3]));
        }

        public static void Part2(string[] inputList)
        {
            List<int> inputListConverted = inputList.Select(il => Int32.Parse(il)).ToList();
            inputListConverted.Add(0);
            inputListConverted = inputListConverted.OrderBy(i => i).ToList();
            globalSortedList = inputListConverted.ToArray();
            globalUniquePathsToEnd = new Dictionary<int, double>();
            
            CreateValidJumpsList();
            CreateUniquePathsToEndList();

            Console.WriteLine("Unique Lists Found: " + globalUniquePathsToEnd[0]);
        }

        public static void CreateUniquePathsToEndList()
        {
            double runningTotal;
            for(int i = globalSortedList.Length - 2; i >= 0; i--)
            {
                runningTotal = 0;

                foreach(var index in globalAllValidJumps[i])
                {
                    if (index == globalSortedList.Length - 1)
                    {
                        runningTotal++;
                    }
                    else
                    {
                        runningTotal += globalUniquePathsToEnd[index];
                    }
                }

                globalUniquePathsToEnd.Add(i, runningTotal);
            }
        }


        public static void CreateValidJumpsList()
        {
            Dictionary<int, int[]> allValidJumps = new Dictionary<int, int[]>();
            List<int> validJumpsPerIndex;
            int currentJolt;

            for (int i = 0; i < globalSortedList.Length; i++)
            {
                validJumpsPerIndex = new List<int>();
                currentJolt = globalSortedList[i];

                if (i + 1 < globalSortedList.Length
                && globalSortedList[i + 1] - currentJolt <= 3)
                {
                    validJumpsPerIndex.Add(i + 1);
                }

                if (i + 2 < globalSortedList.Length
                    && globalSortedList[i + 2] - currentJolt <= 3)
                {
                    validJumpsPerIndex.Add(i + 2);
                }

                if (i + 3 < globalSortedList.Length
                    && globalSortedList[i + 3] - currentJolt <= 3)
                {
                    validJumpsPerIndex.Add(i + 3);
                }

                allValidJumps.Add(i, validJumpsPerIndex.ToArray());
            }

            globalAllValidJumps = allValidJumps;
        }
    }
}
