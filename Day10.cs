using System;
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

        public static double Part2()
        {
            (var f8, var f7) = Part2(Input.ReadLines(day: 10).Select(int.Parse));
            return Math.Pow(8, f8) * Math.Pow(7, f7);
        }

        private static (int, int) Part1(IEnumerable<int> seq)
        {
            var ordered = seq.OrderBy(_ => _).ToArray();
            var zipped = ordered.Prepend(0).Zip(ordered.Append(ordered[^1]+3));
            var diffs = zipped.Select(t => t.Second - t.First);
            var grps = diffs.ToLookup(_ => _);
            return (grps[1].Count(), grps[3].Count());
        }

        private static (int, int) Part2(IEnumerable<int> seq)
        {
            var ordered = seq.OrderBy(_ => _).ToArray();
            var zipped = ordered.Prepend(0).Zip(ordered.Append(ordered[^1] + 3));
            var diffs = zipped.Select(t => t.Second - t.First);
            var parts = PartSizes(diffs).ToLookup(_ => _); ;
            return (parts[3].Count(), parts[4].Count());

            static IEnumerable<int> PartSizes(IEnumerable<int> seq)
            {
                var cnt = 0;
                foreach (var i in seq)
                {
                    switch (i)
                    {
                        case 1:
                            cnt++;
                            continue;
                        case 3: 
                            yield return cnt; 
                            cnt = 0;
                            continue;
                        default: throw new NotSupportedException();
                    }
                }
            }
        }


        public class Tests
        {
            private static int[] Sample1 = new int[] { 16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4 };
            private static int[] Sample2 = new int[] { 28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3 };

            [Fact]
            public void Part1_Sample1()
            {                
                (var Δ1, var Δ3) = Part1(Sample1);
                Assert.Equal(7, Δ1);
                Assert.Equal(5, Δ3);
            }

            [Fact]
            public void Part1_Sample2()
            {                
                (var Δ1, var Δ3) = Part1(Sample2);
                Assert.Equal(22, Δ1);
                Assert.Equal(10, Δ3);
            }

            [Fact]
            public void Part2_Sample1()
            {
                Assert.Equal((1, 0), Part2(Sample1));
            }

            [Fact]
            public void Part2_Sample2()
            {                
                Assert.Equal((1, 4), Part2(Sample2));
            }
        }
    }
}
