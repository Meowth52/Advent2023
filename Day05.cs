using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day05 : Day
    {
        List<List<List<long>>> Instructions;
        public Day05(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input,"\r\n\r\n");
            Instructions = new List<List<List<long>>>();
            foreach (string s in strings)
            {
                Instructions.Add(this.ParseListOfLongerLists(s));
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            long ReturnValue = 0;
            List<long> locations = new List<long>();
            foreach(long seed in Instructions[0][0])
            {
                locations.Add(GetLocationTheHardWay(seed));
            }
            locations.Sort();
            ReturnValue = locations[0];
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            //List<long> locations = new List<long>();
            //for (int i = 0; i < Instructions[0][0].Count;i+=2) 
            //{
            //    long one = Instructions[0][0][i];
            //    long two = Instructions[0][0][i+1];
            //    for (long schmii = one;schmii < one+two;schmii++)
            //    {
            //        locations.Add(GetLocationTheHardWay(schmii));
            //    }
            //}
            //locations.Sort();
            //ReturnValue = locations[0];
            long counter = 46;
            //HashSet<long> fasterLookUp = Instructions[0][0].ToHashSet();
            while (ReturnValue == 0)
            {
                long isThisIt = GetSeedTheHardWay(counter);
                for (int i = 0; i < Instructions[0][0].Count; i += 2)
                {
                    long one = Instructions[0][0][i];
                    long two = Instructions[0][0][i + 1];
                    if (isThisIt >= one && isThisIt <= one + two)
                    {
                        ReturnValue = counter;
                        break;
                    }
                }
                counter++;
            }
            return ReturnValue.ToString();
        }
        public long GetLocationTheHardWay(long seed)
        {
            long current = seed;
            for (int i = 1; i < Instructions.Count(); i++)
            {
                foreach (List<long> instruction in Instructions[i])
                {
                    if (current >= instruction[1] && current < instruction[1] + instruction[2])
                    {
                        current += instruction[0] - instruction[1];
                        break;
                    }
                }
            }
            return current;
        }
        public long GetSeedTheHardWay(long location)
        {
            long current = location;
            for (int i = Instructions.Count()-1; i > 0; i--)
            {
                foreach (List<long> instruction in Instructions[i])
                {
                    if (current >= instruction[0] && current < instruction[0] + instruction[2])
                    {
                        current -= instruction[0] - instruction[1];
                        break;
                    }
                    ;
                }
            }
            return current;
        }
    }
}
