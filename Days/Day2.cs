using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Days
{
    public class Day2 : BaseDay
    {
        public override string PartOne(string input)
        {
            (int x, int y) position = (0, 0);
            var moves = input.Split(Environment.NewLine).Select(x =>
            {
                var parts = x.Split(" ");
                return new
                {
                    direction = parts[0],
                    value = int.Parse(parts[1])
                };
            });
            foreach (var move in moves)
            {
                switch (move.direction)
                {
                    case "up":
                        position.y += move.value;
                        break;
                    case "down":
                        position.y -= move.value;
                        break;
                    case "forward":
                        position.x += move.value;
                        break;
                    default:
                        break;
                }
            }
            return $"{Math.Abs(position.x) * Math.Abs(position.y)}";
        }
        public override string PartTwo(string input)
        {
            (int x, int y, int aim) position = (0, 0, 0);
            var moves = input.Split(Environment.NewLine).Select(x =>
            {
                var parts = x.Split(" ");
                return new
                {
                    direction = parts[0],
                    value = int.Parse(parts[1])
                };
            });
            foreach (var move in moves)
            {
                switch (move.direction)
                {
                    case "up":
                        position.aim -= move.value;
                        break;
                    case "down":
                        position.aim += move.value;
                        break;
                    case "forward":
                        position.x += move.value;
                        position.y += position.aim * move.value;
                        break;
                    default:
                        break;
                }
            }
            return $"{Math.Abs(position.x) * Math.Abs(position.y)}";
        }
    }
}