using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day01 : Day
    {
        string[] Instructions;
        public Day01(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseStringArray(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(Instructions), GetPartTwo());
        }
        public string GetPartOne(string[] instructions)
        {
            int ReturnValue = 0;
            foreach(string s in instructions)
            {
                List<int> ints = new List<int>();
                foreach (char c in s)
                    if (c >= '0' && c <= '9')
                        ints.Add(c-'0');
                ReturnValue += ints.First() * 10 + ints.Last();
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            List<string> Strints = new List<string>
            {
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine"
            };
            List<string> instructionList = new List<string>();
            foreach(string s in Instructions)
            {
                string sch = s;
                for (int i = 1; i <= 9; i++)
                {
                    sch = sch.Replace(Strints[i - 1], Strints[i - 1] + i.ToString() + Strints[i - 1]);
                }
                instructionList.Add(sch);
            }
            return GetPartOne(instructionList.ToArray());
        }
    }
}
