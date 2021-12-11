using System.Drawing;

namespace AdventOfCode.Days;

[Day(2021, 11)]
public class Day11 : BaseDay
{
    private long _flashes = 0L;

    public override string PartOne(string input)
    {
        var grid = CreateGrid(input);

        100.Times(() => Step(grid));

        return _flashes.ToString();
    }

    private bool Step(int[,] grid)
    {
        var flashes = new HashSet<Point>();

        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                grid[x, y]++;
            }
        }

        var highs = FindEnergizedOctopus(grid);

        foreach (var h in highs)
        {
            FlashOctopus(h, flashes, grid);
        }

        foreach (var p in flashes)
        {
            grid[p.X, p.Y] = 0;
        }

        if (flashes.Count == 100)
        {
            return true;
        }

        return false;
    }

    private void FlashOctopus(Point h, HashSet<Point> flashes, int[,] grid)
    {
        if (h.X < 0 || h.Y < 0 || h.X > 9 || h.Y > 9)
        {
            return;
        }

        if (flashes.Contains(h))
        {
            return;
        }

        flashes.Add(h);
        _flashes++;

        foreach (var n in h.GetNeighbors(true))
        {
            if (n.X >= 0 && n.X <= 9 && n.Y >= 0 && n.Y <= 9)
            {
                grid[n.X, n.Y]++;

                if (grid[n.X, n.Y] >= 10)
                {
                    FlashOctopus(n, flashes, grid);
                }
            }
        }
    }

    private IEnumerable<Point> FindEnergizedOctopus(int[,] grid)
    {
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                if (grid[x, y] >= 10)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }

    private int[,] CreateGrid(string input)
    {
        var result = new int[10, 10];

        var lines = input.Lines();

        var x = -1;
        var y = -1;

        foreach (var line in lines)
        {
            y++;
            x = -1;

            foreach (var c in line)
            {
                x++;
                result[x, y] = int.Parse(c.ToString());
            }
        }

        return result;
    }

    public override string PartTwo(string input)
    {
        var grid = CreateGrid(input);
        var result = false;
        var steps = 0;

        while (!result)
        {
            steps++;
            result = Step(grid);
        }

        return steps.ToString();
    }
}
