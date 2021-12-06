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
            var fish = input.Longs().ToList();

            for (var day = 1; day <= 80; day++)
            {
                var fishCount = fish.Count();
                for (var i = 0; i < fishCount; i++)
                {
                    fish[i]--;

                    if (fish[i] < 0)
                    {
                        fish.Add(8L);
                        fish[i] = 6L;
                    }
                }
            }

            return fish.Count().ToString();
        }

        public override string PartTwo(string input)
        {
            var fish = input.Integers().ToList();

            var lanterns = new Dictionary<int, long>();
            lanterns.Add(0, 0);
            lanterns.Add(1, 0);
            lanterns.Add(2, 0);
            lanterns.Add(3, 0);
            lanterns.Add(4, 0);
            lanterns.Add(5, 0);
            lanterns.Add(6, 0);
            lanterns.Add(7, 0);
            lanterns.Add(8, 0);

            foreach (var f in fish)
            {
                lanterns[f] = lanterns[f] + 1;
            }

            for (var day = 1; day <= 256; day++)
            {
                var newLanterns = new Dictionary<int, long>();
                newLanterns.Add(0, 0);
                newLanterns.Add(1, 0);
                newLanterns.Add(2, 0);
                newLanterns.Add(3, 0);
                newLanterns.Add(4, 0);
                newLanterns.Add(5, 0);
                newLanterns.Add(6, 0);
                newLanterns.Add(7, 0);
                newLanterns.Add(8, 0);

                foreach (var l in lanterns)
                {
                    if (l.Key > 0)
                    {
                        newLanterns[l.Key - 1] = lanterns[l.Key];
                    }
                }

                newLanterns[8] = lanterns[0];
                newLanterns[6] += lanterns[0];


                lanterns = newLanterns;
            }

            return lanterns.Sum(x => x.Value).ToString();
        }
    }
}
