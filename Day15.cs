using System.Collections.Generic;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day15
    {
        public static int Part1()
        {
            var seq = new Sequence(new[] { 8, 0, 17, 4, 1, 12 });
            return seq.AddNext(2020);
        }

        public static int Part2()
        {
            var seq = new Sequence(new[] { 8, 0, 17, 4, 1, 12 });
            return seq.AddNext(30000000);
        }

        public class Sequence
        {
            private readonly List<int> _numbers;

            public int Last { get => _numbers[^1]; }

            public Sequence(IEnumerable<int> starting)
            {
                _numbers = new List<int>(starting);
            }

            public int AddNext(int target)
            {
                while( _numbers.Count < target)
                {
                    AddNext();
                }

                return Last;
            }

            public int AddNext()
            {
                var next = GetNext();
                _numbers.Add(next);
                return next;
            }

            public int GetNext()
            {
                var len = _numbers.Count;
                var last = _numbers[len - 1];
                for (int i = len - 1; i > 0; i--)
                {
                    if (_numbers[i - 1] == last)
                    {
                        return len - i;
                    }                    
                }

                return 0;
            }
        }

        public class Test
        {
            [Fact]
            public void Part1_Sample036()
            {
                var seq = new Sequence(new [] {0, 3, 5 });
                Assert.Equal(0, seq.AddNext());
                Assert.Equal(3, seq.AddNext());
                Assert.Equal(3, seq.AddNext());
                Assert.Equal(1, seq.AddNext());
                Assert.Equal(0, seq.AddNext());
                Assert.Equal(4, seq.AddNext());
                Assert.Equal(0, seq.AddNext());
            }

            [Fact]
            public void Part1_Sample132()
            {
                var seq = new Sequence(new[] { 1, 3, 2 });
                Assert.Equal(1, seq.AddNext(2020));                
            }

            [Fact]
            public void Part1_Sample213()
            {
                var seq = new Sequence(new[] { 2, 1, 3 });
                Assert.Equal(10, seq.AddNext(2020));
            }

            [Fact]
            public void Part1_Sample123()
            {
                var seq = new Sequence(new[] { 1, 2, 3 });
                Assert.Equal(27, seq.AddNext(2020));
            }

            [Fact]
            public void Part1_Sample231()
            {
                var seq = new Sequence(new[] { 2, 3, 1 });
                Assert.Equal(78, seq.AddNext(2020));
            }

            [Fact]
            public void Part1_Sample321()
            {
                var seq = new Sequence(new[] { 3, 2, 1 });
                Assert.Equal(438, seq.AddNext(2020));
            }

            [Fact]
            public void Part1_Sample312()
            {
                var seq = new Sequence(new[] { 3, 1, 2 });
                Assert.Equal(1836, seq.AddNext(2020));
            }

        }

    }
}
