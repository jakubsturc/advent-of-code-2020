using System.IO;

namespace JakubSturc.AdventOfCode2020
{
    public static class Input
    {
        private static string GetPath(int day, int part) => $".\\Input\\d{day}p{part}.txt";

        public static string[] ReadLines(int day, int part = 1) => File.ReadAllLines(GetPath(day, part));
        
        public static string ReadText(int day, int part = 1) => File.ReadAllText(GetPath(day, part));
    }
}
