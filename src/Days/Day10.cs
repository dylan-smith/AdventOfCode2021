namespace AdventOfCode.Days;

[Day(2021, 10)]
public class Day10 : BaseDay
{
    public override string PartOne(string input)
    {
        var result = input.Lines().Sum(x => CalcSyntaxScore(x));

        return result.ToString();
    }

    private long CalcSyntaxScore(string input)
    {
        var line = CollapseLine(input);

        var corruptChar = line.FirstOrDefault(c => c is ')' or ']' or '}' or '>');

        switch (corruptChar)
        {
            case default(char):
                return 0;
            case ')':
                return 3;
            case ']':
                return 57;
            case '}':
                return 1197;
            case '>':
                return 25137;
            default:
                throw new Exception();
        }
    }

    public override string PartTwo(string input)
    {
        var lines = input.Lines().Where(x => CalcSyntaxScore(x) == 0).ToList();

        var scores = lines.Select(x => CalcAutoCompleteScore(x)).OrderBy(x => x).ToList();

        var result = scores[scores.Count / 2];

        return result.ToString();
    }

    private long CalcAutoCompleteScore(string input)
    {
        var line = CollapseLine(input);

        var closingChar = new Dictionary<char, char>();
        closingChar.Add('(', ')');
        closingChar.Add('[', ']');
        closingChar.Add('{', '}');
        closingChar.Add('<', '>');

        var completion = string.Empty;

        while (line.Length > 0)
        {
            completion += closingChar[line.Last()];
            line = line.ShaveRight(1);
        }

        return GetCompletionScore(completion);
    }

    private string CollapseLine(string input)
    {
        var next = input;

        do
        {
            input = next;
            next = input.Replace("()", string.Empty)
                        .Replace("[]", string.Empty)
                        .Replace("{}", string.Empty)
                        .Replace("<>", string.Empty);
        } while (next.Length < input.Length);

        return next;
    }

    private long GetCompletionScore(string completion)
    {
        var score = 0L;
        var charScores = new Dictionary<char, long>();
        charScores.Add(')', 1);
        charScores.Add(']', 2);
        charScores.Add('}', 3);
        charScores.Add('>', 4);

        foreach (var c in completion)
        {
            score *= 5;
            score += charScores[c];
        }

        return score;
    }
}
