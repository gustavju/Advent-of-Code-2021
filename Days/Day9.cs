using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Days
{
    public class Day9 : BaseDay
    {
        int[] dr = new int[] { -1, 0, 1, 0 };
        int[] dc = new int[] { 0, 1, 0, -1 };
        (int dr, int dc)[] directions = new (int, int)[4] { (-1, 0), (0, 1), (1, 0), (0, -1) };

        private List<(int r, int c)> GetLowPoints(int[][] grid)
        {
            var lowPoints = new List<(int, int)>();
            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[0].Length; c++)
                {
                    var ok = true;
                    foreach (var d in directions)
                    {
                        var cc = c + d.dc;
                        var rr = r + d.dr;
                        if (0 > cc || cc >= grid[0].Length || 0 > rr || rr >= grid.Length)
                        {
                            continue;
                        }
                        if (grid[rr][cc] <= grid[r][c])
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (ok)
                    {
                        lowPoints.Add((r, c));
                    }
                }
            }
            return lowPoints;
        }

        public override string PartOne(string input)
        {
            var data = input.Split(Environment.NewLine).Select(l => l.ToCharArray().Select(c => (int)c - (int)'0').ToArray()).ToArray();
            var lowPoints = GetLowPoints(data);
            return lowPoints.Aggregate(0, (curr, next) => curr += 1 + data[next.r][next.c]).ToString();
        }

        public override string PartTwo(string input)
        {
            var data = input.Split(Environment.NewLine).Select(l => l.ToCharArray().Select(c => (int)c - (int)'0').ToArray()).ToArray();

            // print the 2d array data to the console
            // for (int r = 0; r < data.Length; r++)
            // {
            //     for (int c = 0; c < data[r].Length; c++)
            //     {
            //         Console.Write(data[r][c]);
            //     }
            //     Console.WriteLine();
            // }

            var lowPoints = GetLowPoints(data);
            var basins = new List<List<(int r, int c)>>();
            var seen = new HashSet<(int r, int c)>();
            foreach (var lowPoint in lowPoints)
            {
                var basin = new List<(int r, int c)>();
                var queue = new Queue<(int r, int c)>();
                queue.Enqueue(lowPoint);
                while (queue.Count > 0)
                {
                    var (r, c) = queue.Dequeue();
                    if (basin.Contains((r, c)) || data[r][c] == 9)
                    {
                        continue;
                    }
                    basin.Add((r, c));
                    foreach (var d in directions)
                    {
                        var cc = c + d.dc;
                        var rr = r + d.dr;
                        if (0 > cc || cc >= data[0].Length || 0 > rr || rr >= data.Length)
                        {
                            continue;
                        }
                        queue.Enqueue((rr, cc));
                    }
                }
                basins.Add(basin);
            }
            return basins.OrderByDescending(b => b.Count)
                            .Take(3)
                            .Aggregate(1, (curr, next) => curr *= next.Count)
                            .ToString();
        }
    }
}