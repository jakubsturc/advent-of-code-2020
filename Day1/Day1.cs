namespace JakubSturc.AdventOfCode2020.Day1
{
    public static class Day1
    {
        public static int Part1()
        {
            var input = Input.ReadDay1Part1();
            (_, var a, var b) = Part1(input);
            return a * b;
        }

        public static int Part2()
        {
            var input = Input.ReadDay1Part1();
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
    }
}
