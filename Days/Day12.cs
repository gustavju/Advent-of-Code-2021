using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Days
{
    public class Day12 : BaseDay
    {
        public override string PartOne(string input)
        {
            var paths = new Dictionary<string, List<string>>();
            var lines = input.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                var from = line.Split("-")[0].Trim();
                var to = line.Split("-")[1].Trim();
                if (!paths.ContainsKey(from))
                {
                    paths.Add(from, new List<string>());
                }
                if (!paths.ContainsKey(to))
                {
                    paths.Add(to, new List<string>());
                }
                Console.WriteLine(from + " " + to);
                paths[from].Add(to);
                paths[to].Add(from);
            }
            var current = "start";
            var queue = new Queue<(string place, HashSet<string> visited)>();
            queue.Enqueue((current, new HashSet<string>(new[] { current })));
            var posibilities = new List<HashSet<string>>();
            var ans = 0;
            while (queue.Count > 0)
            {
                var q = queue.Dequeue();
                if (q.place == "end")
                {
                    ans++;
                    Console.WriteLine(ans);
                    continue;
                }
                foreach (var next in paths[q.place])
                {
                    //Console.WriteLine(q.place + " -> " + next);
                    if (!q.visited.Contains(next))
                    {
                        var newVisited = new HashSet<string>(q.visited);
                        if (next.ToLower() == next)
                        {
                            newVisited.Add(next);
                        }
                        queue.Enqueue((next, newVisited));
                    }
                }
            }
            return ans.ToString();
        }

        public override string PartTwo(string input)
        {
            {
                var paths = new Dictionary<string, List<string>>();
                var lines = input.Split(Environment.NewLine);
                foreach (var line in lines)
                {
                    var from = line.Split("-")[0].Trim();
                    var to = line.Split("-")[1].Trim();
                    if (!paths.ContainsKey(from))
                    {
                        paths.Add(from, new List<string>());
                    }
                    if (!paths.ContainsKey(to))
                    {
                        paths.Add(to, new List<string>());
                    }
                    Console.WriteLine(from + " " + to);
                    paths[from].Add(to);
                    paths[to].Add(from);
                }
                var current = "start";
                var queue = new Queue<(string place, HashSet<string> visited, string twice)>();
                queue.Enqueue((current, new HashSet<string>(new[] { current }), string.Empty));
                var ans = 0;
                while (queue.Count > 0)
                {
                    var q = queue.Dequeue();
                    if (q.place == "end")
                    {
                        ans++;
                        Console.WriteLine(ans);
                        continue;
                    }
                    foreach (var next in paths[q.place])
                    {
                        //Console.WriteLine(q.place + " -> " + next);
                        if (!q.visited.Contains(next))
                        {
                            var newVisited = new HashSet<string>(q.visited);
                            if (next.ToLower() == next)
                            {
                                newVisited.Add(next);
                            }
                            queue.Enqueue((next, newVisited, q.twice));
                        }
                        else if (q.visited.Contains(next) && q.twice == string.Empty && next != "start" && next != "end")
                        {
                            queue.Enqueue((next, new HashSet<string>(q.visited), next));
                        }
                    }
                }
                return ans.ToString();
            }
        }
    }
}