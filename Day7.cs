using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day7
    {
        public static int Part1()
        {
            return Part1(Input.ReadLines(day: 7));
        }

        public static int Part1(IEnumerable<string> lines)
        {
            var allRules = lines.Select(Rule.Parse).ToList();
            var reverseLookup = allRules
                .SelectMany(rule => rule.Inner.Keys.Select(color => (color, rule)))
                .ToLookup(item => item.color, item => item.rule.Outer);

            HashSet<BagColor> n, r;
            n = new HashSet<BagColor>() { new BagColor("shiny gold") };
            r = new HashSet<BagColor>();

            do
            {
                n = n.SelectMany(i => reverseLookup[i]).ToHashSet();
                n.ExceptWith(r);
                r.UnionWith(n);
            } while (n.Count > 0);

            return r.Count();            
        }

        public static int Part2()
        {
            return Part2(Input.ReadLines(day: 7));
        }

        public static int Part2(IEnumerable<string> lines)
        {
            Dictionary<BagColor, Rule> allRules = lines.Select(Rule.Parse).ToDictionary(rule => rule.Outer);

            return Count(allRules[new BagColor("shiny gold")]) - 1;

            int Count(Rule rule)
            {
                var ret = 1;
                foreach ((var color, var cnt) in allRules[rule.Outer].Inner)
                {
                    ret += cnt * Count(allRules[color]);
                }
                return ret;
            }
        }

        public static IEnumerable<BagColor> GetRelatedColors(BagColor targetColor, Rule[] rules) => rules.Where(rule => rule.Inner.ContainsKey(targetColor)).Select(rule => rule.Outer);

        public record Rule(BagColor Outer, Dictionary<BagColor, int> Inner)
        {
            private static readonly Dictionary<BagColor, int> NoOtherBags = new Dictionary<BagColor, int>(0);

            public static Rule Parse(string line)
            {
                var words = line.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                var outer = new BagColor($"{words[0]} {words[1]}");

                if (words[4] is "no") return new Rule(outer, NoOtherBags);

                var inner = new Dictionary<BagColor, int>();
                for (int i=4; i<words.Length; i+=4)
                {
                    inner.Add(new BagColor($"{words[i + 1]} {words[i + 2]}"), int.Parse(words[i]));
                }

                return new Rule(outer, inner);
            }
        }

        public record BagColor(string Name);

        public class Tests
        {
            private static string[] Sample1 = new string[] {
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags." };

            private static string[] Sample2 = new string[] {
                "shiny gold bags contain 2 dark red bags.",
                "dark red bags contain 2 dark orange bags.",
                "dark orange bags contain 2 dark yellow bags.",
                "dark yellow bags contain 2 dark green bags.",
                "dark green bags contain 2 dark blue bags.",
                "dark blue bags contain 2 dark violet bags.",
                "dark violet bags contain no other bags." };



            [Fact]
            public void Sample_Is_Parsing()
            {
                var i1 = "dotted black bags contain no other bags.";
                var r1 = Rule.Parse(i1);
                Assert.Equal("dotted black", r1.Outer.Name);
                Assert.Empty(r1.Inner);

                var i2 = "bright white bags contain 1 shiny gold bag.";
                var r2 = Rule.Parse(i2);
                Assert.Equal("bright white", r2.Outer.Name);
                Assert.Equal(1, r2.Inner[new BagColor("shiny gold")]);


                var i3 = "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.";
                var r3 = Rule.Parse(i3);
                Assert.Equal("vibrant plum", r3.Outer.Name);
                Assert.Equal(5, r3.Inner[new BagColor("faded blue")]);
                Assert.Equal(6, r3.Inner[new BagColor("dotted black")]);
            }

            [Fact]
            public void Sample1_Test()
            {
                Assert.Equal(4, Part1(Sample1));
            }

            [Fact]
            public void Sample2_Test()
            {
                Assert.Equal(126, Part2(Sample2));
            }
        }
    }
}
