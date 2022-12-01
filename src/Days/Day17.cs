using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    [Day(2021, 17)]
    public class Day17 : BaseDay
    {
        private int targetXMin;
        private int targetXMax;
        private int targetYMin;
        private int targetYMax;

        public override string PartOne(string input)
        {
            ReadInput(input);
            
            var yDelta = -targetYMin - 1;
            var y = 0;

            while (yDelta > 0)
            {
                y += yDelta--;
            }

            return y.ToString();
        }

        private void ReadInput(string input)
        {
            var words = input.Words().ToList();
            var xText = words[2].ShaveLeft("x=".Length);
            var yText = words[3].ShaveLeft("y=".Length);

            targetXMin = int.Parse(xText.Split('.', StringSplitOptions.RemoveEmptyEntries)[0]);
            targetXMax = int.Parse(xText.Split('.', StringSplitOptions.RemoveEmptyEntries)[1]);
            targetYMin = int.Parse(yText.Split('.', StringSplitOptions.RemoveEmptyEntries)[0]);
            targetYMax = int.Parse(yText.Split('.', StringSplitOptions.RemoveEmptyEntries)[1]);
        }

        public override string PartTwo(string input)
        {
            ReadInput(input);
            
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

            base.Log($"possible: {velocities.Count}");

            return velocities.Count(v => TestVelocity(v.x, v.y)).ToString();
        }

        private bool TestVelocity(int xVelocity, int yVelocity)
        {
            var x = 0;
            var y = 0;

            while (x <= targetXMax && y >= targetYMin)
            {
                x += xVelocity--;
                y += yVelocity--;

                xVelocity = Math.Max(0, xVelocity);

                if (IsInTarget(x, y))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInTarget(int x, int y)
        {
            return x >= targetXMin && x <= targetXMax && y >= targetYMin && y <= targetYMax;
        }
    }
}
