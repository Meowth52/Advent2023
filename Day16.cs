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
        int MaxX;
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
                        Obstacles.Add(new Coordinate(x, strings.Length - (y + 1)), strings[y][x]);
                    }
                }
            }
            MaxY = strings.Length - 1;
            MaxX = strings[0].Length - 1;
        }
        public override Tuple<string, string> GetResult()
        {
            Beam Starter = new Beam(new Coordinate(-1, MaxY), 1);
            return Tuple.Create(GetPartOne(Starter).ToString(), GetPartTwo());
        }
        public int GetPartOne(Beam Starter)
        {
            int ReturnValue = 0;
            Dictionary<Coordinate, HashSet<int>> Visited = new Dictionary<Coordinate, HashSet<int>>();
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
                                switch (beam.Direction)
                                {
                                    case 1:
                                    case 3:
                                        ;
                                        break;
                                    case 0:
                                    case 2:
                                        Beam newOne = new Beam(beam);
                                        newOne.Turn(-1);
                                        if (newOne.Position.IsInPositiveBounds(MaxX, MaxY))
                                            Next.Add(newOne);
                                        beam.Turn(1);
                                        break;
                                };
                                break;
                            case '|':
                                switch (beam.Direction)
                                {
                                    case 0:
                                    case 2:
                                        ;
                                        break;
                                    case 1:
                                    case 3:
                                        Beam newOne = new Beam(beam);
                                        newOne.Turn(-1);
                                        if (newOne.Position.IsInPositiveBounds(MaxX, MaxY))
                                            Next.Add(newOne);
                                        beam.Turn(1);
                                        break;
                                };
                                break;
                            case '\\':
                                switch (beam.Direction)
                                {
                                    case 0:
                                    case 2:
                                        beam.Turn(-1);
                                        break;
                                    case 1:
                                    case 3:
                                        beam.Turn(1);
                                        break;
                                };
                                break;
                            case '/':
                                switch (beam.Direction)
                                {
                                    case 0:
                                    case 2:
                                        beam.Turn(1);
                                        break;
                                    case 1:
                                    case 3:
                                        beam.Turn(-1);
                                        break;
                                };
                                break;
                        }
                    }
                    if (beam.Position.IsInPositiveBounds(MaxX, MaxY) && !(Visited.ContainsKey(beam.Position) && Visited[beam.Position].Contains(beam.Direction)))
                        Next.Add(new Beam(beam));
                }
                foreach (Beam b in Next)
                {
                    if (!Visited.ContainsKey(b.Position))
                        Visited.Add(new Coordinate(b.Position), new HashSet<int>());
                    Visited[b.Position].Add(b.Direction);
                }
                BeamList = new List<Beam>(Next);
            }
            ReturnValue = Visited.Count;
            return ReturnValue;
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            for (int y = 0; y <= MaxY; y++)
            {
                Beam starter = new Beam(new Coordinate(-1, y), 1);
                int score = GetPartOne(starter);
                if (score > ReturnValue)
                    ReturnValue = score;
                starter = new Beam(new Coordinate(MaxX + 1, y), 3);
                score = GetPartOne(starter);
                if (score > ReturnValue)
                    ReturnValue = score;
            }
            for (int x = 0; x <= MaxX; x++)
            {
                Beam starter = new Beam(new Coordinate(x, -1), 0);
                int score = GetPartOne(starter);
                if (score > ReturnValue)
                    ReturnValue = score;
                starter = new Beam(new Coordinate(x, MaxY + 1), 2);
                score = GetPartOne(starter);
                if (score > ReturnValue)
                    ReturnValue = score;
            }
            Beam Starter = new Beam(new Coordinate(-1, MaxY), 1);
            return ReturnValue.ToString();
        }
    }
    public class Beam
    {
        public Coordinate Position;
        public int Direction;
        Dictionary<int, char> Directions = new Dictionary<int, char>
            {
                {0,'N'},
                {1,'E'},
                {2,'S'},
                {3,'W'}
            };
        public Beam(Coordinate position, int direction)
        {
            Position = position;
            Direction = direction;
        }
        public Beam(Beam beam)
        {
            Position = new Coordinate(beam.Position);
            Direction = beam.Direction;
        }

        public void Move()
        {
            Position.MoveNSteps(Directions[Direction]);
            ;
        }
        public void Turn(int where)
        {
            Direction = (Direction + where + 4) % 4;
            ;
        }

    }
}
