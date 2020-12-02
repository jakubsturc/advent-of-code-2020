using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2020
{
    public static class Input
    {
        public static IEnumerable<string> Read(int day, int part = 1) => File.ReadAllLines($".\\Input\\d{day}p{part}.txt");
    }
}
