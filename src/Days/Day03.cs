namespace AdventOfCode.Days;

[Day(2021, 3)]
public class Day03 : BaseDay
{
    public override string PartOne(string input)
    {
        var diag = input.Lines().ToList();

        var gamma = Convert.ToInt64(Gamma(diag), 2);
        var epsilon = Convert.ToInt64(Epsilon(diag), 2);

        return (gamma * epsilon).ToString();
    }

    private string Gamma(List<string> diag)
    {
        var result = string.Empty;

        for (var i = 0; i < diag.First().Length; i++)
        {
            var oneCount = diag.Count(x => x[i] == '1');
            var zeroCount = diag.Count - oneCount;

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

    private string Epsilon(List<string> diag)
    {
        var result = string.Empty;

        for (var i = 0; i < diag.First().Length; i++)
        {
            var oneCount = diag.Count(x => x[i] == '1');
            var zeroCount = diag.Count - oneCount;

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

        while (oxygen.Count > 1)
        {
            var gamma = Gamma(oxygen);

            oxygen = oxygen.Where(x => x[pos] == gamma[pos]).ToList();
            pos++;
        }

        pos = 0;
        while (scrubber.Count > 1)
        {
            var epsilon = Epsilon(scrubber);

            scrubber = scrubber.Where(x => x[pos] == epsilon[pos]).ToList();
            pos++;
        }

        return (Convert.ToInt64(oxygen.Single(), 2) * Convert.ToInt64(scrubber.Single(), 2)).ToString();
    }
}
