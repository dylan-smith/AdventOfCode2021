namespace AdventOfCode.Days;

[Day(2021, 8)]
public class Day08 : BaseDay
{
    private IEnumerable<char> letters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

    public override string PartOne(string input)
    {
        var outputs = input.ParseLines(x => ParseLine(x));

        return outputs.Sum(x => x.Count(y => y.Length is 2 or 4 or 3 or 7)).ToString();
    }

    private IEnumerable<string> ParseLine(string line)
    {
        var splitPos = line.IndexOf('|');

        var result = line.ShaveLeft(splitPos + 1);

        return result.Words();
    }

    public override string PartTwo(string input)
    {
        var lines = input.Lines();

        var results = lines.Sum(x => ProcessLine(x));

        return results.ToString();
    }

    private int ProcessLine(string line)
    {
        var splitPos = line.IndexOf('|');
        var left = line[..splitPos];
        var right = line[(splitPos + 1)..];

        var mapping = GetLetterMap(left);
        var output = GetOutput(right, mapping).ToList();
        var result = ConvertToNum(output);

        return result;
    }

    private int ConvertToNum(IEnumerable<string> output)
    {
        var result = string.Empty;

        foreach (var digit in output)
        {
            result += GetDigit(digit);
        }

        return int.Parse(result);
    }

    private string GetDigit(string digit)
    {
        if (digit.Count() == 6 && digit.Contains('a') && digit.Contains('b') && digit.Contains('c') && digit.Contains('e') && digit.Contains('f') && digit.Contains('g'))
        {
            return "0";
        }

        if (digit.Count() == 2)
        {
            return "1";
        }

        if (digit.Count() == 5 && digit.Contains('a') && digit.Contains('c') && digit.Contains('d') && digit.Contains('e') && digit.Contains('g'))
        {
            return "2";
        }

        if (digit.Count() == 5 && digit.Contains('a') && digit.Contains('c') && digit.Contains('d') && digit.Contains('f') && digit.Contains('g'))
        {
            return "3";
        }

        if (digit.Count() == 4)
        {
            return "4";
        }

        if (digit.Count() == 5 && digit.Contains('a') && digit.Contains('b') && digit.Contains('d') && digit.Contains('f') && digit.Contains('g'))
        {
            return "5";
        }

        if (digit.Count() == 6 && digit.Contains('a') && digit.Contains('b') && digit.Contains('d') && digit.Contains('e') && digit.Contains('f') && digit.Contains('g'))
        {
            return "6";
        }

        if (digit.Count() == 3)
        {
            return "7";
        }

        if (digit.Count() == 7)
        {
            return "8";
        }

        return "9";
    }

    private IEnumerable<string> GetOutput(string right, Dictionary<char, char> mapping)
    {
        var outputWords = right.Words().ToList();

        foreach (var outputWord in outputWords)
        {
            yield return ApplyMap(outputWord, mapping);
        }
    }

    private string ApplyMap(string outputWord, IDictionary<char, char> mapping)
    {
        var result = string.Empty;

        foreach (var c in outputWord)
        {
            result += mapping.Single(x => x.Value == c).Key;
        }

        return result;
    }

    private Dictionary<char, char> GetLetterMap(string left)
    {
        var words = left.Words().ToList();
        var result = new Dictionary<char, char>();

        result.Add('b', GetB(words));
        result.Add('e', GetE(words));
        result.Add('f', GetF(words));
        result.Add('c', GetC(words, result['f']));
        result.Add('a', GetA(words, result['c'], result['f']));
        result.Add('d', GetD(words, result['b'], result['c'], result['f']));
        result.Add('g', GetG(words, result['a'], result['b'], result['c'], result['d'], result['e'], result['f']));

        return result;
    }

    private char GetG(List<string> words, char a, char b, char c, char d, char e, char f)
    {
        var eightWord = words.Single(w => w.Length == 7);

        return eightWord.Single(x => x != a && x != b && x != c && x != d && x != e && x != f);
    }

    private char GetD(List<string> words, char b, char c, char f)
    {
        var fourWord = words.Single(w => w.Length == 4);

        return fourWord.Single(x => x != b && x != c && x != f);
    }

    private char GetA(List<string> words, char c, char f)
    {
        var sevenWord = words.Single(w => w.Length == 3);

        return sevenWord.Single(x => x != c && x != f);
    }

    private char GetC(IEnumerable<string> words, char f)
    {
        var oneWord = words.Single(w => w.Length == 2);

        return oneWord.Single(c => c != f);
    }

    private char GetE(IEnumerable<string> words)
    {
        foreach (var letter in letters)
        {
            var count = words.Count(w => w.Contains(letter));

            if (count == 4)
            {
                return letter;
            }
        }

        throw new Exception();
    }

    private char GetB(IEnumerable<string> words)
    {
        foreach (var letter in letters)
        {
            var count = words.Count(w => w.Contains(letter));

            if (count == 6)
            {
                return letter;
            }
        }

        throw new Exception();
    }

    private char GetF(IEnumerable<string> words)
    {
        foreach (var letter in letters)
        {
            var count = words.Count(w => w.Contains(letter));

            if (count == 9)
            {
                return letter;
            }
        }

        throw new Exception();
    }
}

