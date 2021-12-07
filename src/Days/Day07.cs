using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    [Day(2021, 7)]
    public class Day07 : BaseDay
    {
        public override string PartOne(string input)
        {
            var crabs = input.Integers().ToList();
            var fuel = 0;
            var result = int.MaxValue;

            for (var i = crabs.Min(); i <= crabs.Max(); i++)
            {
                fuel = crabs.Sum(x => Math.Abs(x - i));

                if (fuel <= result)
                {
                    result = fuel;
                }
            }

            return result.ToString();
        }

        public override string PartTwo(string input)
        {
            var crabs = input.Integers().ToList();
            var fuel = 0;
            var result = int.MaxValue;

            for (var i = crabs.Min(); i <= crabs.Max(); i++)
            {
                fuel = crabs.Sum(x => FuelUsed(x, i));

                if (fuel <= result)
                {
                    result = fuel;
                }
            }

            return result.ToString();
        }

        private int FuelUsed(int start, int target)
        {
            var result = 0;
            var pos = start;
            var fuel = 1;

            while (pos != target)
            {
                result += fuel++;
                if (target > pos)
                {
                    pos++;
                }
                else
                {
                    pos--;
                }
            }

            return result;
        }
    }
}
