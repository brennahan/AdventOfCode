using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day3
    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day3_Input.txt");

            //Timer Start
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //Part1(inputList, 3, 1);
            Part2(inputList);

            //Timer Stop
            sw.Stop();

            Console.WriteLine("Duration: " + sw.Elapsed);

            Console.ReadLine();
        }

        public static void Part1(string[] inputList, int xMovement, int yMovement)
        {
            int xIndex = 0;
            int yIndex = 0;
            int treeHitCount = 0;

            //Convert to true/false 2D array..?

            //Iterate movement, making sure to use Remainder each time.
            do
            {
                //Movement
                xIndex = (xIndex + xMovement) % inputList[0].Length;
                yIndex = (yIndex + yMovement);

                //Check spot for Tree
                if (inputList[yIndex][xIndex] == '#')
                {
                    treeHitCount++;
                }


            } while (yIndex < inputList.Length - 1);

            Console.WriteLine(xMovement + "_" + yMovement + "_Trees Hit: " + treeHitCount + " after " + inputList.Length + " movements");
        }

        public static void Part2(string[] inputList)
        {
            Part1(inputList, 1, 1);
            Part1(inputList, 3, 1);
            Part1(inputList, 5, 1);
            Part1(inputList, 7, 1);
            Part1(inputList, 1, 2);
        }
    }
}
