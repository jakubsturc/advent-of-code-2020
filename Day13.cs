using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day13
    {
        public static int Part1()
        {
            var lines = Input.ReadLines(day: 13);
            var start = int.Parse(lines[0]);
            var nums = lines[1].Split(',').Where(str => str != "x").Select(int.Parse).ToArray();

            for (var t = start; ; t++)
            {
                foreach (var n in nums)
                {
                    if (t % n == 0)
                    {
                        return n * (t - start);
                    }
                }
            }
        }
    }
}
