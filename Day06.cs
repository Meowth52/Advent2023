using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
            for(int i = 0; i < lists[0].Count(); i++)
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
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
    }
}
