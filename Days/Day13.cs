using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Days
{
    public class Day13 : BaseDay
    {
        private List<(int, int)> ParseDots(string dotString) => dotString.Split(Environment.NewLine).Select(x =>
            {
                var coord = x.Split(',').Select(int.Parse);
                return (coord.Last(), coord.First());
            }).ToList();

        private List<(bool left, int line)> ParseFolds(string foldString) => foldString.Split(Environment.NewLine).Select(l =>
        {
            var parts = l.Split(' ').Last().Split('=');
            return (parts[0] == "x", int.Parse(parts[1]));
        }).ToList();

        private List<(int, int)> Fold(List<(int r, int c)> dots, int c, bool left = false)
        {
            var movingDots = dots.Where(d => left ? d.c > c : d.r > c).ToList();
            var stayingDots = dots.Where(d => left ? d.c < c : d.r < c).Except(movingDots).ToList();
            foreach (var mDot in movingDots)
            {
                var newDot = left ? (mDot.r, c - (mDot.c - c)) : (c - (mDot.r - c), mDot.c);
                if (!stayingDots.Contains(newDot))
                {
                    stayingDots.Add(newDot);
                }
            }
            return stayingDots;
        }

        private string PrintDotsList(List<(int, int)> dots)
        {
            var retString = Environment.NewLine;
            for (int r = 0; r <= dots.Max(d => d.Item1); r++)
            {
                for (int c = 0; c <= dots.Max(d => d.Item2); c++)
                {
                    retString += (dots.Contains((r, c)) ? '#' : '.');
                }
                retString += Environment.NewLine;
            }
            return retString;
        }

        public override string PartOne(string input)
        {
            var inputParts = input.Split($"{Environment.NewLine}{Environment.NewLine}");
            var dots = ParseDots(inputParts[0]);
            var fold = ParseFolds(inputParts[1]).First();

            dots = Fold(dots, fold.line, fold.left);

            return dots.Count.ToString();
        }

        public override string PartTwo(string input)
        {
            var inputParts = input.Split($"{Environment.NewLine}{Environment.NewLine}");
            var dots = ParseDots(inputParts[0]);
            var folds = ParseFolds(inputParts[1]);

            foreach (var fold in folds)
            {
                dots = Fold(dots, fold.line, fold.left);
            }
            return PrintDotsList(dots);
        }
    }
}