namespace AdventOfCode.Days;

[Day(2021, 4)]
public class Day04 : BaseDay
{
    public override string PartOne(string input)
    {
        var numbers = input.Lines().First().Integers().ToList();
        var boards = ParseBoards(input.Lines().Skip(1).ToList());

        foreach (var number in numbers)
        {
            boards.ForEach(x => MarkBoard(x, number));

            if (boards.Any(IsWinner))
            {
                var winner = boards.First(IsWinner);
                var score = ScoreBoard(winner, number);

                return score.ToString();
            }
        }

        return "NOT FOUND!";
    }

    public override string PartTwo(string input)
    {
        var numbers = input.Lines().First().Integers().ToList();
        var boards = ParseBoards(input.Lines().Skip(1).ToList());

        foreach (var number in numbers)
        {
            boards.ForEach(x => MarkBoard(x, number));

            if (boards.Count == 1 && IsWinner(boards.First()))
            {
                var score = ScoreBoard(boards.First(), number);

                return score.ToString();
            }

            boards = boards.Where(x => !IsWinner(x)).ToList();
        }

        return "NOT FOUND!";
    }

    private void MarkBoard(int[,] board, int number) => board.Replace(number, -1);

    private List<int[,]> ParseBoards(List<string> lines) => lines.Chunk(5).Select(ParseBoard).ToList();

    private int[,] ParseBoard(IEnumerable<string> lines)
    {
        var result = new int[5, 5];
        var row = 0;

        foreach (var line in lines)
        {
            result.SetRow(row++, line.Integers());
        }

        return result;
    }

    private bool IsWinner(int[,] board)
    {
        if (board.Rows().Any(x => x.Count(y => y == -1) == 5))
        {
            return true;
        }

        if (board.Cols().Any(x => x.Count(y => y == -1) == 5))
        {
            return true;
        }

        return false;
    }

    private int ScoreBoard(int[,] board, int number)
    {
        var score = board.ToList().Where(x => x > 0).Sum();

        return score * number;
    }
}
