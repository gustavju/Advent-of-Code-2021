using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Days
{
    public class Day1 : BaseDay
    {
        private int largerThanPreviousNthItem(int[] depthMeasurements, int n)
        {
            var largerThanPreviousCount = 0;
            for (int i = 0; i < depthMeasurements.Length - n; i++)
            {
                if (depthMeasurements[i] < depthMeasurements[i + n])
                {
                    largerThanPreviousCount++;
                }
            }
            return largerThanPreviousCount;
        }
        public override string PartOne(string input)
        {
            var depthMeasurements = input.Split(Environment.NewLine).Select(int.Parse).ToArray();
            return largerThanPreviousNthItem(depthMeasurements, 1).ToString();
        }

        public override string PartTwo(string input)
        {
            var depthMeasurements = input.Split(Environment.NewLine).Select(int.Parse).ToArray();
            /* The sliding window is 3 elements long where the next window 
            * contains the last 2 elements of the previous window.
            * Therefore we only need to compare the first and last element.
            */
            return largerThanPreviousNthItem(depthMeasurements, 3).ToString();
        }
    }
}