using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day5
    {
        public static uint Part1()
        {
            return Input.Read(day: 5).Select(Decode).Max();
        }

        public static uint Part2()
        {
            var seats = Input.Read(day: 5).Select(Decode).OrderBy(_=>_);
            uint last = 0;
            foreach (var seatId in seats)
            {
                if (seatId - last == 2) return seatId - 1;
                last = seatId;
            }
            
            throw new NotSupportedException();
        }

        public static uint Decode(string pass)
        {
            uint ret = 0;
            foreach (var c in pass)
            {
                ret = ret << 1;
                ret += c switch 
                { 
                    'B' or 'R' => 1u,
                    'F' or 'L' => 0u,
                    _ => throw new NotSupportedException()
                };

            }
            return ret;
        }

        public class Tests
        {
            [Theory]
            [InlineData("FBFBBFFRLR", 357)]
            [InlineData("BFFFBBFRRR", 567)]
            [InlineData("FFFBBBFRRR", 119)]
            [InlineData("BBFFBBFRLL", 820)]
            public void Decode_Samples(string pass, uint seatId)
            {
                Assert.Equal(seatId, Decode(pass));
            }
        }
    }
}
