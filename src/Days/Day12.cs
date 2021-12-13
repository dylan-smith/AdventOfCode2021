namespace AdventOfCode.Days;

[Day(2021, 12)]
public class Day12 : BaseDay
{
    private IDictionary<string, IEnumerable<string>> _next = new Dictionary<string, IEnumerable<string>>();

    public override string PartOne(string input)
    {
        //var paths_old = input.ParseLines(ParseLine).ToList();
        //var paths = new List<(string start, string end)>();

        //foreach (var p in paths_old)
        //{
        //    paths.Add(p);

        //    if (p.start != "start" && p.end != "end")
        //    {
        //        paths.Add((p.end, p.start));
        //    }
        //}

        //var next = paths.Where(path => path.start == "start").ToList();
        //var visited = new HashSet<string>();
        //visited.Add("start");

        //var result = next.Sum(x => CountPaths(x.end, paths, visited));

        //return result.ToString();
        return "";
    }

    private int CountPaths(string pos, IEnumerable<(string start, string end)> paths, Dictionary<string, int> visited)
    {
        if (visited.ContainsKey(pos) && pos.ToLower() == pos && pos != "start" && pos != "end")
        {
            if (visited.Any(x => x.Value > 1))
            {
                return 0;
            }

            visited[pos]++;
        }

        if (pos == "end")
        {
            var msg = "";

            foreach (var foo in visited)
            {
                msg += foo.Key + "[" + foo.Value.ToString() + "] ";
            }
            base.Log(msg);
            return 1;
        }

        var next = paths.Where(path => path.start == pos).ToList();

        if (!visited.ContainsKey(pos) && pos == pos.ToLower()) visited.Add(pos, 1);

        var result = next.Sum(x => CountPaths(x.end, paths, visited));

        if (visited.ContainsKey(pos)) visited[pos]--;

        return result;
    }

    private int CountPaths2(List<string> seen, IEnumerable<(string start, string end)> paths)
    {
        var pos = seen.Last();

        var result = 0;

        foreach (var n in _next[pos])
        {
            if (n == "end")
            {
                result++;
            }
            else
            {
                if (n.ToLower() != n || !seen.Contains(n) || NoDoubleSmall(seen))
                {
                    seen.Add(n);
                    result += CountPaths2(seen, paths);
                    seen.Remove(n);
                }
            }
        }

        return result;
    }

    private bool NoDoubleSmall(List<string> seen)
    {
        var max = seen.Where(x => x.ToLower() == x).GroupBy(x => x).Max(x => x.Count());

        return max <= 1;
    }

    private (string start, string end) ParseLine(string arg)
    {
        var start = arg.Split('-')[0];
        var end = arg.Split('-')[1];

        return (start, end);
    }

    public override string PartTwo(string input)
    {
        var paths_old = input.ParseLines(ParseLine).ToList();
        var paths = new List<(string start, string end)>();

        foreach (var p in paths_old)
        {
            paths.Add(p);
            paths.Add((p.end, p.start));
        }

        var caves = paths.Select(p => p.start).Distinct();

        foreach (var cave in caves)
        {
            _next.Add(cave, paths.Where(p => p.start == cave && p.end != "start").Select(p => p.end).ToList());
        }

        var seen = new List<string>();
        seen.Add("start");

        var result = CountPaths2(seen, paths);

        return result.ToString();
    }
}
