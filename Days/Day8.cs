using System;
using System.Linq;

namespace AoC2021.Days
{
    public class Day8 : BaseDay
    {
        public override string PartOne(string input)
        {
            var lines = input.Split(Environment.NewLine).Select(l =>
            {
                var parts = l.Split(" | ");
                return new
                {
                    lefts = parts[0].Split(' ').Select(p => p.Trim()).ToArray(),
                    rights = parts[1].Split(' ').Select(p => p.Trim()).ToArray()
                };
            });
            return lines.SelectMany(i => i.rights).Count(c => c.Length == 2 || c.Length == 3 || c.Length == 4 || c.Length == 7).ToString();
        }

        public override string PartTwo(string input)
        {
            var lines = input.Split(Environment.NewLine).Select(l =>
            {
                var parts = l.Split(" | ");
                return new
                {
                    lefts = parts[0].Split(' ').Select(p => p.Trim()).ToArray(),
                    rights = parts[1].Split(' ').Select(p => p.Trim()).ToArray()
                };
            });
            var ans = 0;
            foreach (var line in lines)
            {
                // Dont need all of this but still ^^
                var one = line.lefts.Where(l => l.Length == 2).First();
                var seven = line.lefts.Where(l => l.Length == 3).First();
                var four = line.lefts.Where(l => l.Length == 4).First();

                var occurencesGrp = line.lefts.SelectMany(l => l).GroupBy(c => c);

                var a = seven.ToCharArray().Except(one.ToCharArray()).First();
                var b = occurencesGrp.Where(g => g.Count() == 6).First().Key;
                var e = occurencesGrp.Where(g => g.Count() == 4).First().Key;
                var g = occurencesGrp.Where(g => g.Count() == 7).First().Key;
                var f = occurencesGrp.Where(g => g.Count() == 9).First().Key;
                var c = one.ToCharArray().Except(new[] { f }).First();
                var d = four.ToCharArray().Except(new[] { b, c, f }).First();

                var translation = "";
                foreach (var right in line.rights)
                {
                    switch (right.Length)
                    {
                        case 6:
                            if (!right.Contains(d))
                                translation += 0;
                            else if (!right.Contains(e))
                                translation += 9;
                            else
                                translation += 6;
                            break;
                        case 2:
                            translation += 1;
                            break;
                        case 5:
                            if (right.Contains(e))
                                translation += 2;
                            else if (right.Contains(b))
                                translation += 5;
                            else
                                translation += 3;
                            break;
                        case 4:
                            translation += 4;
                            break;
                        case 3:
                            translation += 7;
                            break;
                        case 7:
                            translation += 8;
                            break;
                    }
                }
                ans += int.Parse(translation);
            }
            return ans.ToString();
        }
    }
}