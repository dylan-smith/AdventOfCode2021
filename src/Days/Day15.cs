using System.Drawing;

namespace AdventOfCode.Days;

[Day(2021, 15)]
public class Day15 : BaseDay
{
    public override string PartOne(string input)
    {
        var grid = input.CreateIntGrid();
        var cost = FindPath(grid);

        return cost.ToString();
    }

    public override string PartTwo(string input)
    {
        var grid = BuildExpandedGrid(input);
        var cost = FindPath(grid);

        return cost.ToString();
    }

    private long FindPath(int[,] grid)
    {
        var costs = new Dictionary<Point, long>();
        grid.GetPoints().ForEach(p => costs.Add(p, long.MaxValue));

        var frontier = new PriorityQueue<Point, long>();
        frontier.Enqueue(new Point(0, 0), 0);
        costs[new Point(0, 0)] = 0;

        while (frontier.Count > 0)
        {
            var f = frontier.Dequeue();
            var neighbors = grid.GetNeighborPoints(f);

            foreach (var (point, cost) in neighbors)
            {
                var ncost = costs[f] + cost;

                if (ncost < costs[point])
                {
                    costs[point] = ncost;
                    frontier.Enqueue(point, ncost);
                }
            }
        }

        return costs[new Point(grid.Width() - 1, grid.Height() - 1)];
    }

    private int[,] BuildExpandedGrid(string input)
    {
        var lines = input.Lines().ToList();
        var inputWidth = lines[0].Length;
        var inputHeight = lines.Count;
        var result = new int[inputWidth * 5, inputHeight * 5];

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

                        result[tileX, tileY] = tileValue;
                    }
                }
            }
        }

        return result;
    }
}
