using System;
using System.Linq;

namespace AoC2021.Days
{
    public class Day7 : BaseDay
    {
        public int CalculateFuel(int[] crabs, bool part2 = false)
        {
            var min = int.MaxValue;
            for (int i = crabs.Min(); i < crabs.Max(); i++)
            {
                var spentFuel = 0;
                foreach (var crab in crabs)
                {
                    var moves = Math.Abs(crab - i);
                    if (part2)
                    {
                        spentFuel += moves * (moves + 1) / 2;
                    }
                    else
                    {
                        spentFuel += moves;
                    }
                }
                min = Math.Min(min, spentFuel);
            }
            return min;
        }
        public override string PartOne(string input)
        {
            var crabs = input.Split(',').Select(int.Parse).ToArray();
            return CalculateFuel(crabs).ToString();
        }

        public override string PartTwo(string input)
        {
            var crabs = input.Split(',').Select(int.Parse).ToArray();
            return CalculateFuel(crabs, true).ToString();
        }
    }
}