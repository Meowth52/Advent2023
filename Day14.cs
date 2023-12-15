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
        HashSet<Coordinate> Stones;
        HashSet<Coordinate> StillStones;
        int YMax;
        public Day14(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Stones = new HashSet<Coordinate>();
            StillStones = new HashSet<Coordinate>();
            YMax = strings.Length - 1;
            for (int y = 0; y < strings.Length; y++)
            {
                for (int x = 0; x < strings[0].Length; x++)
                {
                    if (strings[y][x] != '.')
                    {
                        if (strings[y][x] == 'O')
                            Stones.Add(new Coordinate(x, y));
                        else
                            StillStones.Add(new Coordinate(x, y));
                    }
                }
            }
            for (int x = 0; x < strings[0].Length; x++)
            {
                StillStones.Add(new Coordinate(x, -1));
                StillStones.Add(new Coordinate(x, YMax + 1));
            }
            for (int y = 0; y < strings.Length; y++)
            {
                StillStones.Add(new Coordinate(-1, y));
                StillStones.Add(new Coordinate(strings[0].Length, y));
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
            List<int> ICanHasPattern = new List<int>();
            List<char> Directions = new List<char>
            {
                'S','W','N','E',
            };
            for (int i = 1; i <= 500; i++)
            {
                for (int direction = 0; direction < 4; direction++)
                {
                    switch (direction)
                    {
                        case 0:
                            Stones = Stones.OrderBy(x => x.y).ToHashSet();
                            RollTheStones(Directions[direction]);
                            break;
                        case 1:
                            Stones = Stones.OrderBy(x => x.x).ToHashSet();
                            RollTheStones(Directions[direction]);
                            break;
                        case 2:
                            Stones = Stones.OrderByDescending(x => x.y).ToHashSet();
                            RollTheStones(Directions[direction]);
                            break;
                        case 3:
                            Stones = Stones.OrderByDescending(x => x.x).ToHashSet();
                            RollTheStones(Directions[direction]);
                            break;
                    }
                }
                int TheNextOne = 0;
                foreach (Coordinate stone in Stones)
                {
                    TheNextOne += (YMax + 1) - stone.y;
                }
                ICanHasPattern.Add(TheNextOne);
            }
            List<int> CommonPatterPlease = new List<int>();
            int IteratorOne = 300;
            CommonPatterPlease.Add(ICanHasPattern[IteratorOne]);
            CommonPatterPlease.Add(ICanHasPattern[IteratorOne + 1]);
            CommonPatterPlease.Add(ICanHasPattern[IteratorOne + 2]);
            CommonPatterPlease.Add(ICanHasPattern[IteratorOne + 3]);
            CommonPatterPlease.Add(ICanHasPattern[IteratorOne + 4]);
            int Start = 300;
            bool yes = false;
            while (!yes)
            {
                IteratorOne++;
                yes = true;
                for (int ii = 0; ii < CommonPatterPlease.Count; ii++)
                {
                    if (CommonPatterPlease[ii] != ICanHasPattern[IteratorOne + ii])
                    {
                        yes = false; break;
                    }
                }
            }
            int ReturnValue = ICanHasPattern[Start + ((1000000000 - Start) % (IteratorOne - Start)) - 1];
            return ReturnValue.ToString();
        }
        public void RollTheStones(char direction)
        {
            List<Coordinate> slowStones = new List<Coordinate>(Stones);
            foreach (Coordinate stone in slowStones)
            {
                Coordinate GhostStone = new Coordinate(stone);
                GhostStone.MoveNSteps(direction);
                while (!slowStones.Contains(GhostStone) && !StillStones.Contains(GhostStone))
                {
                    stone.MoveNSteps(direction);
                    GhostStone.MoveNSteps(direction);
                }
            }
            Stones = slowStones.ToHashSet();
        }
    }
}
