using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    [Day(2021, 6)]
    public class Day06 : BaseDay
    {
        public override string PartOne(string input)
        {
            var fish = input.Integers().ToList();

            return SimulateFish(fish, 80).ToString();
        }

        public override string PartTwo(string input)
        {
            var fish = input.Integers().ToList();

            return SimulateFish(fish, 256).ToString();
        }

        private long SimulateFish(IEnumerable<int> fish, int days)
        {
            var lanterns = new Dictionary<int, long>();
            fish.ForEach(f => lanterns.SafeIncrement(f));

            for (var day = 1; day <= days; day++)
            {
                var newLanterns = new Dictionary<int, long>();
                lanterns.ForEach(x => newLanterns.SafeSet((x.Key + 8) % 9, x.Value));
                newLanterns.SafeIncrement(6, lanterns.SafeGet(0));

                lanterns = newLanterns;
            }

            return lanterns.Sum(x => x.Value);
        }
    }
}
