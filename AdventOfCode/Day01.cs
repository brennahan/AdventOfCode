using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode
{
    class Day01
    {
        public static void StartProcess()
        {
            //Some sort of loop to find what adds up to 2020
            //Multiply the specified numbers together
            string[] inputRawList = System.IO.File.ReadAllLines("../../../Input/Day01_Input.txt");
            int[] inputList = new int[inputRawList.Length];

            for (int i = 0; i < inputRawList.Length; i++)
            {
                Int32.TryParse(inputRawList[i], out inputList[i]);
            }

            int result;

            //Timer Start
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //result = BruteForce(inputList);
            result = BruteForceP2(inputList);

            //Timer Stop
            sw.Stop();

            Console.WriteLine("Answer: " + result);
            Console.WriteLine("Duration: " + sw.Elapsed);

            Console.ReadLine();
        }

        public static int BruteForce(int[] inputList)
        {
            for (int i = 0; i < inputList.Length-1; i++)
            {
                for (int j = i + 1; j < inputList.Length; j++)
                {
                    if (inputList[i] + inputList[j] == 2020)
                    {
                        return inputList[i] * inputList[j];
                    }
                }
            }

            return -1;
        }

        public static int BruteForceP2(int[] inputList)
        {
            for (int i = 0; i < inputList.Length - 2; i++)
            {
                for (int j = i + 1; j < inputList.Length - 1; j++)
                {
                    for (int k = j + 1; k < inputList.Length; k++)
                    {
                        if (inputList[i] + inputList[j] + inputList[k] == 2020)
                        {
                            return inputList[i] * inputList[j] * inputList[k];
                        }
                    }
                }
            }

            return -1;
        }


        /*Brainstorming section
         * 1. Brute force. For-loop inside for-loop til you find what adds to 2020, then break out. Complexity: N^2, dogshit
         * 2. Sort the entries in a list. Then, go through somehow and do... whatever. Complexity: nlogn + 
         * 3. Loop 
         */
    }
}
