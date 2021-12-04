namespace AdventOfCode.Days;

[Day(2021, 1)]
public class Day01 : BaseDay
{
    public override string PartOne(string input)
    {
        var depths = input.Integers();

        var count = depths.Window(2)
                          .Count(x => x.Last() > x.First());

        return count.ToString();
    }

    public override string PartTwo(string input)
    {
        var depths = input.Integers();

        // This is a trick because if the 4th one is greater than the 1st, that means the new 3-window will be bigger than the previous
        var count = depths.Window(4)
                          .Count(x => x.Last() > x.First());

        return count.ToString();
    }
}
