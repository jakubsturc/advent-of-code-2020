using System.Linq;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day6
    {
        public static int Part1()
        {
            return Input
                .ReadText(day: 6)
                .Split("\r\n\r\n")
                .Select(group => group.Split("\r\n"))
                .Select(lines => lines.Select(Encode))
                .Select(nums => nums.Aggregate((a, b) => a | b))
                .Select(BitCount)
                .Sum();
        }

        public static int Part2()
        {
            return Input
                .ReadText(day: 6)
                .Split("\r\n\r\n")
                .Select(group => group.Split("\r\n"))
                .Select(lines => lines.Select(Encode))
                .Select(nums => nums.Aggregate((a, b) => a & b))
                .Select(BitCount)
                .Sum(); 
        }

        public static uint Encode(string str)
        {
            uint res = 0;
            foreach (var c in str)
            {
                res |= 1u << c - 'a';
            }
            return res;
        }

        public static int BitCount(uint n)
        {
            int count = 0;
            while (n != 0)
            {
                count++;
                n &= (n - 1);
            }
            return count;
        }
    }
}
