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
            private readonly Dictionary<int, int> _numbers; // key: number, value: last index

            public int Last { get; private set; }

            public int Index { get; private set; }

            public Sequence(int[] starting)
            {
                _numbers = new Dictionary<int, int>();
                for (int i = 0; i < starting.Length - 1; i++)
                {
                    _numbers.Add(starting[i], i + 1);
                }
                Last = starting[^1];
                Index = starting.Length;
            }

            public int AddNext(int target)
            {
                while( Index < target)
                {
                    AddNext();
                }

                return Last;
            }

            public int AddNext() => Add(GetNext());

            private int Add(int next)
            {                
                _numbers[Last] = Index;
                Index += 1;
                return Last = next;
            }

            public int GetNext()
            {
                if (!_numbers.ContainsKey(Last))
                {
                    return 0;
                }
                else
                {
                    return Index - _numbers[Last];
                }
            }
        }

        public class Test
        {
            [Fact]
            public void Part1_Sample036()
            {
                var seq = new Sequence(new [] {0, 3, 6 });
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
