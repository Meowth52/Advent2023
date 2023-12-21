using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace Advent2023
{
    public class Day21 : Day
    {
        HashSet<Coordinate> Plots;
        int MaxX;
        int MaxY;
        Coordinate Start;
        public Day21(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Plots = new HashSet<Coordinate>();
            for (int y = 0; y < strings.Length; y++)
            {
                for (int x = 0; x < strings[0].Length; x++)
                {
                    if (strings[y][x] != '#')
                    {
                        Plots.Add(new Coordinate(x, strings.Length - (y + 1)));
                        if (strings[y][x] == 'S')
                            Start = new Coordinate(x, strings.Length - (y + 1));
                    }
                }
            }
            MaxY = strings.Length - 1;
            MaxX = strings[0].Length - 1;
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            int NrOfSteps = 64;
            char[] Directions =
            {
                'N','E','S','W'
            };
            HashSet<Coordinate> Possibilities = new HashSet<Coordinate>()
            {
                Start
            };
            for (int i = 0; i < NrOfSteps; i++)
            {
                HashSet<Coordinate> Next = new HashSet<Coordinate>();
                foreach (Coordinate possibility in Possibilities)
                {
                    foreach (char direction in Directions)
                    {
                        Coordinate maybe = new Coordinate(possibility);
                        maybe.MoveNSteps(direction);
                        if (Plots.Contains(maybe))
                        {
                            Next.Add(maybe);
                        }
                    }
                }
                Possibilities = new HashSet<Coordinate>(Next);
            }
            ReturnValue = Possibilities.Count;
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            //ffs
            return ReturnValue.ToString();
        }
    }
}
