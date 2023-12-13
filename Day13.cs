using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day13 : Day
    {
        List<List<List<List<bool>>>> Instructions; //patter<dimension<row<char>>>>
        public Day13(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] stringPatterns = this.ParseStringArray(Input, "\r\n\r\n");
            Instructions = new List<List<List<List<bool>>>>();
            foreach(string pattern in stringPatterns)
            {
                List<List<bool>> OneDimension= new List<List<bool>>();
                List<List<bool>> AnotherDimension= new List<List<bool>>();  
                string[] rows = this.ParseStringArray(pattern);
                for (int i = 0; i < rows[0].Length;i++)
                {
                    OneDimension.Add(new List<bool>());
                }
                foreach (string row in rows)
                {
                    List<bool> boolRow = new List<bool>();
                    for(int i = 0;i<row.Length;i++)
                    {
                        boolRow.Add(row[i]=='#');
                        OneDimension[i].Add(row[i] == '#');
                    }
                    AnotherDimension.Add(boolRow);
                }
                List<List<List<bool>>> bothDimension = new List<List<List<bool>>>();
                bothDimension.Add(OneDimension);
                bothDimension.Add(AnotherDimension);
                Instructions.Add(bothDimension);
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            foreach (var pattern in Instructions)
            {
                bool otherDimension = false;
                foreach(var dimension in pattern)
                {
                    for(int i = 0;i<dimension.Count-1;i++)
                    {
                        if (this.CompareLists(dimension[i],dimension[i+1]))
                        {
                            int left = i;
                            int right = i+1;
                            bool hit = true;
                            while (left > 0 && right < dimension.Count-1)
                            {
                                left--;
                                right++;
                                if (!this.CompareLists( dimension[left],dimension[right]))
                                {
                                    hit = false;
                                    break;
                                }
                            }
                            if(hit)
                            {
                                int score = i+1;
                                if (otherDimension)
                                    score *= 100;
                                ReturnValue += score;
                                break;
                            }
                        }
                    }
                    otherDimension= true;
                }
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
        public bool CompareLists(List<bool> one , List<bool> two)
        {
            bool same = true;
            for(int i = 0; i < one.Count;i++)
                if (one[i] != two[i])
                {
                    same = false;
                    break;
                }
            return same;
        }
    }
}
