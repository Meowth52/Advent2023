using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day23 : Day
    {
        Dictionary<Coordinate, char> Obstacles;
        public Day23(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Obstacles = this.ParseAnotherCoordinateMap(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(false).ToString(), GetPartTwo());
        }
        public int GetPartOne(bool InRealityThisIsPart2)
        {
            int ReturnValue = 0;
            int YMax = 0;
            int XMax = 0;
            List<(Coordinate, HashSet<Coordinate>)> Paths = new List<(Coordinate, HashSet<Coordinate>)>();
            Coordinate Start;
            Coordinate End = new Coordinate(0, 0); //den är ju fucking assigned. Låt mig ta mina risker
            char[] Directions = { 'N', 'E', 'S', 'W' };
            foreach (Coordinate coordinate in Obstacles.Keys)
            {
                if (coordinate.x > XMax)
                    XMax = coordinate.x;
                if (coordinate.y > YMax)
                    YMax = coordinate.y;
            }
            for (int x = 0; x < XMax; x++)
            {
                if (!Obstacles.ContainsKey(new Coordinate(x, YMax)))
                {
                    Start = new Coordinate(x, YMax);
                    Coordinate FirstStep = new Coordinate(x, YMax - 1);
                    Paths.Add((FirstStep, new HashSet<Coordinate> { Start, FirstStep }));
                }
                if (!Obstacles.ContainsKey(new Coordinate(x, 0)))
                    End = new Coordinate(x, 0);
            }
            while (Paths.Count > 0)
            {
                List<(Coordinate, HashSet<Coordinate>)> Next = new List<(Coordinate, HashSet<Coordinate>)>();
                foreach ((Coordinate, HashSet<Coordinate>) path in Paths)
                {
                    Coordinate now = new Coordinate(path.Item1);
                    foreach (char direction in Directions)
                    {
                        Coordinate Maybe = new Coordinate(now);
                        Maybe.MoveNSteps(direction);
                        if (!path.Item2.Contains(Maybe) && Maybe != End)
                        {
                            bool jepp = true;
                            if (!InRealityThisIsPart2 && Obstacles.ContainsKey(now))
                            {
                                switch (Obstacles[now])
                                {
                                    case '^':
                                        jepp = direction == 'N';
                                        break;
                                    case '>':
                                        jepp = direction == 'E';
                                        break;
                                    case 'v':
                                        jepp = direction == 'S';
                                        break;
                                    case '<':
                                        jepp = direction == 'W';
                                        break;
                                }
                            }
                            if (Obstacles.ContainsKey(Maybe) && Obstacles[Maybe] == '#')
                            {
                                jepp = false;
                            }
                            if (jepp)
                            {
                                HashSet<Coordinate> next = new HashSet<Coordinate>(path.Item2);
                                next.Add(Maybe);
                                if (next.Count > ReturnValue)
                                    ReturnValue = next.Count;
                                Next.Add((Maybe, next));
                            }

                        }
                    }
                }
                Paths = new List<(Coordinate, HashSet<Coordinate>)>(Next);
            }
            return ReturnValue;
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            Dictionary<Coordinate, char> LessObstacles = new Dictionary<Coordinate, char>();
            foreach (var obstacle in Obstacles)
                if (obstacle.Value == '#')
                    LessObstacles.Add(obstacle.Key,obstacle.Value);
            Obstacles = LessObstacles;
            ReturnValue = GetPartOne(true);
            return ReturnValue.ToString();
        }
    }
}
