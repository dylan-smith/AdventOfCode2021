using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
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

        private void MarkBoard(int[,] board, int number)
        {
            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    if (board[x, y] == number)
                    {
                        board[x, y] = -1;
                    }
                }
            }
        }

        private List<int[,]> ParseBoards(List<string> lines)
        {
            var boards = new List<int[,]>();

            while (lines.Any())
            {
                boards.Add(ParseBoard(lines.Take(5).ToList()));
                lines = lines.Skip(5).ToList();
            }

            return boards;
        }

        private int[,] ParseBoard(List<string> lines)
        {
            var result = new int[5, 5];
            var boardLines = lines.Take(5).ToList();

            for (var y = 0; y < 5; y++)
            {
                var line = boardLines[y].Integers().ToList();

                for (var x = 0; x < 5; x++)
                {
                    result[x, y] = line[x];
                }
            }

            return result;
        }

        private bool IsWinner(int[,] board)
        {
            for (var i = 0; i < 5; i++)
            {
                if (board[i, 0] == -1 && board[i, 1] == -1 && board[i, 2] == -1 && board[i, 3] == -1 && board[i, 4] == -1)
                {
                    return true;
                }

                if (board[0, i] == -1 && board[1, i] == -1 && board[2, i] == -1 && board[3, i] == -1 && board[4, i] == -1)
                {
                    return true;
                }
            }

            return false;
        }

        private int ScoreBoard(int[,] board, int number)
        {
            var score = 0;

            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    if (board[x, y] > 0)
                    {
                        score += board[x, y];
                    }
                }
            }

            return score * number;
        }
    }
}
