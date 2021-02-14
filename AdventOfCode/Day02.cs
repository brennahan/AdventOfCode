using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day02
    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day02_Input.txt");

            //Timer Start
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //int result = Part1(inputList);
            int result = Part2(inputList);

            //Timer Stop
            sw.Stop();

            Console.WriteLine("Good Passwords: " + result + "/" + inputList.Length);
            Console.WriteLine("Duration: " + sw.Elapsed);

            Console.ReadLine();
        }

        public static int Part1(string[] inputList)
        {
            int tackIndex;
            int spaceIndex;
            char letterReq;
            int min;
            int max;
            string password;
            int foundCount;
            int goodPassCount = 0;

            //Do the Thing
            foreach (string input in inputList)
            {
                tackIndex = input.IndexOf('-');
                spaceIndex = input.IndexOf(' ');
                letterReq = input[spaceIndex + 1];

                Int32.TryParse(input.Substring(0, tackIndex), out min);
                Int32.TryParse(input.Substring(tackIndex + 1, spaceIndex - tackIndex), out max);
                password = input.Substring(input.IndexOf(':') + 2);

                foundCount = password.Count(p => p == letterReq);

                if (foundCount >= min && foundCount <= max)
                {
                    goodPassCount++;
                }
            }

            return goodPassCount;
        }

        public static int Part2(string[] inputList)
        {
            int tackIndex;
            int spaceIndex;
            char letterReq;
            int firstSpot;
            int secondSpot;
            string password;
            int goodPassCount = 0;

            //Do the Thing
            foreach (string input in inputList)
            {
                tackIndex = input.IndexOf('-');
                spaceIndex = input.IndexOf(' ');
                letterReq = input[spaceIndex + 1];

                Int32.TryParse(input.Substring(0, tackIndex), out firstSpot);
                Int32.TryParse(input.Substring(tackIndex + 1, spaceIndex - tackIndex), out secondSpot);
                password = input.Substring(input.IndexOf(':') + 2);

                if ((firstSpot <= password.Length && password[firstSpot - 1] == letterReq) 
                    ^ (secondSpot <= password.Length && password[secondSpot - 1] == letterReq))
                {
                    goodPassCount++;
                }
            }

            return goodPassCount;
        }
    }
}
