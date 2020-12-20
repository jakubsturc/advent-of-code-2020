using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day16
    {
        public static long Part1()
        {
            return Part1(Input.ReadLines(day: 16));
        }

        public static long Part2()
        {
            return Part2(Input.ReadLines(day: 16));
        }

        public static long Part1(string[] lines)
        {
            (var rules, _, var nearbyTickets) = Parse(lines);

            var all = nearbyTickets.SelectMany(ticket => ticket.Values);
            var invalid = all.Where(number => DoesNotMatchAllRules(rules, number));
            return invalid.Sum();
        }

        public static long Part2(string[] lines)
        {
            throw new NotImplementedException();
        }


        private static bool AllMatchSomeRules(IEnumerable<Rule> rules, Ticket ticket)
            => ticket.Values.All(number => !DoesNotMatchAllRules(rules, number));

        private static bool DoesNotMatchAllRules(IEnumerable<Rule> rules, long number) 
            => rules.All(rule => !rule.Match(number));

        public static ParseResult Parse(string[] lines)
        {
            int step = 0;

            var rules = new List<Rule>();
            Ticket? myTicket = null;
            var nearbyTickets = new List<Ticket>();

            foreach (var line in lines)
            {
                switch(step)
                {
                    case 0:
                        if (line == String.Empty) step += 1;
                        else rules.AddRange(ParseRule(line));
                        continue;
                    case 1:
                        if (line == "your ticket:") step += 1; // skip while line is found
                        continue;
                    case 2:
                        myTicket = ParseTicket(line);
                        step += 1;
                        continue;
                    case 3:
                        if (line == "nearby tickets:") step += 1; // skip while line is found
                        continue;
                    case 4:
                        nearbyTickets.Add(ParseTicket(line));
                        continue;

                }
            }

            return new ParseResult(rules, myTicket!, nearbyTickets);

            IEnumerable<Rule> ParseRule(string line)
            {
                var matches = Regex.Match(line, @"(\w+\s?\w*): (\d+)-(\d+) or (\d+)-(\d+)");
                var name = matches.Groups[1].Value;
                
                var min1 = int.Parse(matches.Groups[2].Value);
                var max1 = int.Parse(matches.Groups[3].Value);
                yield return new Rule(name, min1, max1);
                
                var min2 = int.Parse(matches.Groups[4].Value);
                var max2 = int.Parse(matches.Groups[5].Value);
                yield return new Rule(name, min2, max2);
            }

            Ticket ParseTicket(string line) => new Ticket(line.Split(',').Select(long.Parse).ToArray());            
        }

        public record ParseResult(IList<Rule> Rules, Ticket MyTicket, IList<Ticket> NearbyTickets);
        
        public record Ticket(long[] Values);

        public record Rule(string Name, long Min, long Max)
        {
            public bool Match(long number) => Min <= number && number <= Max;
        }

        public class Tests
        {
            [Fact]
            public void Part1_Sample()
            {
                var sample = new string[] {
                    "class: 1-3 or 5-7",
                    "row: 6-11 or 33-44",
                    "seat: 13-40 or 45-50",
                    "",
                    "your ticket:",
                    "7,1,14",
                    "",
                    "nearby tickets:",
                    "7,3,47",
                    "40,4,50",
                    "55,2,20",
                    "38,6,12"};

                Assert.Equal(71, Part1(sample));
            }
        }
    }
}
