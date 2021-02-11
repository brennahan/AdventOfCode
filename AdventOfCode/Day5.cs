using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day5

    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day5_Input.txt");

            //Timer Start
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Process(inputList);

            //Timer Stop
            sw.Stop();

            Console.WriteLine("Duration: " + sw.Elapsed);

            Console.ReadLine();
        }

        public static void Process(string[] inputList)
        {
            string yInput, xInput;
            int yIndex, yLower, yUpper, xIndex, xLower, xUpper, id;
            int maxId = -1;
            HashSet<int> idList = new HashSet<int>();

            foreach (var input in inputList)
            {

                yIndex = 0;
                yLower = 0;
                yUpper = 127;
                xIndex = 0;
                xLower = 0;
                xUpper = 7;

                yInput = input.Substring(0, 7);
                xInput = input.Substring(7, 3);

                //Scan y - 7 characters
                do
                {
                    if (yInput[yIndex] == 'F')
                    {
                        yUpper = yUpper - ((yUpper - yLower + 1) / 2);
                    }
                    else
                    {
                        yLower = yLower + ((yUpper - yLower + 1) / 2);
                    }

                    yIndex++;
                } while (yIndex < 7);

                //Scan x - 3 characters
                do
                {
                    if (xInput[xIndex] == 'L')
                    {
                        xUpper = xUpper - ((xUpper - xLower + 1) / 2);
                    }
                    else
                    {
                        xLower = xLower + ((xUpper - xLower + 1) / 2);
                    }

                    xIndex++;
                } while (xIndex < 3);

                id = yUpper * 8 + xUpper;

                if (maxId < id)
                    maxId = id;

                idList.Add(id);
                Console.WriteLine("Row: " + yUpper + ", Column: " + xUpper + ", Id: " + id);
            }

            for(int i = 0; i < maxId - 1; i++)
            {
                if (idList.Contains(i-1)
                    && !idList.Contains(i)
                    && idList.Contains(i + 1))
                {
                    Console.WriteLine("Your ID: " + i + ", Row: " + ((int) i / 8) + ", Column: " + i % 8);
                }
            }

            Console.WriteLine("Max Id: " + maxId);
        }
    }
}
