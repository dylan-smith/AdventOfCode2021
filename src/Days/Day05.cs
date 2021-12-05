using System;
using System.Drawing;
using System.Linq;

namespace AdventOfCode.Days
{
    [Day(2021, 5)]
    public class Day05 : BaseDay
    {
        public override string PartOne(string input)
        {
            var vents = input.ParseLines(ParseVent);

            vents = vents.Where(x => x.start.X == x.end.X || x.start.Y == x.end.Y).ToList();

            var maxX = Math.Max(vents.Max(v => v.start.X), vents.Max(v => v.end.X));
            var maxY = Math.Max(vents.Max(v => v.start.Y), vents.Max(v => v.end.Y));

            var danger = new Dictionary<Point, int>();

            foreach (var vent in vents)
            {
                if (vent.start.X == vent.end.X)
                {
                    var startY = Math.Min(vent.start.Y, vent.end.Y);
                    var endY = Math.Max(vent.start.Y, vent.end.Y);

                    for (var y = startY; y <= endY; y++)
                    {
                        danger.SafeIncrement(new Point(vent.start.X, y));
                    }
                }

                if (vent.start.Y == vent.end.Y)
                {
                    var startX = Math.Min(vent.start.X, vent.end.X);
                    var endX = Math.Max(vent.start.X, vent.end.X);

                    for (var x = startX; x <= endX; x++)
                    {
                        danger.SafeIncrement(new Point(x, vent.start.Y));
                    }
                }
            }

            return danger.Count(x => x.Value > 1).ToString();
        }

        private (Point start, Point end) ParseVent(string line)
        {
            var startX = line.Split("->")[0].Integers().First();
            var startY = line.Split("->")[0].Integers().Last();
            var endX = line.Split("->")[1].Integers().First();
            var endY = line.Split("->")[1].Integers().Last();

            return (new Point(startX, startY), new Point(endX, endY));
        }

        public override string PartTwo(string input)
        {
            var vents = input.ParseLines(ParseVent);

            var maxX = Math.Max(vents.Max(v => v.start.X), vents.Max(v => v.end.X));
            var maxY = Math.Max(vents.Max(v => v.start.Y), vents.Max(v => v.end.Y));

            var danger = new Dictionary<Point, int>();

            foreach (var vent in vents)
            {
                var ventPoints = GetVentPoints(vent);

                foreach (var p in ventPoints)
                {
                    danger.SafeIncrement(p);
                }
            }

            return danger.Count(x => x.Value > 1).ToString();
        }

        private IEnumerable<Point> GetVentPoints((Point start, Point end) vent)
        {
            if (vent.start.X == vent.end.X)
            {
                var startY = Math.Min(vent.start.Y, vent.end.Y);
                var endY = Math.Max(vent.start.Y, vent.end.Y);

                for (var y = startY; y <= endY; y++)
                {
                    yield return new Point(vent.start.X, y);
                }

                yield break;
            }

            if (vent.start.Y == vent.end.Y)
            {
                var startX = Math.Min(vent.start.X, vent.end.X);
                var endX = Math.Max(vent.start.X, vent.end.X);

                for (var x = startX; x <= endX; x++)
                {
                    yield return new Point(x, vent.start.Y);
                }

                yield break;
            }

            var left = vent.start;
            var right = vent.end;

            if (vent.start.X > vent.end.X)
            {
                left = vent.end;
                right = vent.start;
            }

            var top = vent.start;
            var bottom = vent.end;

            if (vent.start.Y > vent.end.Y)
            {
                top = vent.end;
                bottom = vent.start;
            }

            if (left == top)
            {
                var x = left.X;
                var y = left.Y;

                while (x <= right.X)
                {
                    yield return new Point(x, y);
                    x++;
                    y++;
                }
            }
            else
            {
                var x = left.X;
                var y = left.Y;

                while (x <= right.X)
                {
                    yield return new Point(x, y);
                    x++;
                    y--;
                }
            }
        }
    }
}
