using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Days
{
    public class Day5 : BaseDay
    {
        public class Line
        {
            public int x1 { get; set; }
            public int y1 { get; set; }
            public int x2 { get; set; }
            public int y2 { get; set; }
            public Line(int x1, int y1, int x2, int y2)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }
        }

        public List<Line> parseLines(string input)
        {
            return input.Split(Environment.NewLine).Select(lines =>
            {
                var parts = lines.Split(" -> ");
                var p1 = parts[0].Split(",");
                var p2 = parts[1].Split(",");
                return new Line(int.Parse(p1[0]), int.Parse(p1[1]), int.Parse(p2[0]), int.Parse(p2[1]));
            }).ToList();
        }

        private int getDangerPointCount(List<Line> lines, bool countDiagonals = false)
        {
            var seenPoints = new Dictionary<(int x, int y), int>();

            foreach (var line in lines)
            {
                var dx = line.x2 - line.x1;
                var dy = line.y2 - line.y1;

                for (int i = 0; i <= Math.Max(Math.Abs(dx), Math.Abs(dy)); i++)
                {
                    var xi = dx.CompareTo(0); // -1, 0, 1 
                    var yi = dy.CompareTo(0);

                    var x = line.x1 + xi * i;
                    var y = line.y1 + yi * i;
                    if (!seenPoints.ContainsKey((x, y)))
                    {
                        seenPoints.Add((x, y), 0);
                    }
                    seenPoints[(x, y)] += 1;
                }
            }
            return seenPoints.Count(s => s.Value > 1);
        }

        public override string PartOne(string input)
        {
            var straightLines = parseLines(input).Where(line => (line.x1 == line.x2 || line.y1 == line.y2)).ToList();
            return getDangerPointCount(straightLines).ToString();
        }

        public override string PartTwo(string input)
        {
            var lines = parseLines(input);
            return getDangerPointCount(lines).ToString();
        }

    }
}