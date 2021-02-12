using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day7
    {
        private static Dictionary<string, Dictionary<string, int>> bagList = new Dictionary<string, Dictionary<string, int>>();

        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day7_Input.txt");

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
            string inputFormatted;
            //Dictionary<string, Dictionary<string, int>> bagList = new Dictionary<string, Dictionary<string, int>>();
            string[] inputSplit;
            string[] inputSecondSplitList;

            //Insert into related list
            foreach (var input in inputList)
            {
                inputFormatted = input.Replace("bags", "bag").Replace(" bag", "").Replace(".","");
                inputSplit = inputFormatted.Split(" contain ");
                
                bagList.Add(inputSplit[0], new Dictionary<string, int>());

                inputSecondSplitList = inputSplit[1].Split(", ");

                foreach (var inputSecondSplit in inputSecondSplitList) 
                {
                    if (!inputSecondSplit.Equals("no other"))
                    {
                        bagList[inputSplit[0]].Add(inputSecondSplit.Substring(2), Int32.Parse(inputSecondSplit.Substring(0, 1)));
                    }
                }
            }

            int bagCount = 0;

            foreach(var bag in bagList)
            {
                if(ContainsSpecificBag(bag.Key))
                {
                    bagCount++;
                }
            }
            //Part 1
            Console.WriteLine("Bags containing at least 1 'shiny gold' bag: " + bagCount);

            //Part 2
            Console.WriteLine("Inner bags of 'shiny gold' bag: " + (CountInnerBagForSpecific("shiny gold") - 1));

            Console.WriteLine("");
        }

        private static bool ContainsSpecificBag(string bagInspected)
        {
            bool anyTrue = false;
            foreach(var bag in bagList[bagInspected])
            {
                if (bag.Key == "shiny gold")
                {
                    anyTrue = true;
                }
                else
                {
                    anyTrue = anyTrue || ContainsSpecificBag(bag.Key);
                }
            }

            return anyTrue;
        }

        private static int CountInnerBagForSpecific(string bagInspected)
        {
            int innerBagCount = 1;

            if (!bagList[bagInspected].Any())
            {
                return innerBagCount;
            }

            foreach (var bag in bagList[bagInspected])
            {
                innerBagCount += bag.Value * CountInnerBagForSpecific(bag.Key);
            }

            return innerBagCount;
        }
    }
}
