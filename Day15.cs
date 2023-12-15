using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2023
{
    public class Day15 : Day
    {
        string[] Instructions;
        Dictionary<int,Box> Boxes;
        public Day15(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseStringArray(Input, ",");
            Boxes= new Dictionary<int,Box>();
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            foreach (string s in Instructions)
            {
                ReturnValue += Hashis(s);
            }

            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            for (int i = 0;i<256;i++)
            {
                Boxes.Add(i, new Box(i));
            }
            foreach (string s in Instructions)
            {
                string hashstring = s.Replace('=','-');
                string lensId = hashstring.Split('-').First();
                int id = Hashis(lensId);
                if (s.Contains('='))
                {
                    int lensStrenght = this.ParseListOfInteger(s).First();
                    if (Boxes[id].Lenses.ContainsKey(lensId))
                        Boxes[id].Lenses[lensId] = lensStrenght;
                    else
                    {
                        Boxes[id].Lenses.Add(lensId, lensStrenght);
                    }

                }
                else
                {

                    if (Boxes[id].Lenses.ContainsKey(lensId))
                    {
                        Boxes[id].Lenses.Remove(lensId);
                    }
                }
            }
            foreach(Box box in Boxes.Values)
            {
                int i = 0;
                foreach(int lens in box.Lenses.Values)
                {
                    i++;
                    ReturnValue += (box.Id + 1) * (i) * lens;
                }
            }

            return ReturnValue.ToString();
        }
        int Hashis(string hhaassshh)
        {
            int CurrentValue = 0;
            foreach (char c in hhaassshh)
            {
                CurrentValue += (int)c;
                CurrentValue *= 17;
                CurrentValue %= 256;
            }
            return CurrentValue;
        }
    }
    public class Box
    {
        public int Id;
        public Dictionary<string,int> Lenses;
        public Box(int id)
        {
            Id = id;
            Lenses = new Dictionary<string, int>();
        }
    }
}
