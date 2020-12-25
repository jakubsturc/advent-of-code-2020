using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day18
    {

        public static long Part1() => Input.ReadLines(day: 18).Select(Eval1).Sum();
        public static long Part2() => Input.ReadLines(day: 18).Select(Eval2).Sum();

        public static long Eval2(string str)
        {
            throw new NotImplementedException(); 
        }


        public static long Eval1(string str)
        {
            int len = str.Length;
            int idx = 0;
            return E();

            long E()
            {
                long acc = 0;
                Func<long, long, long> op = A;

                while (idx < len)
                {
                    char c = str[idx];
                    idx += 1;
                    switch (c)
                    {
                        case ' ': continue;
                        case '+': op = A; continue;
                        case '*': op = M; continue;
                        case '(': acc = op(acc, E()); continue;
                        case ')': return acc;
                        default: acc = op(acc, c - '0'); continue;
                    }                    
                }

                return acc;                
            }

            static long A(long a, long b) => a + b;
            static long M(long a, long b) => a * b;

        }



        public class Tests
        {
            [Theory]
            [InlineData("1 + 2 * 3 + 4 * 5 + 6", 71)]
            [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
            [InlineData("2 * 3 + (4 * 5)", 26)]
            [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
            [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
            [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
            public void Part1_Samples(string expression, long result)
            {
                Assert.Equal(result, Eval1(expression));
            }

            [Theory]
            [InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
            [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
            [InlineData("2 * 3 + (4 * 5)", 46)]
            [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
            [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
            [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
            public void Part2_Samples(string expression, long result)
            {
                Assert.Equal(result, Eval2(expression));
            }
        }
    }
}
