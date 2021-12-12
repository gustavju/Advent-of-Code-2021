using System;
using System.Linq;

namespace AoC2021.Days
{
    public class Day11 : BaseDay
    {
        int[][] grid;
        int flashes = 0;
        (int dr, int dc)[] directions = new (int, int)[8] {
            (-1, -1),   // NW
            (-1, 0),    // N
            (-1, 1),    // NE
            (0, -1),    // W
            (0, 1),     // E
            (1, -1),    // SW
            (1, 0),     // S
            (1, 1)      // SE
        };

        public void Flash(int r, int c)
        {
            flashes++;
            grid[r][c] = -1;
            foreach (var dir in directions)
            {
                var cc = c + dir.dc;
                var rr = r + dir.dr;
                if (0 > cc || cc >= grid[0].Length || 0 > rr || rr >= grid.Length || grid[rr][cc] == -1)
                {
                    continue;
                }
                grid[rr][cc]++;
                if (grid[rr][cc] >= 10)
                {
                    Flash(rr, cc);
                }
            }
        }

        public int OctopusSimulation(int iterations, bool part2)
        {
            for (int iteration = 0; iteration < iterations; iteration++)
            {
                for (int r = 0; r < grid.Length; r++)
                {
                    for (int c = 0; c < grid[0].Length; c++)
                    {
                        grid[r][c]++;
                    }
                }
                for (int r = 0; r < grid.Length; r++)
                {
                    for (int c = 0; c < grid[0].Length; c++)
                    {
                        if (grid[r][c] >= 10)
                        {
                            Flash(r, c);
                        }
                    }
                }
                var isAllFlashing = true;
                for (int r = 0; r < grid.Length; r++)
                {
                    for (int c = 0; c < grid[0].Length; c++)
                    {
                        if (grid[r][c] == -1)
                        {
                            grid[r][c] = 0;
                        }
                        else
                        {
                            isAllFlashing = false;
                        }
                    }
                }
                if (part2 && isAllFlashing)
                {
                    return iteration + 1;
                }
            }
            return flashes;
        }

        public override string PartOne(string input)
        {
            grid = input.Split(Environment.NewLine).Select(l => l.ToCharArray().Select(c => (int)c - (int)'0').ToArray()).ToArray();
            return OctopusSimulation(100, false).ToString();
        }

        public override string PartTwo(string input)
        {
            grid = input.Split(Environment.NewLine).Select(l => l.ToCharArray().Select(c => (int)c - (int)'0').ToArray()).ToArray();
            return OctopusSimulation(int.MaxValue, true).ToString();
        }
    }
}