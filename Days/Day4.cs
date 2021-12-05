using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Days
{
    public class Day4 : BaseDay
    {
        class Board
        {
            public Cell[][] cells;
            public int score;
            public Board(string boardStr)
            {
                // Nasty, but works ;_;
                this.cells = boardStr.Split(Environment.NewLine)
                .Select(row => row.Split(' ').Where(cellVal => cellVal != "")
                .Select(cellVal => new Cell(int.Parse(cellVal.Trim()))).ToArray()).ToArray();
            }
            public Cell[] GetRow(int row) => cells[row];
            public Cell[] GetColumn(int col) => cells.Select(row => row[col]).ToArray();
            public Cell[] GetAllCells() => cells.SelectMany(row => row).ToArray();
            public void MarkNumber(int number)
            {
                GetAllCells().Where(cell => cell.Number == number).ToList().ForEach(cell => cell.Marked = true);
                if (HasBingo())
                {
                    score = SumUnmarkedNumbers() * number;
                }
            }
            public int SumUnmarkedNumbers() => GetAllCells().Where(cell => !cell.Marked).Sum(cell => cell.Number);
            public bool HasBingo()
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (GetRow(i).All(cell => cell.Marked) || GetColumn(i).All(cell => cell.Marked))
                        return true;
                }
                return false;
            }
        }
        class Cell
        {
            public int Number { get; set; }
            public bool Marked { get; set; }
            public Cell(int number)
            {
                Number = number;
                Marked = false;
            }
        }

        IEnumerable<Board> GetWinningBoards(Board[] boards, int[] numbers)
        {
            var winningBoards = new List<Board>();
            foreach (var number in numbers)
            {
                foreach (var board in boards.Where(b => b.score == 0))
                {
                    board.MarkNumber(number);
                    if (board.score != 0)
                        yield return board;
                }
            }
        }


        public override string PartOne(string input)
        {
            var parts = input.Split($"{Environment.NewLine}{Environment.NewLine}");
            var numbers = parts[0].Split(',').Select(int.Parse).ToArray();
            var boards = parts.Skip(1).Select(boardStr => new Board(boardStr)).ToArray();

            return GetWinningBoards(boards, numbers).First().score.ToString();
        }

        public override string PartTwo(string input)
        {
            var parts = input.Split($"{Environment.NewLine}{Environment.NewLine}");
            var numbers = parts[0].Split(',').Select(int.Parse).ToArray();
            var boards = parts.Skip(1).Select(boardStr => new Board(boardStr)).ToArray();

            return GetWinningBoards(boards, numbers).Last().score.ToString();
        }
    }
}