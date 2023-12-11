using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day11 : Day
    {
        List<Coordinate> Spaaace;
        List<Cooooordinate> Spaaaaaaaace;
        public Day11(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Dictionary<long, bool> HasGalaxy = new Dictionary<long, bool>();
            for (long x = 0; x < strings[0].Count(); x++)
                HasGalaxy.Add(x, false);

            for (int y = 0; y < strings.Length; y++)
            {
                for (int x = 0; x < strings[y].Count(); x++)
                {
                    if (strings[y][x] != '.')
                        HasGalaxy[x] = true;
                }
            }
            Spaaace = new List<Coordinate>();
            Spaaaaaaaace = new List<Cooooordinate>();
            int yy = 0;
            long yy2 = 0;
            long part2 = 999999;
            for (int y = 0; y < strings.Length; y++)
            {
                int xx = 0;
                long xx2 = 0;
                for (int x = 0; x < strings[y].Count(); x++)
                {
                    if (!HasGalaxy[x])
                    {
                        xx++;
                        xx2 += part2;
                    }
                    else if (strings[y][x] == '#')
                    {
                        Spaaace.Add(new Coordinate(xx, yy));
                        Spaaaaaaaace.Add(new Cooooordinate(xx2, yy2));
                    }
                    xx++;
                    xx2++;
                }
                if (!strings[y].Contains('#'))
                {
                    yy++;
                    yy2 += part2;
                }
                yy++;
                yy2++;
            }
            ;
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            Dictionary<Coordinate, Dictionary<Coordinate, int>> Distances = new Dictionary<Coordinate, Dictionary<Coordinate, int>>();
            foreach (Coordinate c in Spaaace)
            {
                if (!Distances.ContainsKey(c))
                {
                    Distances.Add(c, new Dictionary<Coordinate, int>());
                }
                foreach (Coordinate cc in Spaaace)
                {
                    if (c != cc)
                    {
                        if (!Distances[c].ContainsKey(cc)) ;
                        {
                            int distance = c.ManhattanDistance(cc);
                            Distances[c].Add(cc, distance);
                            ReturnValue += distance;
                        }
                    }
                }
            }
            ReturnValue /= 2;
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            Dictionary<Cooooordinate, Dictionary<Cooooordinate, long>> Distances = new Dictionary<Cooooordinate, Dictionary<Cooooordinate, long>>();
            foreach (Cooooordinate c in Spaaaaaaaace)
            {
                if (!Distances.ContainsKey(c))
                {
                    Distances.Add(c, new Dictionary<Cooooordinate, long>());
                }
                foreach (Cooooordinate cc in Spaaaaaaaace)
                {
                    if (c != cc)
                    {
                        if (!Distances[c].ContainsKey(cc))
                        {
                            long distance = c.ManhattanDistance(cc);
                            Distances[c].Add(cc, distance);
                            ReturnValue += distance;
                        }
                    }
                }
            }
            ReturnValue /= 2;
            return ReturnValue.ToString();
        }
    }
}
