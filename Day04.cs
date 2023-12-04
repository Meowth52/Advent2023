using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day04 : Day
    {
        Dictionary<int, List<HashSet<int>>> Numbers;

        public Day04(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Numbers = new Dictionary<int, List<HashSet<int>>>();
            foreach (string s in strings)
            {
                List<HashSet<int>> Listylist = new List<HashSet<int>>();
                string[] scptruings = s.Split(':');
                int index = this.ParseListOfInteger(scptruings[0]).First();
                string[] schtrungs = scptruings[1].Split('|');
                Listylist.Add(this.ParseListOfInteger(schtrungs[0]).ToHashSet());
                Listylist.Add(this.ParseListOfInteger(schtrungs[1]).ToHashSet());
                Listylist.Add(new HashSet<int> { 1 });
                Numbers.Add(index, Listylist);
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            foreach (KeyValuePair<int, List<HashSet<int>>> row in Numbers)
            {
                int rowValue = 1;
                foreach (int i in row.Value[1])
                {
                    if (row.Value[0].Contains(i))
                    {
                        rowValue *= 2;
                    }
                }
                ReturnValue += rowValue / 2; //din mamma kan vara en fulfix
            }

            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            for (int i = 1; i <= Numbers.Count; i++)
            {
                var row = Numbers[i];
                int matches = 0;
                foreach (int ii in row[1])
                {
                    if (row[0].Contains(ii))
                    {
                        matches++;
                    }
                }
                for (int ii = 1; ii <= matches; ii++)
                {
                    Numbers[i + ii][2] = new HashSet<int> { (Numbers[i + ii][2].First() + row[2].First()) };
                }
                ReturnValue += row[2].First();
            }
            return ReturnValue.ToString();
        }
    }
}
