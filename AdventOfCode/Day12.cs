using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day12
    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day12_Input.txt");

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
            int xPos = 0, yPos = 0;
            
            List <Command> commandList = inputList
                .Select(i => new Command() { direction = i[0], amount = Int32.Parse(i.Substring(1)) })
                .ToList();

            //Set Current direction to East
            Command.currentDegree = 90;

            foreach(var command in commandList)
            {
                //Move, then set current direction
                if (command.direction == 'N')
                {
                    yPos += command.amount;
                }
                else if (command.direction == 'E')
                {
                    xPos += command.amount;
                }
                else if (command.direction == 'S')
                {
                    yPos -= command.amount;
                }
                else if (command.direction == 'W')
                {
                    xPos -= command.amount;
                }
                else if (command.direction == 'L')
                {
                    Command.currentDegree = CustomModulus(Command.currentDegree - command.amount);
                }
                else if (command.direction == 'R')
                {
                    Command.currentDegree = CustomModulus(Command.currentDegree + command.amount);
                }
                else if (command.direction == 'F')
                {
                    if (Command.currentDegree == 0)
                    {
                        yPos += command.amount;
                    }
                    else if (Command.currentDegree == 90)
                    {
                        xPos += command.amount;
                    }
                    else if (Command.currentDegree == 180)
                    {
                        yPos -= command.amount;
                    }
                    else if (Command.currentDegree == 270)
                    {
                        xPos -= command.amount;
                    }
                }
            }
            
            Console.WriteLine("Manhatten distance: " + (Math.Abs(xPos) + Math.Abs(yPos)));
        }

        public static void Part2(string[] inputList)
        {
            int xPos = 0, yPos = 0;
            int wayXPos = 10, wayYPos = 1;
            int temp;

            List<Command> commandList = inputList
                .Select(i => new Command() { direction = i[0], amount = Int32.Parse(i.Substring(1)) })
                .ToList();

            //Set Current direction to East
            Command.currentDegree = 90;

            foreach (var command in commandList)
            {
                //Move, then set current direction
                if (command.direction == 'N')
                {
                    wayYPos += command.amount;
                }
                else if (command.direction == 'E')
                {
                    wayXPos += command.amount;
                }
                else if (command.direction == 'S')
                {
                    wayYPos -= command.amount;
                }
                else if (command.direction == 'W')
                {
                    wayXPos -= command.amount;
                }
                else if (command.direction == 'L' || command.direction == 'R')
                {
                    int clockRotates = 0;
                    if (command.direction == 'L' && command.amount == 90
                        || command.direction == 'R' && command.amount == 270)
                    {
                        clockRotates = 3;
                    }
                    else if (command.direction == 'L' && command.amount == 180
                        || command.direction == 'R' && command.amount == 180)
                    {
                        clockRotates = 2;
                    }
                    else if (command.direction == 'L' && command.amount == 270
                        || command.direction == 'R' && command.amount == 90)
                    {
                        clockRotates = 1;
                    }

                    for(int i = 0; i < clockRotates; i++)
                    {
                        temp = wayXPos;
                        wayXPos = wayYPos;
                        wayYPos = 0 - temp;
                    }
                }
                else if (command.direction == 'F')
                {
                    for (int i = 0; i < command.amount; i++)
                    {
                        xPos += wayXPos;
                        yPos += wayYPos;
                    }
                }
            }

            Console.WriteLine("Manhatten distance: " + (Math.Abs(xPos) + Math.Abs(yPos)));
        }

        public static int CustomModulus(int input)
        {
            return (input % 360 + 360) % 360;
        }
    }

    class Command
    {
        public static int currentDegree { get; set; }
        public char direction { get; set; } // NOTE: Thankfully, locks to cardinal direction.
        public int amount { get; set; }
    }
}
