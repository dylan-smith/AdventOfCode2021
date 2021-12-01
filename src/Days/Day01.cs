using System.Linq;

namespace AdventOfCode.Days
{
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

            var count = depths.Window(3)
                              .Select(x => x.Sum())
                              .Window(2)
                              .Count(x => x.Last() > x.First());

            return count.ToString();
        }
    }
}
