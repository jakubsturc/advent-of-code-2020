using System;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day11
    {
        public static int Part1()
        {
            return Part1(Map.From(Input.ReadLines(day: 11))).Count('#');
        }

        public static int Part2()
        {
            return Part2(Map.From(Input.ReadLines(day: 11))).Count('#');
        }

        public static Map Part1(Map map)
        {
            Map prev, next;
            next = map;
            do
            {
                prev = next;
                next = next.Next1();
            }
            while (!next.Equals(prev));

            return next;
        }

        public static Map Part2(Map map)
        {
            Map prev, next;
            next = map;
            do
            {
                prev = next;
                next = next.Next2();
            }
            while (!next.Equals(prev));

            return next;
        }

        public class Map
        {
            private readonly char[,] _inner;
            private readonly int _w;
            private readonly int _h;

            private Map(char[,] inner)
            {
                _inner = inner;
                _w = inner.GetLength(0);
                _h = inner.GetLength(1);
            }

            public static Map From(string[] lines)
            {
                var h = lines.Length;
                var w = lines[0].Length;
                var a = new char[h, w];

                for (var r = 0; r < h; r++)
                {
                    for (var c = 0; c < w; c++)
                    {
                        a[r, c] = lines[r][c];
                    }
                }

                return new Map(a);
            }

            public int Count(char t)
            {
                var x = 0;
                Each(Match);
                return x;

                bool Match(int r, int c)
                {
                    x += this[r, c] == t ? 1 : 0;
                    return true;
                }
            }

            public Map Next1()
            {
                var a = new char[_h, _w];
                Each(Π);
                return new Map(a);

                bool Π(int r, int c)
                {
                    var n = CountNeighbours(r, c);
                    var x = this[r, c];
                    a[r, c] = x switch
                    {
                        'L' when n == 0 => '#', // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                        '#' when n >= 4 => 'L', // If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                        _ => x                  // Otherwise, the seat's state does not change.
                    };
                    return true;
                }
            }

            public Map Next2()
            {
                var a = new char[_h, _w];
                Each(Π);
                return new Map(a);

                bool Π(int r, int c)
                {
                    var n = CountVisible(r, c);
                    var x = this[r, c];
                    a[r, c] = x switch
                    {
                        'L' when n == 0 => '#',
                        '#' when n >= 5 => 'L',
                        _ => x
                    };
                    return true;
                }
            }

            private bool Each(Func<int, int, bool> f)
            {
                for (var r = 0; r < _h; r++)
                {
                    for (var c = 0; c < _w; c++)
                    {   
                        if (!f(r, c)) return false;
                    }
                }

                return true;
            }

            private int CountNeighbours(int r, int c)
            {
                return Ψ(r - 1, c - 1)
                     + Ψ(r - 1, c)
                     + Ψ(r - 1, c + 1)
                     + Ψ(r, c - 1)                     
                     + Ψ(r, c + 1)
                     + Ψ(r + 1, c - 1)
                     + Ψ(r + 1, c)
                     + Ψ(r + 1, c + 1);

                int Ψ(int r, int c) => this[r, c] == '#' ? 1 : 0;
            }

            private int CountVisible(int r, int c)
            {
                return Ψ(-1,-1)
                     + Ψ(-1, 0)
                     + Ψ(-1, 1)
                     + Ψ( 0,-1)
                     + Ψ( 0, 1)
                     + Ψ( 1,-1)
                     + Ψ( 1, 0)
                     + Ψ( 1, 1);

                int Ψ(int Δr, int Δc)
                {
                    var rr = r + Δr;
                    var cc = c + Δc;
                    
                    while (true)
                    { 
                        switch (this[rr,cc])
                        {
                            case 'X': return 0;
                            case 'L': return 0;
                            case '#': return 1;
                            default:
                                rr = rr + Δr;
                                cc = cc + Δc;
                                break;
                        }
                    }
                }
            }

            public char this[int r, int c]
            {
                get => (r, c) switch
                {
                    _ when r < 0 || c < 0 => 'X',
                    _ when r >= _h || c >= _w => 'X',
                    _ => _inner[r, c]
                };
            }

            public override bool Equals(object? obj)
            {
                if (obj is not Map other) return false;

                if (other._h != _h) return false;
                
                if (other._w != _w) return false;                

                return Each(Ξ);

                bool Ξ(int r, int c) => this[r, c] == other[r, c];
            }

            public override int GetHashCode()
            {
                int h = 0;

                Each(φ);

                return h;

                bool φ(int r, int c)
                {
                    unchecked 
                    {
                        h *= 7;
                        h += this[r, c];
                    }
                    return true;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (var r = 0; r < _h; r++)
                {
                    for (var c = 0; c < _w; c++)
                    {
                        sb.Append(this[r, c]);
                    }

                    if (r + 1 < _h) sb.AppendLine();
                }

                return sb.ToString();
            }

        }

        public class Tests
        {
            private static readonly string Sample1Step1 = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

            private static readonly string Sample1Step2 = @"#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##";

            private static readonly string Sample1Step3 = @"#.LL.L#.##
#LLLLLL.L#
L.L.L..L..
#LLL.LL.L#
#.LL.LL.LL
#.LLLL#.##
..L.L.....
#LLLLLLLL#
#.LLLLLL.L
#.#LLLL.##";

            private static readonly string Sample1StepL = @"#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##";

            [Fact]
            public void Sample1_Step1()
            {
                var lines = Sample1Step1.Split("\r\n");
                var next = Map.From(lines).Next1();
                Assert.Equal(Sample1Step2, next.ToString());
                next = next.Next1();
                Assert.Equal(Sample1Step3, next.ToString());
            }

            [Fact]
            public void Sample_Equals()
            {
                var map1 = Map.From(Sample1Step1.Split("\r\n"));
                var map2 = Map.From(Sample1Step2.Split("\r\n"));
                var mapL = Map.From(Sample1StepL.Split("\r\n"));

                Assert.Equal(map1, map1);
                Assert.Equal(map2, map2);
                Assert.Equal(mapL, mapL);
                Assert.NotEqual(map1, map2);
                Assert.NotEqual(map2, mapL);
                Assert.NotEqual(mapL, map1);
            }

            [Fact]
            public void Sample1_StepL()
            {
                var lines = Sample1Step1.Split("\r\n");
                var last = Part1(Map.From(lines));
                Assert.Equal(Sample1StepL, last.ToString());
                Assert.Equal(37, last.Count('#'));
            }

        }
    }
}
