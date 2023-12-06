using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics.Metrics;

namespace Advent2023
{
    public class Day06 : Day
    {
        List<Tuple<int, int>> Instructions;
        public Day06(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            List<List<int>> lists = this.ParseListOfIntegerLists(Input);
            Instructions = new List<Tuple<int, int>>();
            for (int i = 0; i < lists[0].Count(); i++)
            {
                Instructions.Add(new Tuple<int, int>(lists[0][i], lists[1][i]));
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 1;
            foreach (Tuple<int, int> instruction in Instructions)
            {
                int counter = 0;
                for (int i = 0; i < instruction.Item1; i++)
                {
                    if (i * (instruction.Item1 - i) > instruction.Item2)
                        counter++;
                }
                ReturnValue *= counter;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            string item1String = "";
            string item2String = "";
            foreach (Tuple<int, int> instruction in Instructions)
            {
                item1String += instruction.Item1.ToString();
                item2String += instruction.Item2.ToString();
            }
            long item1 = Int64.Parse(item1String);
            long item2 = Int64.Parse(item2String);

            for (long i = 0; i < item1; i++)
            {
                if (i * (item1 - i) > item2)
                    ReturnValue++;
            }
            return ReturnValue.ToString();
        }
    }
}
