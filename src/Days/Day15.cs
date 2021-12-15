using System.Drawing;

namespace AdventOfCode.Days;

[Day(2021, 15)]
public class Day15 : BaseDay
{
    private Dictionary<Point, long> _costs;

    public override string PartOne(string input)
    {
        var grid = input.CreateIntGrid();

        var pos = new Point(grid.Width() - 1, grid.Height() - 1);
        _costs = new Dictionary<Point, long>();

        _costs.Add(pos, grid[pos.X, pos.Y]);

        CalcCosts(pos, grid);

        return (_costs[new Point(0, 0)] - grid[0, 0]).ToString();
    }

    private void CalcCosts(Point pos, int[,] grid)
    {
        var neighbors = grid.GetNeighborPoints(pos);
        var cost = _costs[pos];
        var end = new Point(grid.Width() - 1, grid.Height() - 1);

        foreach (var n in neighbors.OrderBy(x => x.point.ManhattanDistance(end)))
        {
            var ncost = cost + grid[n.point.X, n.point.Y];

            if (!_costs.ContainsKey(n.point))
            {
                _costs.Add(n.point, ncost);
                CalcCosts(n.point, grid);
            }
            else
            {
                if (_costs[n.point] > ncost)
                {
                    _costs[n.point] = ncost;
                    CalcCosts(n.point, grid);
                }
            }
        }
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
