using System.Drawing;

namespace AdventOfCode.Days;

[Day(2021, 15)]
public class Day15 : BaseDay
{
    private Dictionary<Point, long> _costs;
    private int[,] _grid;
    private long _maxCost = long.MaxValue;

    public override string PartOne(string input)
    {
        _grid = input.CreateIntGrid();

        var pos = new Point(_grid.Width() - 1, _grid.Height() - 1);
        _costs = new Dictionary<Point, long>();

        _maxCost = GetMaxCost();

        CalcCosts(pos, _grid[pos.X, pos.Y]);

        return (_costs[new Point(0, 0)] - _grid[0, 0]).ToString();
    }

    private long GetMaxCost()
    {
        var pos = new Point(0, 0);
        var end = new Point(_grid.Width() - 1, _grid.Height() - 1);
        var cost = 0L;

        while (pos != end)
        {
            var downCost = long.MaxValue;
            var rightCost = long.MaxValue;

            if ((pos.Y + 1) < _grid.Height())
            {
                downCost = _grid[pos.X, pos.Y + 1];
            }

            if ((pos.X + 1) < _grid.Width())
            {
                rightCost = _grid[pos.X + 1, pos.Y];
            }

            if (downCost <= rightCost)
            {
                pos = new Point(pos.X, pos.Y + 1);
                cost += downCost;
            }
            else
            {
                pos = new Point(pos.X + 1, pos.Y);
                cost += rightCost;
            }
        }

        return cost;
    }

    private void CalcCosts(Point pos, long cost)
    {
        if (cost >= _maxCost) return;
        if (_costs.ContainsKey(pos) && _costs[pos] <= cost) return;
        _costs.SafeSet(pos, cost);

        if (pos.X == 0 && pos.Y == 0)
        {
            if (cost < _maxCost)
            {
                base.Log($"Max Cost: {cost}");
                _maxCost = cost;
            }
        }

        var neighbors = _grid.GetNeighborPoints(pos).OrderBy(x => x.c + x.point.ManhattanDistance());

        foreach (var n in neighbors)
        {
            var ncost = cost + _grid[n.point.X, n.point.Y];

            CalcCosts(n.point, ncost);
        }
    }

    public override string PartTwo(string input)
    {
        var lines = input.Lines().ToList();
        var inputWidth = lines[0].Length;
        var inputHeight = lines.Count;
        _grid = new int[inputWidth * 5, inputHeight * 5];

        for (var y = 0; y < lines.Count; y++)
        {
            for (var x = 0; x < lines[0].Length; x++)
            {
                var startValue = int.Parse(lines[y][x].ToString());

                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 5; j++)
                    {
                        var tileX = x + (i * inputWidth);
                        var tileY = y + (j * inputHeight);

                        var tileValue = startValue + i + j;

                        if (tileValue > 9)
                        {
                            tileValue -= 9;
                        }

                        _grid[tileX, tileY] = tileValue;
                    }
                }
            }
        }

        var pos = new Point(_grid.Width() - 1, _grid.Height() - 1);
        _costs = new Dictionary<Point, long>();

        _maxCost = GetMaxCost();
        base.Log($"Max Cost: {_maxCost}");

        CalcCosts(pos, _grid[pos.X, pos.Y]);

        return (_costs[new Point(0, 0)] - _grid[0, 0]).ToString();
    }
}
