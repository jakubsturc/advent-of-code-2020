using System;
using System.Linq;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2020.Day1
{
    public class Tests
    {
        [Fact]
        public void Part1_Sample()
        {
            var nums = new int[] { 1721, 979, 366, 299, 675, 1456 };
            (var r, var a, var b) = Day1.Part1(nums);

            Assert.True(r);
            Assert.Equal(2020, a + b);
            Assert.Equal(514579, a * b);
        }

        [Fact]
        public void Part2_Sample()
        {
            var nums = new int[] { 1721, 979, 366, 299, 675, 1456 };
            (var r, var a, var b, var c) = Day1.Part2(nums);

            Assert.True(r);
            Assert.Equal(2020, a + b + c);
            Assert.Equal(241861950, a * b * c);
        }
    }
}
