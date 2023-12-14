using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day14 : Day
    {
        List<Coordinate> Stones;
        List<Coordinate> StillStones;
        int YMax;
        public Day14(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Stones = new List<Coordinate>();
            StillStones = new List<Coordinate>();
            YMax = strings.Length - 1;
            for (int y = 0; y < strings.Length; y++)
            {
                for (int x = 0; x < strings[0].Length; x++)
                {
                    if (strings[y][x] != '.')
                    {
                        if(strings[y][x] == 'O')
                            Stones.Add(new Coordinate(x, y));
                        else
                        StillStones.Add(new Coordinate(x, y));
                    }
                }
            }
            for (int x = 0; x < strings[0].Length; x++)
            {
                Stones.Add(new Coordinate(x, -1));
                Stones.Add(new Coordinate(x, YMax+1));
            }
            for (int y = 0; y < strings.Length; y++)
            {
                Stones.Add(new Coordinate(-1,y ));
                Stones.Add(new Coordinate(strings[0].Length, y));
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            RollTheStones('S');
            foreach (Coordinate stone in Stones)
            {
                    ReturnValue += (YMax + 1) - stone.y;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            List<char> Directions = new List<char>
            {
                'S','E','N','W'
            };
            for (int i = 1; i <= 1000000000; i++)
            {
                int direction = i % 4;
                RollTheStones(Directions[direction]);
                switch (direction)
                {
                    case 0:
                        Stones = Stones.OrderBy(x => x.x).ToList();
                        break;
                    case 1:
                        Stones = Stones.OrderByDescending(x => x.y).ToList();
                        break;
                    case 2:
                        Stones = Stones.OrderBy(x => x.x).ToList();
                        break;
                    case 3:
                        Stones = Stones.OrderByDescending(x => x.y).ToList();
                        break;
                }
                foreach (Coordinate stone in Stones)
                {
                    ReturnValue += (YMax + 1) - stone.y;
                }
                return ReturnValue.ToString();
            }
            return ReturnValue.ToString();
        }
        public void RollTheStones(char direction)
        {
            foreach (Coordinate stone in Stones)
            {
                Coordinate GhostStone = new Coordinate(stone);
                GhostStone.MoveNSteps(direction);
                while (!Stones.Contains(GhostStone) && !StillStones.Contains(GhostStone))
                    {
                        stone.MoveNSteps(direction);
                    }
            }
        }
    }
}
