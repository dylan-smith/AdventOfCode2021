namespace AdventOfCode.Days;

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
            }
            else
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

    private Dictionary<string, string> _rules;
    private Dictionary<(string pair, int steps), Dictionary<char, long>> _lookup;

    public override string PartTwo(string input)
    {
        var polymer = input.Lines().First();
        var rules = input.Lines().Skip(1).Select(ParseRule).ToList();
        var steps = 1;

        _rules = new Dictionary<string, string>();
        rules.ForEach(r => _rules.Add(r.pair, r.element.ToString()));

        _lookup = new Dictionary<(string pair, int steps), Dictionary<char, long>>();

        foreach (var rule in _rules)
        {
            _lookup.Add((rule.Key, steps), CalcPairData(rule.Key, steps));
        }

        var elementCounts = ExpandPolymer(polymer, steps);

        var max = elementCounts.Max(x => x.Value);
        var min = elementCounts.Min(x => x.Value);

        return (max - min).ToString();
    }

    private Dictionary<char, long> ExpandPolymer(string polymer, int steps)
    {
        var result = new Dictionary<char, long>();

        result.SafeIncrement(polymer.First());

        foreach (var pair in polymer.WindowS(2).ToList())
        {
            foreach (var counts in _lookup[(pair, steps)])
            {
                result.SafeIncrement(counts.Key, counts.Value);
            }

            result.SafeDecrement(pair[0]);
        }

        return result;
    }

    private Dictionary<char, long> CalcPairData(string pair, int steps)
    {
        if (_lookup.ContainsKey((pair, steps)))
        {
            return _lookup[(pair, steps)];
        }

        var rule = _rules[pair];
        var result = new Dictionary<char, long>();

        if (steps == 0)
        {
            result.SafeIncrement(pair[0]);
            result.SafeIncrement(pair[1]);

            return result;
        }

        var left = pair[0].ToString() + rule;
        var right = rule + pair[1].ToString();

        var leftCounts = CalcPairData(left, steps - 1);
        var rightCounts = CalcPairData(right, steps - 1);

        

        _lookup[(left, steps - 1)] = leftCounts;
        _lookup[(right, steps - 1)] = rightCounts;

        if (leftCounts.Any(x => x.Value < 0))
        {
            var foo = "blah";
        }

        rightCounts.ForEach(x => leftCounts.SafeIncrement(x.Key, x.Value));
        leftCounts.SafeDecrement(rule[0]);

        if (leftCounts.Any(x => x.Value < 0))
        {
            var foo = "blah";
        }

        return leftCounts;
    }
}