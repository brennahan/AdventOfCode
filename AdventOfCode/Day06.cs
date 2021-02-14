using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day06

    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day06_Input.txt");

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
            int sequenceId = 0;
            Dictionary<int, string> answerList = new Dictionary<int, string>();

            foreach (string input in inputList)
            {
                //Different entry detected
                if (String.IsNullOrEmpty(input))
                {
                    sequenceId++;
                    continue;
                }

                foreach (char answer in input)
                {
                    if (!answerList.ContainsKey(sequenceId)){
                        answerList.Add(sequenceId, answer.ToString());
                    }
                    else
                    {
                        if (!answerList[sequenceId].Contains(answer))
                        {
                            answerList[sequenceId] = String.Concat(answerList[sequenceId], answer);
                        }
                    }
                }
            }

            int totalUniqueCount = 0;

            foreach(var uniqueAnswers in answerList)
            {
                totalUniqueCount += uniqueAnswers.Value.Length;
            }

            Console.WriteLine("Sum of unique 'yes' group-answers: " + totalUniqueCount);
        }

        public static void Part2(string[] inputList)
        {
            int groupId = 0;
            int sequenceId = 0;
            Dictionary<int, string> answerList = new Dictionary<int, string>();
            Dictionary<int, int> groupMemberCountList = new Dictionary<int, int>();
            groupMemberCountList.Add(groupId, 0);

            foreach (string input in inputList)
            {
                //Different entry detected
                if (String.IsNullOrEmpty(input))
                {
                    groupId++;
                    groupMemberCountList.Add(groupId, 0);
                    sequenceId = 0;
                    continue;
                }

                groupMemberCountList[groupId] = groupMemberCountList[groupId] + 1;

                if (!answerList.ContainsKey(groupId))
                {
                    answerList.Add(groupId, input);
                }
                else
                {
                    answerList[groupId] = String.Concat(answerList[groupId], input);
                }

                sequenceId++;
            }

            int totalUnanimousCount = 0;

            foreach (var groupCountEntry in groupMemberCountList)
            {
                char[] possibleAnswers = new char[26] { 
                    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 
                    'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' 
                };

                foreach(var possible in possibleAnswers)
                {
                    if (answerList[groupCountEntry.Key].Count(e => e == possible) == groupCountEntry.Value)
                    {
                        totalUnanimousCount++;
                    }
                }
            }

            Console.WriteLine("Sum of unique 'yes' group-answers: " + totalUnanimousCount);
        }
    }
}
