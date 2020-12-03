using System.Collections.Generic;
using System.IO;

namespace JakubSturc.AdventOfCode2020
{
    public static class Input
    {
        public static string[] Read(int day, int part = 1) => File.ReadAllLines($".\\Input\\d{day}p{part}.txt");
    }
}
