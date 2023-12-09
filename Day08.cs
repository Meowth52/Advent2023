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
            string CurrentPath = "AAA";
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
            long Iteration = 0;
            List<string> CurrentPaths = Paths.Keys.Where(x => x.Last() == 'A').ToList();
            List<string> FirstPaths = new List<string>();
            Dictionary<int, int> FirstStart = new Dictionary<int, int>();
            int InstructionIndex = 0;
            Dictionary<int,List<long>> EndDic = new Dictionary<int,List<long>>();
            for(int i = 0;i<CurrentPaths.Count;i++)
            {
                EndDic.Add(i, new List<long>() { 0 });
            }
            int counter = 0;
            while (counter<6)
            {
                Iteration++;
                for(int i = 0; i < CurrentPaths.Count;i++)
                {
                    CurrentPaths[i] = Paths[CurrentPaths[i]][Instructions[InstructionIndex]];
                    if (CurrentPaths[i].Last() == 'Z' && EndDic[i][0] != 0 && EndDic[i].Count == 1)
                    {
                        EndDic[i].Add(Iteration- EndDic[i][0]);
                        counter++;
                    }
                    if (FirstPaths.Count() > 0 && CurrentPaths[i] == FirstPaths[i] && InstructionIndex == Instructions.Count()-1)
                    {
                        if (EndDic[i][0] == 0)
                        {
                            EndDic[i][0] =Iteration- Instructions.Count();
                        }
                    }

                }
                InstructionIndex++;
                if (InstructionIndex >= Instructions.Count())
                {
                    if (FirstPaths.Count() == 0)
                    {
                        FirstPaths = new List<string>(CurrentPaths);
                    }
                    InstructionIndex = 0;
                }
            }
            long schmiterator = 1;
            while (true)
            {
                bool nope = false;
                //long IsThisIt = schmiterator * EndDic[0][0] + EndDic[0][1];
                foreach(List<long> eh in EndDic.Values)
                {
                    if(!(schmiterator % eh[0] == 0))
                        nope = true;
                }
                if (!nope)
                {
                    return (schmiterator  + EndDic[0][1]).ToString();
                }
                schmiterator += EndDic[0][0];
            }
            return Iteration.ToString();
        }
    }
}
