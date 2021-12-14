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

            10.Times(() => polymer = Step(polymer, rules));

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

            return result;
        }

        private (string pair, char element) ParseRule(string line)
        {
            return (line.Words().First(), line.Words().Last()[0]);
        }

        public override string PartTwo(string input)
        {
            var polymer = input.Lines().First();
            var rules = input.Lines().Skip(1).Select(ParseRule).ToList();
            var steps = 40;

            var pairData = new Dictionary<string, Dictionary<int, Dictionary<char, long>>>();

            foreach (var rule in rules)
            {
                CalcPairData(rule.pair, steps, rules, pairData);
            }

            var elementCounts = new Dictionary<char, long>();

            elementCounts.SafeIncrement(polymer.First());

            foreach (var window in polymer.Window(2).ToList())
            {
                var pair = $"{window.First().ToString()}{window.Last().ToString()}";

                foreach (var counts in pairData[pair][steps])
                {
                    elementCounts.SafeIncrement(counts.Key, counts.Value);
                }

                elementCounts.SafeDecrement(window.First());
            }

            var max = elementCounts.Max(x => x.Value);
            var min = elementCounts.Min(x => x.Value);

            return (max - min).ToString();
        }

        private void CalcPairData(string pair, int steps, List<(string pair, char element)> rules, Dictionary<string, Dictionary<int, Dictionary<char, long>>> pairData)
        {
            if (pairData.ContainsKey(pair) && pairData[pair].ContainsKey(steps))
            {
                return;
            }

            var after = Step(pair, rules);
            var result = new Dictionary<char, long>();

            if (steps == 1)
            {
                foreach (var c in after)
                {
                    result.SafeIncrement(c);
                }
            }
            else
            {
                var pair1 = after.ShaveRight(1);
                var pair2 = after.ShaveLeft(1);

                if (!pairData.ContainsKey(pair1) || !pairData[pair1].ContainsKey(steps - 1))
                {
                    CalcPairData(pair1, steps - 1, rules, pairData);
                }

                if (!pairData.ContainsKey(pair2) || !pairData[pair2].ContainsKey(steps - 1))
                {
                    CalcPairData(pair2, steps - 1, rules, pairData);
                }

                foreach (var i in pairData[pair1][steps - 1])
                {
                    result.SafeIncrement(i.Key, i.Value);
                }

                foreach (var i in pairData[pair2][steps - 1])
                {
                    result.SafeIncrement(i.Key, i.Value);
                }

                result.SafeDecrement(after[1]);
            }

            if (!pairData.ContainsKey(pair))
            {
                pairData.Add(pair, new Dictionary<int, Dictionary<char, long>>());
            }

            pairData[pair].Add(steps, result);
        }
    }
}
