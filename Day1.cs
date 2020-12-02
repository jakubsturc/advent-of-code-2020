using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day1
    {
        public static int[] ReadInput() => Input.Read(day: 1).Select(int.Parse).ToArray();

        public static int Part1()
        {
            var input = ReadInput();
            (_, var a, var b) = Part1(input);
            return a * b;
        }

        public static int Part2()
        {
            var input = ReadInput();
            (_, var a, var b, var c) = Part2(input);
            return a * b * c;
        }

        public static (bool, int, int) Part1(int[] nums)
        {
            var l = nums.Length;
            for (int i = 0; i < l; i++)
                for (int j = 0; j < i; j++)
                    if (nums[i] + nums[j] == 2020)
                        return (true, nums[i], nums[j]);
            return (false, 0, 0);
        }

        public static (bool, int, int, int) Part2(int[] nums)
        {
            var l = nums.Length;
            for (int i = 0; i < l; i++)
                for (int j = 0; j < i; j++)
                    for (int m = 0; m < j; m++)
                        if (nums[i] + nums[j] + nums[m] == 2020)
                            return (true, nums[i], nums[j], nums[m]);
            return (false, 0, 0, 0);
        }

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
}
