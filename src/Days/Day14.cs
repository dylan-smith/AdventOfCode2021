using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    [Day(2021, 14)]
    public class Day14 : BaseDay
    {
        public override string PartOne(string input)
        {
            var polymer = input.Lines().First();
            var rules = input.Lines().Skip(1).Select(ParseRule).ToList();

            40.Times(() => polymer = Step(polymer, rules));

            var max = polymer.GroupBy(c => c).Max(g => g.Count());
            var min = polymer.GroupBy(c => c).Min(g => g.Count());

            return (max - min).ToString();
        }

        private string Step(string polymer, IEnumerable<(string pair, char element)> rules)
        {
            var result = string.Empty;
            var first = true;

            foreach (var p in polymer.Window(2).ToList())
            {
                var rule = rules.Single(r => r.pair == $"{p.First()}{p.Last()}");

                if (first)
                {
                    result += p.First().ToString() + rule.element.ToString() + p.Last().ToString();
                    first = false;
                } else
                {
                    result += rule.element.ToString() + p.Last().ToString();
                }
            }

            //base.Log(result);
            return result;
        }

        private (string pair, char element) ParseRule(string line)
        {
            return (line.Words().First(), line.Words().Last()[0]);
        }

        public override string PartTwo(string input)
        {
            return string.Empty;
        }
    }
}
