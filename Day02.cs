using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Advent2023
{
    public class Day02 : Day
    {
        List<Dictionary<string, int>> Games;
        public Day02(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] Instructions = this.ParseStringArray(Input);
            Games = new List<Dictionary<string, int>>();
            foreach (string s in Instructions)
            {
                string[] sets = this.ParseStringArray(s.Split(": ")[1].Trim(), ";");
                Dictionary<string, int> set = new Dictionary<string, int>();
                foreach (string setString in sets)
                {
                    string[] cubes = this.ParseStringArray(setString, ",");
                    foreach (string c in cubes)
                    {
                        int numberOfCubes = this.ParseListOfInteger(c).First();
                        string colourOfCubes = this.ParseStringArray(c, " ")[1];
                        if (!set.ContainsKey(colourOfCubes))
                        {
                            set.Add(colourOfCubes, numberOfCubes);
                        }
                        else
                        {
                            if (set[colourOfCubes] < numberOfCubes)
                                set[colourOfCubes] = numberOfCubes;
                        }
                    }
                }
                Games.Add(set);
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;

            for (int i = 0; i < Games.Count; i++)
            {
                int red = 0;
                int green = 0;
                int blue = 0;
                if (Games[i].ContainsKey("red"))
                    red = Games[i]["red"];
                if (Games[i].ContainsKey("green"))
                    green = Games[i]["green"];
                if (Games[i].ContainsKey("blue"))
                    blue = Games[i]["blue"];

                if (red <= 12 && green <= 13 && blue <= 14)
                    ReturnValue += i + 1;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;

            for (int i = 0; i < Games.Count; i++)
            {
                int red = 0;
                int green = 0;
                int blue = 0;
                if (Games[i].ContainsKey("red"))
                    red = Games[i]["red"];
                if (Games[i].ContainsKey("green"))
                    green = Games[i]["green"];
                if (Games[i].ContainsKey("blue"))
                    blue = Games[i]["blue"];

                ReturnValue += red * green * blue;
            }
            return ReturnValue.ToString();
        }
    }
}
