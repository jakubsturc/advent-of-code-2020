using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2020
{
    public static class Input
    {
        public static int[] ReadDay1Part1() 
            => File.ReadAllLines(@".\Input\d1p1.txt").Select(int.Parse).ToArray();
    }
}
