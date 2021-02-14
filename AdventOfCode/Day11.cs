using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day11
    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day11_Input.txt");

            //Timer Start
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //Process(inputList, false);
            Process(inputList, true);

            //Timer Stop
            sw.Stop();

            Console.WriteLine("Duration: " + sw.Elapsed);

            Console.ReadLine();
        }

        public static void Process(string[] inputList, bool isStep2)
        {
            bool firstRun = true;
            int timesLooped = 0;
            char[,] grid = Create2DArray(inputList);
            char[,] gridNew = new char[grid.GetLength(0), grid.GetLength(1)];

            do
            {
                if (!firstRun)
                {
                    grid = (char[,])gridNew.Clone();
                    timesLooped++;
                }
                else
                {
                    firstRun = false;
                }
                
                gridNew = new char[grid.GetLength(0), grid.GetLength(1)];

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] == 'L')
                        {
                            gridNew[i, j] = Rule1Applied(grid, i, j, isStep2);
                        }
                        else if (grid[i, j] == '#')
                        {
                            gridNew[i, j] = Rule2Applied(grid, i, j, isStep2);
                        }
                        else
                        {
                            gridNew[i, j] = '.';
                        }
                    }
                }
            } while (!Compare2DArrays(grid, gridNew));
            
            Console.WriteLine("Times Looped: " + timesLooped);
            Console.WriteLine("Occupied Seats: " + CountOccupied(grid));
        }

        public static char Rule1Applied(char[,] grid, int rowIndex, int colIndex, bool isStep2)
        {
            int multMod;
            int rowCheck;
            int colCheck;

            for (int i = -1; i < 2; i++)
            {
                if (rowIndex + i < 0 || rowIndex + i >= grid.GetLength(0))
                {
                    continue;
                }
            
                for (int j = -1; j < 2; j++)
                {
                    if (colIndex + j < 0
                        || colIndex + j >= grid.GetLength(1)
                        || (i == 0 && j == 0))
                    {
                        continue;
                    }

                    multMod = 1;
                    rowCheck = rowIndex + i * multMod;
                    colCheck = colIndex + j * multMod;

                    do
                    {
                        if (grid[rowCheck, colCheck] == '#')
                        {
                            return 'L';
                        }
                        else if (isStep2 && grid[rowCheck, colCheck] == 'L')
                        {
                            break;
                        }

                        if (!isStep2)
                        {
                            break;
                        }
                        else
                        {
                            multMod++;
                            rowCheck = rowIndex + i * multMod;
                            colCheck = colIndex + j * multMod;
                        }
                    } while (rowCheck >= 0 
                        && rowCheck < grid.GetLength(0)
                        && colCheck >= 0 
                        && colCheck < grid.GetLength(1));
                }
            }

            return '#';
        }

        public static char Rule2Applied(char[,] grid, int rowIndex, int colIndex, bool isStep2)
        {
            int occupiedCount = 0;
            int multMod;
            int rowCheck;
            int colCheck;

            for (int i = -1; i < 2; i++)
            {
                if (rowIndex + i < 0 || rowIndex + i >= grid.GetLength(0))
                {
                    continue;
                }

                for (int j = -1; j < 2; j++)
                {
                    if (colIndex + j < 0
                        || colIndex + j >= grid.GetLength(1)
                        || (i == 0 && j == 0))
                    {
                        continue;
                    }

                    multMod = 1;
                    rowCheck = rowIndex + i * multMod;
                    colCheck = colIndex + j * multMod;

                    do
                    {
                        if (grid[rowCheck, colCheck] == '#')
                        {
                            occupiedCount++;
                            
                            if (isStep2)
                            {
                                break;
                            }
                        }
                        else if (isStep2 && grid[rowCheck, colCheck] == 'L')
                        {
                            break;
                        }

                        if (!isStep2)
                        {
                            break;
                        }
                        else
                        {
                            multMod++;
                            rowCheck = rowIndex + i * multMod;
                            colCheck = colIndex + j * multMod;
                        }
                    } while (rowCheck >= 0
                        && rowCheck < grid.GetLength(0)
                        && colCheck >= 0
                        && colCheck < grid.GetLength(1));
                }
            }

            if ((!isStep2 && occupiedCount >= 4)
                || (isStep2 && occupiedCount >= 5))
            {
                return 'L';
            }
            else
            {
                return '#';
            }
            
        }
        
        public static char[,] Create2DArray(string[] input)
        {
            char[,] grid = new char[input.Length,input[0].Length];

            for(int i=0; i < input.Length; i++)
            {
                for(int j=0; j < input[i].Length; j++)
                {
                    grid[i,j] = input[i][j];
                }
            }

            return grid;
        }

        public static bool Compare2DArrays(char[,] input1, char[,] input2)
        {

            for (int i = 0; i < input1.GetLength(0); i++)
            {
                for (int j = 0; j < input1.GetLength(1); j++)
                {
                    if (input1[i,j] != input2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static int CountOccupied(char[,] input)
        {
            int occupied = 0;

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == '#')
                    {
                        occupied++;
                    }
                }
            }

            return occupied;
        }
    }
}
