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
            var fish = InitFish(input);
            80.Times(() => fish = SimulateFish(fish));

            return fish.Sum(x => x.Value).ToString();
        }

        public override string PartTwo(string input)
        {
            var fish = InitFish(input);
            256.Times(() => fish = SimulateFish(fish));

            return fish.Sum(x => x.Value).ToString();
        }

        private IDictionary<int, long> InitFish(string input)
        {
            var inputFish = input.Integers().ToList();
            IDictionary<int, long> fish = new Dictionary<int, long>();
            inputFish.ForEach(f => fish.SafeIncrement(f));

            return fish;
        }

        private IDictionary<int, long> SimulateFish(IDictionary<int, long> fish)
        {
            var newFish = new Dictionary<int, long>();
            fish.ForEach(x => newFish.SafeSet((x.Key + 8) % 9, x.Value));
            newFish.SafeIncrement(6, fish.SafeGet(0));

            return newFish;
        }
    }
}
