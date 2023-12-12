using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day09 : Day
    {
        List<List<int>> Instructions;
        public Day09(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseListOfIntegerLists(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            foreach (List<int> Instruction in Instructions)
            {
                List<int> Next = GetAnotheList(Instruction);
                List<List<int>> MooooreLists = new List<List<int>>();
                MooooreLists.Add(Instruction);
                MooooreLists.Add(Next);
                bool Done = false;
                while (!Done)
                {
                    Done = true;
                    Next = GetAnotheList(Next);
                    MooooreLists.Add(Next);
                    foreach (int x in Next)
                    {
                        if (x != 0)
                        {
                            Done = false;
                            break;
                        }
                    }
                }
                int NextInt = 0;
                for (int i = MooooreLists.Count - 1; i >= 0; i--)
                {
                    NextInt += MooooreLists[i][MooooreLists[i].Count - 1];
                    ;
                }
                ReturnValue += NextInt;
            }

            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            foreach (List<int> Instruction in Instructions)
            {
                List<int> Next = GetAnotheList(Instruction);
                List<List<int>> MooooreLists = new List<List<int>>();
                MooooreLists.Add(Instruction);
                MooooreLists.Add(Next);
                bool Done = false;
                while (!Done)
                {
                    Done = true;
                    Next = GetAnotheList(Next);
                    MooooreLists.Add(Next);
                    foreach (int x in Next)
                    {
                        if (x != 0)
                        {
                            Done = false;
                            break;
                        }
                    }
                }
                int NextInt = 0;
                for (int i = MooooreLists.Count - 1; i >= 0; i--)
                {
                    NextInt = MooooreLists[i][0] - NextInt;
                    ;
                }
                ReturnValue += NextInt;
            }

            return ReturnValue.ToString();
        }
        List<int> GetAnotheList(List<int> list)
        {
            List<int> ReturnList = new List<int>();
            for (int i = 0; i < list.Count - 1; i++)
            {
                ReturnList.Add(list[i + 1] - list[i]);
            }
            return ReturnList;
        }
    }
}
