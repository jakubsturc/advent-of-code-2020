using System.IO;

namespace JakubSturc.AdventOfCode2020
{
    public static class Input
    {
        private static string GetPath(int day, int part) => $".\\Input\\d{day}p{part}.txt";

        public static string[] Read(int day, int part = 1) => File.ReadAllLines(GetPath(day, part));
        
        public static StreamReader Open(int day, int part = 1) => File.OpenText(GetPath(day, part));
    }
}
