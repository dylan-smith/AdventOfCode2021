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
            var depths = input.Integers();

            var cur = depths.First();

            var increases = 0;

            for (var i = 0; i < depths.Count(); i++)
            {
                if (depths.ElementAt(i) > cur)
                {
                    increases++;
                }

                cur = depths.ElementAt(i);
            }

            return increases.ToString();
        }

        public override string PartTwo(string input)
        {
            var depths = input.Integers().ToArray();

            var a = 0;
            var count = 0;

            var sum = depths[a] + depths[a + 1] + depths[a + 2];

            a++;

            while (a + 2 < depths.Length)
            {
                var newSum = depths[a] + depths[a + 1] + depths[a + 2];

                if (newSum > sum)
                {
                    count++;
                }

                sum = newSum;
                a++;
            }

            return count.ToString();
        }
    }
}
