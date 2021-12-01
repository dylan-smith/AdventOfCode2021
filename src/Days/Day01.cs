using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    [Day(2021, 1)]
    public class Day01 : BaseDay
    {
        public override string PartOne(string input)
        {
            var depths = input.Integers().ToArray();
            var count = depths.SelectWithIndex().Count(x => x.index > 0 && x.item > depths[x.index - 1]);

            return count.ToString();
        }

        public override string PartTwo(string input)
        {
            var depths = input.Integers().ToArray();
            depths = depths.SelectWithIndex().Where(x => x.index > 1).Select(x => x.item + depths[x.index - 1] + depths[x.index - 2]).ToArray();
            var count = depths.SelectWithIndex().Count(x => x.index > 0 && x.item > depths[x.index - 1]);

            return count.ToString();
        }
    }
}
