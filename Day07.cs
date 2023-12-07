using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace Advent2023
{
    public class Day07 : Day
    {
        Dictionary<string, (int Winnability, int Bet)> Hands;
        Dictionary<string, (int Winnability, int Bet)> JokerHands;
        Dictionary<char, int> Values;
        public Day07(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Input = Input.Replace('T','a');
            Input = Input.Replace('J', 'b');
            Input = Input.Replace('Q', 'c');
            Input = Input.Replace('K', 'd');
            Input = Input.Replace('A', 'e');
            List<string[]> strings = this.ParseListOfStringArraysLineSpace(Input);
            Hands = new Dictionary<string, (int Winnability, int Bet)>();
            JokerHands = new Dictionary<string, (int Winnability, int Bet)>();
            foreach (string[] instruction in strings)
            {
                List<char> sortHand = instruction[0].ToCharArray().ToList();
                sortHand.Sort();
                Hands.Add(instruction[0], (1, Int32.Parse(instruction[1])));
                JokerHands.Add(instruction[0].Replace('b','1'), (1, Int32.Parse(instruction[1])));
            }
            Values = new Dictionary<char, int>
            {
                { 'A',14 },
                { 'K',13 },
                { 'Q',12 },
                { 'J',11 },
                { 'T',10 },
                { '9',9 },
                { '8',8 },
                { '7',7 },
                { '6',6 },
                { '5',5 },
                { '4',4 },
                { '3',3 },
                { '2',2 },
            };
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            var MenJagVillÄndra = Hands;
            foreach (KeyValuePair<string, (int Winnability, int Bet)> hand in MenJagVillÄndra)
            {
                int Winnability = 0;
                IEnumerable<IGrouping<char, char>> groups = hand.Key.ToCharArray().GroupBy(x => x);
                List<int> numbers = new List<int>();
                foreach (IGrouping<char, char> g in groups)
                {
                    numbers.Add(g.Count());
                }
                numbers.Sort((a, b) => b.CompareTo(a));
                if (numbers[0] == 5)
                    Winnability = 7;
                else if (numbers[0] == 4)
                    Winnability = 6;
                else
                {
                    if (numbers[0] == 3)
                        Winnability = 3;
                    if (numbers[0] == 2)
                        Winnability = 1;
                    Winnability += numbers[1];
                    if (numbers[0] == 1)
                        Winnability = 1; // -.-
                }
                Hands[hand.Key] = (Winnability, hand.Value.Bet);
            }
            int number = 1;
            List<string> debug= new List<string>();
            for(int i = 1; i <= 7; i++)
            {
                var sortedGroup = Hands.Where(x => x.Value.Winnability == i).OrderBy(x=>x.Key);
                foreach(var item in sortedGroup)
                {
                    ReturnValue += number * item.Value.Bet;
                    number++;
                    debug.Add(item.Key);
                }
                ;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            var MenJagVillÄndra = JokerHands;
                foreach (KeyValuePair<string, (int Winnability, int Bet)> hand in MenJagVillÄndra)
            {
                int Winnability = 0;
                IEnumerable<IGrouping<char, char>> groups = hand.Key.ToCharArray().GroupBy(x => x);
                List<int> numbers = new List<int>();
                foreach (IGrouping<char, char> g in groups)
                {
                    numbers.Add(g.Count());
                }
                numbers.Sort((a, b) => b.CompareTo(a));
                int jokers = 0;
                if(groups.FirstOrDefault(g => g.Key == '1')!=null)
                    jokers = groups.First(g => g.Key == '1').Count();

                if (jokers == 1)
                    numbers[0] += jokers;
                else if (jokers == 2)
                {
                    numbers[0] = numbers[1] + jokers;
                    numbers[1] = 1;
                }
                else if (jokers >= 3 && jokers != 5)
                {
                    numbers[0] += numbers[1];
                    numbers[1] = 1;
                }


                if (numbers[0] == 5)
                {
                    Winnability = 7;
                }
                else if (numbers[0] == 4)
                    Winnability = 6;
                else
                {
                    if (numbers[0] == 3)
                        Winnability = 3;
                    if (numbers[0] == 2)
                        Winnability = 1;
                    Winnability += numbers[1];
                    if (numbers[0] == 1)
                        Winnability = 1; // -.-
                }
                JokerHands[hand.Key] = (Winnability, hand.Value.Bet);
            }
            int number = 1;
            List<string> debug = new List<string>();
            for (int i = 1; i <= 7; i++)
            {
                var sortedGroup = JokerHands.Where(x => x.Value.Winnability == i).OrderBy(x => x.Key);
                foreach (var item in sortedGroup)
                {
                    ReturnValue += number * item.Value.Bet;
                    number++;
                    debug.Add(item.Key);
                }
                ;
            }
            return ReturnValue.ToString();
        }
    }
}
