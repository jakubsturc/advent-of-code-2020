using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day9
    {
        public static long Part1()
        {
            var w = 25;
            var d = Input.ReadLines(day: 9).Select(long.Parse).ToArray();

            for (int i = w + 1; i < d.Length - w; i++)
            {
                if (!Validate(d[i], d.AsSpan((i - w)..i)))
                {
                    return d[i];
                }
            }

            throw new NotSupportedException("No answer found");

            static bool Validate(long x, Span<long> ps)
            {
                var l = ps.Length;
                for (int i = 0; i < l; i++)
                {
                    for (int j = 0; j < l; j++)
                    {
                        if (i == j) continue;
                        if (ps[i] + ps[j] == x) return true;
                    }
                }
                return false;
            }
        }

        public static long Part2()
        {
            var d = Input.ReadLines(day: 9).Select(long.Parse).ToArray();
            var r = 15353384L;
            return Part2(r, d);
        }

        public static long Part2(long r, long[] d)
        {
            var l = d.Length;
            var res = Intervals(l).First(rng => SumEquals(r, d.AsSpan(rng)));
            return d[res].Min() + d[res].Max();
        }

        private static bool SumEquals(long t, Span<long> s)
        {
            long r = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                r += s[i];
                if (r > t) return false;
            }
            
            return t == r;
        }

        private static IEnumerable<Range> Intervals(int l)
        {
            for (int i = l; i > 0; i--)
            {
                for (int j = 0; j + i <= l; j++)
                {
                    yield return j..(j + i);
                }
            }
        }

        public class Tests
        {
            [Fact]
            public void Part2_Sample()
            {
                var d = new long[] { 35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576 };
                var r = 127;
                Assert.Equal(62, Part2(r, d));
            }
        }
    }
}
