namespace AdventOfCode.Days;

[Day(2021, 3)]
public class Day03 : BaseDay
{
    public override string PartOne(string input)
    {
        var diag = input.Lines().ToList();

        var gamma = Convert.ToInt64(GetGamma(diag), 2);
        var epsilon = Convert.ToInt64(GetEpsilon(diag), 2);

        return (gamma * epsilon).ToString();
    }

    private string GetGamma(List<string> diag)
    {
        var result = string.Empty;

        for (var i = 0; i < diag.First().Length; i++)
        {
            var oneCount = diag.Count(x => x[i] == '1');
            var zeroCount = diag.Count(x => x[i] == '0');

            if (oneCount >= zeroCount)
            {
                result += "1";
            }
            else
            {
                result += "0";
            }
        }

        return result;
    }

    private string GetEpsilon(List<string> diag)
    {
        var result = string.Empty;

        for (var i = 0; i < diag.First().Length; i++)
        {
            var oneCount = diag.Count(x => x[i] == '1');
            var zeroCount = diag.Count(x => x[i] == '0');

            if (oneCount >= zeroCount)
            {
                result += "0";
            }
            else
            {
                result += "1";
            }
        }

        return result;
    }


    public override string PartTwo(string input)
    {
        var oxygen = input.Lines().ToList();
        var scrubber = input.Lines().ToList();
        var pos = 0;

        while (oxygen.Count() > 1)
        {
            var gamma = GetGamma(oxygen)[pos];

            oxygen = oxygen.Where(x => x[pos] == gamma).ToList();
            pos++;
        }

        pos = 0;
        while (scrubber.Count() > 1)
        {
            var epsilon = GetEpsilon(scrubber)[pos];

            scrubber = scrubber.Where(x => x[pos] == epsilon).ToList();
            pos++;
        }

        return (Convert.ToInt64(oxygen.First(), 2) * Convert.ToInt64(scrubber.First(), 2)).ToString();
    }
}
