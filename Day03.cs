using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day03 : Day
    {
        string[] Instructions;
        public Day03(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseStringArray(Input);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            Dictionary<Coordinate, (int, Coordinate)> numbers = new Dictionary<Coordinate, (int, Coordinate)>(); //Top left, number, bottom right
            Dictionary<Coordinate, char> Thingimajings = new Dictionary<Coordinate, char>();
            for (int y = 0; y < Instructions.Length; y++)
            {
                string instruction = Instructions[y];
                int currentDigit = 0;
                int currentDigitPosition = 1;
                Coordinate BottomRight = new Coordinate(0, 0);
                for (int x = instruction.Length - 1; x >= 0; x--)
                {
                    if (Char.IsDigit(instruction[x]))
                    {
                        if (currentDigitPosition == 1)
                            BottomRight = new Coordinate(x + 1, y - 1);
                        char c = instruction[x];
                        currentDigit += (c - '0') * currentDigitPosition;
                        currentDigitPosition *= 10;
                    }
                    else
                    {
                        if (currentDigitPosition != 1)
                        {
                            numbers.Add(new Coordinate(x, y + 1), (currentDigit, BottomRight));
                        }
                        currentDigit = 0;
                        currentDigitPosition = 1;
                        if (instruction[x] == '.') { }
                        else
                        {
                            Thingimajings.Add(new Coordinate(x, y), instruction[x]);
                        }

                    }
                }
                if (currentDigitPosition != 1)
                {
                    numbers.Add(new Coordinate(0, y + 1), (currentDigit, BottomRight));
                }
            }
            List<int> debug = new List<int>();
            foreach (KeyValuePair<Coordinate, (int, Coordinate)> number in numbers)
            {
                foreach (KeyValuePair<Coordinate, char> thingimajing in Thingimajings)
                {
                    if (thingimajing.Key.IsBetween(number.Key, number.Value.Item2))
                    {
                        ReturnValue += number.Value.Item1;
                        debug.Add(number.Value.Item1);
                        break;
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
}
