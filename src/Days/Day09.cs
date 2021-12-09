using System.Drawing;

namespace AdventOfCode.Days;

[Day(2021, 9)]
public class Day09 : BaseDay
{
    public override string PartOne(string input)
    {
        var heights = input.CreateCharGrid();
        var lowPoints = GetLowPoints(heights);

        var result = lowPoints.Sum(p => int.Parse(heights[p.X, p.Y].ToString()) + 1);

        return result.ToString();
    }

    private bool IsLowPoint(int x, int y, char[,] heights)
    {
        var neighbours = heights.GetNeighbors(x, y, false);

        return neighbours.All(n => int.Parse(n.ToString()) > int.Parse(heights[x, y].ToString()));
    }

    public override string PartTwo(string input)
    {
        var heights = input.CreateCharGrid();
        var lowPoints = GetLowPoints(heights);

        var basins = lowPoints.Select(p => GetBasinSize(heights, p)).ToList();

        var result = basins.OrderByDescending(b => b).Take(3).Multiply();

        return result.ToString();
    }

    private int GetBasinSize(char[,] heights, Point p)
    {
        var basinPoints = new HashSet<Point>();

        var next = new List<Point>() { p };

        while (next.Any())
        {
            basinPoints.AddRange(next);
            next = basinPoints.SelectMany(x => heights.GetNeighborPoints(x))
                              .Where(x => heights[x.point.X, x.point.Y] != '9')
                              .Where(p => !basinPoints.Contains(p.point))
                              .Select(p => p.point)
                              .ToList();
        }

        return basinPoints.Count;
    }

    private IEnumerable<Point> GetLowPoints(char[,] heights)
    {
        for (var x = 0; x <= heights.GetUpperBound(0); x++)
        {
            for (var y = 0; y <= heights.GetUpperBound(1); y++)
            {
                if (IsLowPoint(x, y, heights))
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}
