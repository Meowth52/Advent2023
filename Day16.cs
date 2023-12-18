using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day16 : Day
    {
        Dictionary<Coordinate, char> Obstacles;
        int MaxY;
        public Day16(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Obstacles = new Dictionary<Coordinate, char>();
            for (int y = 0; y < strings.Length; y++)
            {
                for (int x = 0; x < strings[0].Length; x++)
                {
                    if (strings[y][x] != '.')
                    {
                        Obstacles.Add(new Coordinate(x, strings.Length - y), strings[y][x]);
                    }
                }
            }
            MaxY = strings.Length - 1;
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            HashSet<Coordinate> Visited = new HashSet<Coordinate>();
            Beam Starter = new Beam(new Coordinate(MaxY, -1), 'E');
            List<Beam> BeamList = new List<Beam>();
            BeamList.Add(Starter);
            while (BeamList.Count > 0)
            {
                List<Beam> Next = new List<Beam>();
                foreach (Beam beam in BeamList)
                {
                    beam.Move();
                    if (Obstacles.ContainsKey(beam.Position))
                    {
                        switch (Obstacles[beam.Position])
                        {
                            case '-':
                                ;
                                break;
                            case '|':
                                ;
                                break;
                            case '\\':
                                ;
                                break;
                            case '/':
                                ;
                                break;
                        }

                    }
                }
            }

            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            return ReturnValue.ToString();
        }
    }
    public class Beam
    {
        public Coordinate Position;
        public char Direction;
        public Beam(Coordinate position, char direction)
        {

        }
        public void Move()
        {
            Position.MoveNSteps(Direction);
        }

    }
}
