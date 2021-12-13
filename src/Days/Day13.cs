using System.Drawing;

namespace AdventOfCode.Days;

[Day(2021, 13)]
public class Day13 : BaseDay
{
    public override string PartOne(string input)
    {
        var dots = input.Lines().Where(x => x.Contains(",")).Select(ParseDot).ToList();
        var folds = input.Lines().Where(x => x.StartsWith("fold")).Select(ParseFold).ToList();

        dots = Fold(dots, folds.First());
        
        return dots.Distinct().Count().ToString();
    }

    private List<Point> Fold(List<Point> dots, (string axis, int value) fold)
    {
        if (fold.axis == "y")
        {
            return FoldUp(dots, fold.value);
        }
        else
        {
            return FoldLeft(dots, fold.value);
        }
    }

    private List<Point> FoldLeft(List<Point> dots, int value)
    {
        for (var i = 0; i < dots.Count; i++)
        {
            if (dots[i].X > value)
            {
                dots[i] = new Point(dots[i].X - (2 * (dots[i].X - value)), dots[i].Y);
            }
        }

        return dots;
    }

    private List<Point> FoldUp(List<Point> dots, int value)
    {
        for (var i = 0; i < dots.Count; i++)
        {
            if (dots[i].Y > value)
            {
                dots[i] = new Point(dots[i].X, dots[i].Y - (2 * (dots[i].Y - value)));
            }
        }

        return dots;
    }

    private (string axis, int value) ParseFold(string line)
    {
        var axis = line.Words().Last().Split("=").First();
        var value = int.Parse(line.Words().Last().Split("=").Last());

        return (axis, value);
    }

    private Point ParseDot(string line)
    {
        return new Point(line.Integers().First(), line.Integers().Last());
    }

    public override string PartTwo(string input)
    {
        var dots = input.Lines().Where(x => x.Contains(",")).Select(ParseDot).ToList();
        var folds = input.Lines().Where(x => x.StartsWith("fold")).Select(ParseFold).ToList();

        folds.ForEach(fold => Fold(dots, fold));

        var width = dots.Max(dot => dot.X) - dots.Min(dot => dot.X);
        var height = dots.Max(dot => dot.Y) - dots.Min(dot => dot.Y);
        var xOffset = 0 - dots.Min(dot => dot.X);
        var yOffset = 0 - dots.Min(dot => dot.Y);

        var filePath = "C:\\git\\AoCDay13.bmp";
        ImageHelper.CreateBitmap(width + 1, height + 1, filePath, (x, y) => dots.Any(dot => dot.X == x - xOffset && dot.Y == y - yOffset) ? Color.Black : Color.White);

        return filePath;
    }
}