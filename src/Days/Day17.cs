using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    [Day(2021, 17)]
    public class Day17 : BaseDay
    {
        private const int targetXMin = 235;
        private const int targetXMax = 259;
        private const int targetYMin = -118;
        private const int targetYMax = -62;

        //private const int targetXMin = 20;
        //private const int targetXMax = 30;
        //private const int targetYMin = -10;
        //private const int targetYMax = -5;

        public override string PartOne(string input)
        {
            var yDelta = -targetYMin - 1;
            var y = 0;
            var steps = 0;
            var yMax = 0;

            while (y != targetYMin)
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

            for (var xDelta = 1; xDelta <= targetXMax; xDelta++)
            {
                var delta = xDelta;
                var x = 0;

                while (x < targetXMin)
                {
                    x += delta;
                    delta--;
                }

                if (x is >= targetXMin and <= targetXMax)
                {
                    base.Log($"xDelta: {xDelta}");
                    break;
                }
            }

            return yMax.ToString();
        }

        public override string PartTwo(string input)
        {
            var xOptions = Enumerable.Range(0, targetXMax + 1);
            var yOptions = Enumerable.Range(targetYMin, (-targetYMin)*2);
            var velocities = new List<(int x, int y)>();

            foreach (var x in xOptions)
            {
                foreach (var y in yOptions)
                {
                    velocities.Add((x, y));
                }
            }

            base.Log($"possible count: {velocities.Count}");

            return velocities.Count(v => TestVelocity(v.x, v.y)).ToString();
        }

        private bool TestVelocity(int xVelocity, int yVelocity)
        {
            var x = 0;
            var y = 0;
            var xDelta = xVelocity;
            var yDelta = yVelocity;

            while (x <= targetXMax && y >= targetYMin)
            {
                x += xDelta;
                y += yDelta;

                xDelta--;
                yDelta--;

                xDelta = Math.Max(0, xDelta);

                if (IsInTarget(x, y))
                {
                    base.Log($"Valid: {xVelocity}, {yVelocity}");
                    return true;
                }
            }

            return false;
        }

        private bool IsInTarget(int x, int y)
        {
            return x is >= targetXMin and <= targetXMax && y is >= targetYMin and <= targetYMax;
        }
    }
}
