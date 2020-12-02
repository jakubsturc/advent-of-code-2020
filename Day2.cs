using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day2
    {
        public static int Part1() => Input.Read(day: 2, part: 1).Where(IsValid).Count();

        public static bool IsValid(string line)
        {
            var r = InputParser.Parse(line);
            var p = new Policy(r.C, r.Min, r.Max);
            return p.Validate(r.Psw);
        }

        public record Policy(char C, int Min, int Max)
        {
            public bool Validate(string s)
            {
                var cnt = 0;
                foreach (char c in s)
                {
                    if (c == C) cnt++;
                }

                return Min <= cnt && cnt <= Max;
            }            
        }

        public static class InputParser
        { 
            public static Result Parse(string str)
            {
                var m = Regex.Match(str, "([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");
                var min = int.Parse(m.Groups[1].Value);
                var max = int.Parse(m.Groups[2].Value);
                var c = m.Groups[3].Value[0];
                var psw = m.Groups[4].Value;
                return new Result(min, max, c, psw);
            }

            public record Result(int Min, int Max, char C, string Psw);
        }


        public class Tests
        {
            [Fact]
            public void Part1_Sample()
            {
                var p1 = new Policy('a', 1, 3);
                var p2 = new Policy('b', 1, 3);
                var p3 = new Policy('c', 2, 9);

                Assert.True(p1.Validate("abcde"));
                Assert.False(p2.Validate("cdefg"));
                Assert.True(p3.Validate("ccccccccc"));
            }

            [Fact]
            public void InputParser_Parse()
            {
                Assert.Equal(new InputParser.Result(1, 3, 'a', "abcde"), InputParser.Parse("1-3 a: abcde"));
                Assert.Equal(new InputParser.Result(1, 3, 'b', "cdefg"), InputParser.Parse("1-3 b: cdefg"));
                Assert.Equal(new InputParser.Result(2, 9, 'c', "cccccccc"), InputParser.Parse("2-9 c: cccccccc"));
            }
        }
    }
}
