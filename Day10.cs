using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace Advent2023
{
    public class Day10 : Day
    {
        Dictionary<Coordinate, char> Pipes;
        HashSet<Coordinate> ThePipe;
        Coordinate Start;
        HashSet<Coordinate> Fill;
        int MaxY;
        int MaxX;
        int StartDirection;
        public Day10(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Pipes = new Dictionary<Coordinate, char>();
            for (int y = 0; y < strings.Length; y++)
            {
                for (int x = 0; x < strings[0].Length; x++)
                {
                    if (strings[y][x] != '.')
                    {
                        if (strings[y][x] == 'S')
                            Start = new Coordinate(x, strings.Length - (y + 1));
                        else
                            Pipes.Add(new Coordinate(x, strings.Length - (y + 1)), strings[y][x]);
                    }
                }
            }
            MaxY = strings.Length - 1;
            MaxX = strings[0].Length - 1;
            Fill = new HashSet<Coordinate>();
            StartDirection = 2;
            ThePipe = new HashSet<Coordinate>();
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 1;
            Coordinate Here = new Coordinate(Start);
            Dictionary<int, char> Directions = new Dictionary<int, char>
            {
                {0,'N'},
                {1,'E'},
                {2,'S'},
                {3,'W'}
            };
            int Direction = StartDirection;
            Here.MoveNSteps(Directions[Direction]);
            while (true)
            {
                int turn = 0;
                Coordinate Right = new Coordinate(Here);
                ThePipe.Add(new Coordinate(Here));
                switch (Pipes[Here])

                {
                    case 'F':
                        if (Direction == 0)
                            turn = 1;
                        else if (Direction == 3)
                            turn = -1;
                        break;
                    case '7':
                        if (Direction == 1)
                            turn = 1;
                        else if (Direction == 0)
                            turn = -1;
                        break;
                    case 'J':
                        if (Direction == 2)
                            turn = 1;
                        else if (Direction == 1)
                            turn = -1;
                        break;
                    case 'L':
                        if (Direction == 3)
                            turn = 1;
                        else if (Direction == 2)
                            turn = -1;
                        break;
                    default:
                        break;
                }
                Direction = Coordinate.Turn(Direction, turn);
                Here.MoveNSteps(Directions[Direction]);
                if (Pipes.ContainsKey(Here))
                    ReturnValue++;
                else
                    break;
            }
            ReturnValue = (ReturnValue + 1) / 2;
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;




            Coordinate Here = new Coordinate(Start);
            Dictionary<int, char> Directions = new Dictionary<int, char>
            {
                {0,'N'},
                {1,'E'},
                {2,'S'},
                {3,'W'}
            };
            int Direction = StartDirection;
            Here.MoveNSteps(Directions[Direction]);
            while (true)
            {
                int turn = 0;
                Coordinate Right = new Coordinate(Here);
                Right.MoveNSteps(Directions[Coordinate.Turn(Direction, 1)]);
                if ((Right.x != Start.x || Right.y != Start.y) && !ThePipe.Contains(Right))
                    Fill.Add(new Coordinate(Right));
                if (Right.x == Start.x && Right.y == Start.y)
                {
                    ;
                    if (Right == Start)
                    {
                        ;//Fuuuu
                    }
                }
                switch (Pipes[Here])

                {
                    case 'F':
                        if (Direction == 0)
                            turn = 1;
                        else if (Direction == 3)
                            turn = -1;
                        break;
                    case '7':
                        if (Direction == 1)
                            turn = 1;
                        else if (Direction == 0)
                            turn = -1;
                        break;
                    case 'J':
                        if (Direction == 2)
                            turn = 1;
                        else if (Direction == 1)
                            turn = -1;
                        break;
                    case 'L':
                        if (Direction == 3)
                            turn = 1;
                        else if (Direction == 2)
                            turn = -1;
                        break;
                    default:
                        break;
                }
                Direction = Coordinate.Turn(Direction, turn);
                Right = new Coordinate(Here);
                Right.MoveNSteps(Directions[Coordinate.Turn(Direction, 1)]);
                if ((Right.x != Start.x || Right.y != Start.y) && !ThePipe.Contains(Right))
                    Fill.Add(new Coordinate(Right));
                Here.MoveNSteps(Directions[Direction]);
                if (Pipes.ContainsKey(Here))
                    ;
                else
                    break;
            }






            char[] Directionss =
            {
                'N','E','S','W'
            };
            HashSet<Coordinate> LookingAt = new HashSet<Coordinate>(Fill);
            while (LookingAt.Count > 0)
            {
                HashSet<Coordinate> Next = new HashSet<Coordinate>();
                foreach (Coordinate filler in LookingAt)
                {
                    foreach (char direction in Directionss)
                    {
                        Coordinate maybe = new Coordinate(filler);
                        maybe.MoveNSteps(direction);
                        if (!ThePipe.Contains(maybe) && !Fill.Contains(maybe) && maybe != Start)
                        {
                            Fill.Add(maybe);
                            Next.Add(maybe);
                        }
                    }
                }
                LookingAt = new HashSet<Coordinate>(Next);

            }
            ReturnValue = Fill.Count;
            return ReturnValue.ToString();
        }
    }
}
