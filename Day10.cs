using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day10
    {
        public static long Part1()
        {
            (var Δ1, var Δ3) = Part1(Input.ReadLines(day: 10).Select(int.Parse));
            return Δ1 * Δ3;
        }


        private static (int, int) Part1(IEnumerable<int> seq)
        {
            var ordered = seq.OrderBy(_ => _).ToArray();
            var zipped = ordered.Prepend(0).Zip(ordered.Append(ordered[^1]+3));
            var diffs = zipped.Select(t => t.Second - t.First);
            var grps = diffs.ToLookup(_ => _);
            return (grps[1].Count(), grps[3].Count());
        }
         

        public class Tests
        {
            [Fact]
            public void Part1_Sample1()
            {
                var seq = new int[] { 16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4 };
                (var Δ1, var Δ3) = Part1(seq);
                Assert.Equal(7, Δ1);
                Assert.Equal(5, Δ3);
            }

            [Fact]
            public void Part1_Sample2()
            {
                var seq = new int[] { 28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3 };
                (var Δ1, var Δ3) = Part1(seq);
                Assert.Equal(22, Δ1);
                Assert.Equal(10, Δ3);
            }
        }
    }
}
