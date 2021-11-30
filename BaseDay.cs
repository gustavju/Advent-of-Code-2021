using System;

namespace AoC2021
{
    public abstract class BaseDay
    {
        public abstract string PartOne(string input);
        public abstract string PartTwo(string input);

        public Action<string> Log { get; set; }
    }
}