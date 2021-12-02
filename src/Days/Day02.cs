namespace AdventOfCode.Days;

[Day(2021, 2)]
public class Day02 : BaseDay
{
    public override string PartOne(string input)
    {
        var commands = input.Lines();
        var hor = 0;
        var depth = 0;

        foreach (var command in commands)
        {
            var instruction = command.Words().First();
            var value = long.Parse(command.Words().Last());

            switch (instruction)
            {
                case "forward":
                    hor += int.Parse(command.Split(' ')[1]);
                    break;
                case "down":
                    depth += int.Parse(command.Split(' ')[1]);
                    break;
                case "up":
                    depth -= int.Parse(command.Split(' ')[1]);
                    break;
            }
        }

        return (hor * depth).ToString();
    }

    public override string PartTwo(string input)
    {
        var commands = input.Lines();
        var hor = 0;
        var depth = 0;
        var aim = 0;

        foreach (var command in commands)
        {
            if (command.Split(' ')[0] == "forward")
            {
                hor += int.Parse(command.Split(' ')[1]);
                depth += int.Parse(command.Split(' ')[1]) * aim;
            }

            if (command.Split(' ')[0] == "down")
            {
                aim += int.Parse(command.Split(' ')[1]);
            }

            if (command.Split(' ')[0] == "up")
            {
                aim -= int.Parse(command.Split(' ')[1]);
            }
        }

        return (hor * depth).ToString();
    }
}

