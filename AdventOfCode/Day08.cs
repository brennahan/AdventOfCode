using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day08
    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day08_Input.txt");

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
            HashSet<int> executedLines = new HashSet<int>();
            int acc = 0;
            int currentLine = 0;
            string[] currentCommand;
            int amount;
            do
            {
                currentCommand = inputList[currentLine].Split(' ');
                amount = Int32.Parse(currentCommand[1].Replace("+", ""));
                executedLines.Add(currentLine);

                if (currentCommand[0].Equals("acc"))
                {
                    acc += amount;
                    currentLine++;
                }
                else if (currentCommand[0].Equals("jmp"))
                {
                    currentLine += amount;
                }
                else if (currentCommand[0].Equals("nop"))
                {
                    currentLine++;
                }
            } while (!executedLines.Contains(currentLine));

            Console.WriteLine("Accumulator before loop detected: " + acc);
        }

        public static void Part2(string[] inputList)
        {
            int acc;
            int currentLine;
            int currentJmpOrNop;
            int swapJmpOrNopFromStart = -1;
            string[] currentCommandCombo;
            string command;
            int amount;
            HashSet<int> executedLines;

            do
            {
                acc = 0;
                currentLine = 0;
                currentJmpOrNop = 0;
                executedLines = new HashSet<int>();

                do
                {
                    currentCommandCombo = inputList[currentLine].Split(' ');
                    command = currentCommandCombo[0];
                    amount = Int32.Parse(currentCommandCombo[1].Replace("+", ""));
                    executedLines.Add(currentLine);

                    if (currentJmpOrNop == swapJmpOrNopFromStart)
                    {
                        if (command == "jmp") { command = "nop"; }
                        else if (command == "nop") { command = "jmp"; }
                    }

                    if (command.Equals("acc"))
                    {
                        acc += amount;
                        currentLine++;
                    }
                    else if (command.Equals("jmp"))
                    {
                        currentJmpOrNop++;
                        currentLine += amount;
                    }
                    else if (command.Equals("nop"))
                    {
                        currentJmpOrNop++;
                        currentLine++;
                    }
                } while (!executedLines.Contains(currentLine) && currentLine < inputList.Length);

                if (executedLines.Contains(currentLine))
                {
                    swapJmpOrNopFromStart++;
                }

            } while (currentLine < inputList.Length);

            Console.WriteLine("Accumulator after successful swap: " + acc);
        }
    }
}
