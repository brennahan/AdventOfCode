using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day4
    {
        public static void StartProcess()
        {
            string[] inputList = System.IO.File.ReadAllLines("../../../Input/Day4_Input.txt");

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
            int passportSequenceId = 0;
            string[] splitList;
            string[] secondSplit;
            Dictionary<int, int> requiredFieldsCount = new Dictionary<int, int>();
            requiredFieldsCount.Add(passportSequenceId, 0);

            foreach (string input in inputList)
            {
                //Split on space
                splitList = input.Split(' ');

                //Different entry detected
                if (splitList.Length == 1 && splitList[0] == "")
                {
                    passportSequenceId++;
                    requiredFieldsCount.Add(passportSequenceId, 0);
                }

                foreach(string split in splitList)
                {
                    secondSplit = split.Split(':');

                    if (secondSplit[0] == "byr"
                        || secondSplit[0] == "iyr"
                        || secondSplit[0] == "eyr"
                        || secondSplit[0] == "hgt"
                        || secondSplit[0] == "hcl"
                        || secondSplit[0] == "ecl"
                        || secondSplit[0] == "pid")
                    {
                        requiredFieldsCount[passportSequenceId] = requiredFieldsCount[passportSequenceId] + 1;
                    }
                }
            }

            Console.WriteLine("Valid passports: " + requiredFieldsCount.Where(rfc => rfc.Value == 7).Count() + "/" + requiredFieldsCount.Count);
        }

        public static void Part2(string[] inputList)
        {
            int passportSequenceId = 0;
            string[] splitList;
            string[] secondSplit;
            int parsedInt;
            string uom;
            Regex rx1 = new Regex("^#([0-9a-f]{6})$");
            Regex rx2 = new Regex("^([0-9]{9})$");
            Dictionary<int, int> requiredFieldsCount = new Dictionary<int, int>();
            requiredFieldsCount.Add(passportSequenceId, 0);

            foreach (string input in inputList)
            {
                //Split on space
                splitList = input.Split(' ');

                //Different entry detected
                if (splitList.Length == 1 && splitList[0] == "")
                {
                    passportSequenceId++;
                    requiredFieldsCount.Add(passportSequenceId, 0);
                }

                foreach (string split in splitList)
                {
                    secondSplit = split.Split(':');

                    if (secondSplit[0] == "byr")
                    {
                        Int32.TryParse(secondSplit[1], out parsedInt);

                        if (parsedInt >= 1920 && parsedInt <= 2002)
                        {
                            requiredFieldsCount[passportSequenceId] = requiredFieldsCount[passportSequenceId] + 1;
                        }
                    }
                    else if (secondSplit[0] == "iyr")
                    {
                        Int32.TryParse(secondSplit[1], out parsedInt);

                        if (parsedInt >= 2010 && parsedInt <= 2020)
                        {
                            requiredFieldsCount[passportSequenceId] = requiredFieldsCount[passportSequenceId] + 1;
                        }
                    }
                    else if (secondSplit[0] == "eyr")
                    {
                        Int32.TryParse(secondSplit[1], out parsedInt);

                        if (parsedInt >= 2020 && parsedInt <= 2030)
                        {
                            requiredFieldsCount[passportSequenceId] = requiredFieldsCount[passportSequenceId] + 1;
                        }
                    }
                    else if (secondSplit[0] == "hgt")
                    {
                        if (secondSplit[1].Length >= 3)
                        {
                            uom = secondSplit[1].Substring(secondSplit[1].Length - 2, 2);
                            Int32.TryParse(secondSplit[1].Substring(0, secondSplit[1].Length - 2), out parsedInt);

                            if ((uom == "cm" && parsedInt >= 150 && parsedInt <= 193)
                                || (uom == "in" && parsedInt >= 59 && parsedInt <= 76))
                            {
                                requiredFieldsCount[passportSequenceId] = requiredFieldsCount[passportSequenceId] + 1;
                            }
                        }
                    }
                    else if (secondSplit[0] == "hcl")
                    {
                        if (rx1.IsMatch(secondSplit[1]))
                        {
                            requiredFieldsCount[passportSequenceId] = requiredFieldsCount[passportSequenceId] + 1;
                        }
                    }
                    else if (secondSplit[0] == "ecl")
                    {
                        if (secondSplit[1] == "amb"
                            || secondSplit[1] == "blu"
                            || secondSplit[1] == "brn"
                            || secondSplit[1] == "gry"
                            || secondSplit[1] == "grn"
                            || secondSplit[1] == "hzl"
                            || secondSplit[1] == "oth")
                        {
                            requiredFieldsCount[passportSequenceId] = requiredFieldsCount[passportSequenceId] + 1;
                        }
                    }
                    else if (secondSplit[0] == "pid")
                    {
                        if (rx2.IsMatch(secondSplit[1]))
                        {
                            requiredFieldsCount[passportSequenceId] = requiredFieldsCount[passportSequenceId] + 1;
                        }
                    }
                }
            }

            Console.WriteLine("Valid passports: " + requiredFieldsCount.Where(rfc => rfc.Value == 7).Count() + "/" + requiredFieldsCount.Count);
        }
    }
}
