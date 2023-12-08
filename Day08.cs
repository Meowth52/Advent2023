using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;

namespace Advent2023
{
    public class Day08 : Day
    {
        List<char> Instructions;
        Dictionary<string,Dictionary<char,string>> Paths;
        public Day08(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] rows = this.ParseStringArray(Input);
            Instructions = new List<char>();
            Paths = new Dictionary<string, Dictionary<char, string>>();
            foreach(char c in rows[0])
                Instructions.Add(c);
            for(int i = 1; i < rows.Length; i++)
            {
                string row = rows[i].Replace(" = ",", ").Replace(")","").Replace("(","");
                string[] path = this.ParseStringArray(row,", ");
                Paths.Add(path[0], new Dictionary<char, string>
                {
                    {'L', path[1] },
                    {'R', path[2] }
                });
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            long ReturnValue = 0;
            string CurrentPath = Paths.Keys.First();
            int InstructionIndex = 0;
            while (CurrentPath!="ZZZ")
            {
                ReturnValue++;
                CurrentPath = Paths[CurrentPath][Instructions[InstructionIndex]];
                InstructionIndex++;
                if (InstructionIndex >= Instructions.Count())
                    InstructionIndex = 0;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
    }
}
