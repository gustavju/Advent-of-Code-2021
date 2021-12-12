using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Days
{
    public class Day10 : BaseDay
    {
        Dictionary<char, (char close, int points)> dic = new Dictionary<char, (char, int)>()
        {
            {'(', (')', 3)},
            {'[', (']', 57)},
            {'{', ('}', 1197)},
            {'<', ('>', 25137)},
        };
        Dictionary<char, int> dicP2 = new Dictionary<char, int>() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };

        public override string PartOne(string input)
        {
            var lines = input.Split(Environment.NewLine).ToArray();
            var ret = 0;
            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                //var ok = true;
                foreach (var c in line)
                {
                    if (dic.Keys.Contains(c))
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        var matchAgainst = stack.Peek();
                        if (dic[matchAgainst].close != c)
                        {
                            ret += dic.Where(d => d.Value.close == c).First().Value.points;
                            //ok = false;
                            break;
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                }
            }
            return $"{ret}";
        }

        public override string PartTwo(string input)
        {
            var lines = input.Split(Environment.NewLine).ToArray();
            var ret = 0;
            var scores = new List<long>();
            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                var ok = true;
                foreach (var c in line)
                {
                    if (dic.Keys.Contains(c))
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        var matchAgainst = stack.Peek();
                        if (dic[matchAgainst].close != c)
                        {
                            ret += dic.Where(d => d.Value.close == c).First().Value.points;
                            ok = false;
                            break;
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                }
                if (ok)
                {
                    long score = 0;
                    foreach (var cx in stack)
                    {
                        score = score * 5 + dicP2[cx];
                    }
                    scores.Add(score);
                }
            }
            scores.Sort();
            var res = scores[scores.Count / 2];
            return $"{res}";
        }
    }
}