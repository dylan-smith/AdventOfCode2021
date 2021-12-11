using System.Drawing;

namespace AdventOfCode.Days;

[Day(2021, 11)]
public class Day11 : BaseDay
{
    public override string PartOne(string input)
    {
        var grid = input.CreateIntGrid();
        var flashes = 0L;

        100.Times(() => flashes += Step(grid));

        return flashes.ToString();
    }

    private int Step(int[,] grid)
    {
        var flashes = new HashSet<Point>();

        grid.Increment();
        var highs = grid.GetPoints(p => grid[p.X, p.Y] >= 10).ToList();

        highs.ForEach(x => FlashOctopus(x, flashes, grid));
        flashes.ForEach(p => grid[p.X, p.Y] = 0);

        return flashes.Count;
    }

    private void FlashOctopus(Point h, HashSet<Point> flashes, int[,] grid)
    {
        if (flashes.Add(h))
        {
            foreach (var (point, _) in grid.GetNeighborPoints(h, true))
            {
                grid[point.X, point.Y]++;

                if (grid[point.X, point.Y] >= 10)
                {
                    FlashOctopus(point, flashes, grid);
                }
            }
        }
    }

    public override string PartTwo(string input)
    {
        var grid = input.CreateIntGrid();
        var steps = 1;

        while (Step(grid) != 100)
        {
            steps++;
        }

        return steps.ToString();
    }
}
