using System;
using System.Linq;

namespace AoC2021.Days
{
    public class Day6 : BaseDay
    {
        public long[] ParseInput(string input)
        {
            long[] fishCounts = new long[9];
            var fishes = input.Split(',').Select(int.Parse).ToArray();
            for (int i = 0; i < fishCounts.Length; i++)
            {
                fishCounts[i] = fishes.Count(f => f == i);
            }
            return fishCounts;
        }

        public long SimulateLanternFish(int days, long[] fishCounts)
        {
            for (int i = 0; i < days; i++)
            {
                var tmp = fishCounts[0];
                for (int j = 1; j < fishCounts.Length; j++)
                {
                    fishCounts[j - 1] = fishCounts[j];
                }
                fishCounts[6] += tmp;
                fishCounts[8] = tmp;
            }
            return fishCounts.Sum();
        }
        public override string PartOne(string input)
        {
            return SimulateLanternFish(80, ParseInput(input)).ToString();
        }

        public override string PartTwo(string input)
        {
            return SimulateLanternFish(256, ParseInput(input)).ToString();
        }
    }
}