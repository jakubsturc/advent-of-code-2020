using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day3
    {
        public static int Part1()
        {
            var input = Input.Read(day: 3);
            var map = new Map(input);
            return Sloop(map, Δ: (row: 1, col: 3));
        }

        public static long Part2()
        {
            var input = Input.Read(day: 3);
            var map = new Map(input);
            var res = 1L;
            
            res *= Sloop(map, Δ: (row: 1, col: 1));
            res *= Sloop(map, Δ: (row: 1, col: 3));
            res *= Sloop(map, Δ: (row: 1, col: 5));
            res *= Sloop(map, Δ: (row: 1, col: 7));
            res *= Sloop(map, Δ: (row: 2, col: 1));

            return res;
        }

        public static int Sloop(Map map, (int row, int col) Δ)
        {
            var res = 0; // number of hit trees

            for (int row=0, col=0; row<map.Height; row+=Δ.row, col+= Δ.col)
            {
                if (map[row, col] == '#') res++;
            }

            return res;
        }

        public class Map
        {
            private readonly int _width;            
            private readonly string[] _lines;
            
            public int Height { get; }


            public Map(string[] lines)
            {
                _lines = lines;
                _width = _lines[0].Length;
                Height = _lines.Length;
            }

            public char this[int row, int col]
            {
                get => _lines[row][col % _width];
            }
        }
    }
}
