using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    [Day(2021, 17)]
    public class Day17 : BaseDay
    {
        public override string PartOne(string input)
        {
            var yDelta = 117;
            var y = 0;
            var steps = 0;
            var yMax = 0;

            while (y != -118)
            {
                y += yDelta;
                yDelta--;
                steps++;

                if (y >= yMax)
                {
                    yMax = y;
                }
            }

            base.Log($"yMax: {yMax}");
            base.Log($"steps: {steps}");

            for (var xDelta = 1; xDelta <= 259; xDelta++)
            {
                var delta = xDelta;
                var x = 0;

                while (x < 235)
                {
                    x += delta;
                    delta--;
                }

                if (x is >= 235 and <= 259)
                {
                    base.Log($"xDelta: {xDelta}");
                    break;
                }
            }

            return yMax.ToString();
        }

        public override string PartTwo(string input)
        {
            return string.Empty;
        }
    }
}
