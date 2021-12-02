namespace AdventOfCode.Days;

[Day(2021, 2)]
public class Day02 : BaseDay
{
    public override string PartOne(string input)
    {
        var commands = input.ParseLines(line => (instruction: line.Words().First(), 
                                                 value: long.Parse(line.Words().Last())));
        var hor = 0L;
        var depth = 0L;

        foreach (var (instruction, value) in commands)
        {
            switch (instruction)
            {
                case "forward":
                    hor += value;
                    break;
                case "down":
                    depth += value;
                    break;
                case "up":
                    depth -= value;
                    break;
            }
        }

        return (hor * depth).ToString();
    }

    public override string PartTwo(string input)
    {
        var commands = input.ParseLines(line => (instruction: line.Words().First(), 
                                                 value: long.Parse(line.Words().Last())));
        var hor = 0L;
        var depth = 0L;
        var aim = 0L;

        foreach (var (instruction, value) in commands)
        {
            switch (instruction)
            {
                case "forward":
                    hor += value;
                    depth += value * aim;
                    break;
                case "down":
                    aim += value;
                    break;
                case "up":
                    aim -= value;
                    break;
            }
        }

        return (hor * depth).ToString();
    }
}