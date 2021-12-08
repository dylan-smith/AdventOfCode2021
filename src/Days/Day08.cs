namespace AdventOfCode.Days;

[Day(2021, 8)]
public class Day08 : BaseDay
{
    private readonly IDictionary<string, string> _digitPatterns = new Dictionary<string, string>();
    private readonly IEnumerable<string> _mappings = "abcdefg".GetPermutations<string>().ToList();
    private readonly string _defaultMap = "abcdefg";

    public Day08()
    {
        _digitPatterns.Add("abcefg", "0");
        _digitPatterns.Add("cf", "1");
        _digitPatterns.Add("acdeg", "2");
        _digitPatterns.Add("acdfg", "3");
        _digitPatterns.Add("bcdf", "4");
        _digitPatterns.Add("abdfg", "5");
        _digitPatterns.Add("abdefg", "6");
        _digitPatterns.Add("acf", "7");
        _digitPatterns.Add("abcdefg", "8");
        _digitPatterns.Add("abcdfg", "9");
    }

    public override string PartOne(string input)
    {
        var displays = input.ParseLines(ParseLine);

        var result = displays.Sum(d => d.digits.Count(x => x.Length is 2 or 3 or 4 or 7));

        return result.ToString();
    }

    private (IEnumerable<string> patterns, IEnumerable<string> digits) ParseLine(string line)
    {
        var splitPos = line.IndexOf('|');
        var left = line[..splitPos];
        var right = line[(splitPos + 1)..];

        var patterns = left.Words().Select(NormalizePattern);
        var digits = right.Words().Select(NormalizePattern);

        return (patterns, digits);
    }

    private string NormalizePattern(string pattern) => string.Concat(pattern.OrderBy(c => c));

    public override string PartTwo(string input)
    {
        var displays = input.ParseLines(ParseLine);

        var result = displays.Sum(x => SolveDisplay(x.patterns, x.digits));

        return result.ToString();
    }

    private int SolveDisplay(IEnumerable<string> patterns, IEnumerable<string> digits)
    {
        var mapping = _mappings.First(x => IsValidMapping(x, patterns));

        return ResolveDigits(mapping, digits);
    }

    private int ResolveDigits(string mapping, IEnumerable<string> digits)
    {
        var result = string.Concat(digits.Select(x => MapPattern(x, mapping)).Select(x => _digitPatterns[x]).ToList());

        return int.Parse(result);
    }

    private bool IsValidMapping(string mapping, IEnumerable<string> patterns)
    {
        var mappedPatterns = patterns.Select(x => MapPattern(x, mapping)).Select(NormalizePattern);

        return IsCorrectPatterns(mappedPatterns);
    }

    private bool IsCorrectPatterns(IEnumerable<string> mappedPatterns) => mappedPatterns.All(x => _digitPatterns.ContainsKey(x));

    private string MapPattern(string pattern, string mapping)
    {
        var result = pattern.Select(c => _defaultMap[mapping.IndexOf(c)]);
        return NormalizePattern(string.Concat(result));
    }
}

