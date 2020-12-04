using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day4
    {
        public static int Part1()
        {
            var input = Input.Read(day: 4);
            var passports = Parser.Process(input);
            return passports.Where(pass => pass.HasRequiredFields()).Count();
        }

        public static int Part2()
        {
            var input = Input.Read(day: 4);
            var passports = Parser.Process(input);
            return passports.Where(pass => pass.HasRequiredFields() && pass.IsValid()).Count();
        }

        private class Parser
        {
            public static IEnumerable<Passport> Process(string[] lines)
            {
                // welcome to the allocation jungle
                foreach (var line in SplitByEmptyLine(lines))
                {
                    var parts = line.Split(' ');
                    var pairs = parts.Select(part => part.Split(':'));
                    var kvps = pairs.Select(pair => new KeyValuePair<string, string>(pair[0], pair[1]));
                    yield return new Passport(kvps);
                }

            }

            /// <summary>
            /// Splits text by empty lines. New lines are replaced with space,
            /// </summary>
            private static IEnumerable<string> SplitByEmptyLine(string[] lines)
            {
                var sb = new StringBuilder();

                foreach (var line in lines)
                {
                    if (line.Length == 0 && sb.Length > 0)
                    {
                        yield return sb.ToString();
                        sb.Clear();
                        continue;
                    }

                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }

                    sb.Append(line);
                }

                if (sb.Length > 0)
                {
                    yield return sb.ToString();
                }
            }
        }

        private class Passport
        {
            private Dictionary<string, string> _dict;

            public Passport(IEnumerable<KeyValuePair<string, string>> values)
            {
                _dict = new Dictionary<string, string>(values);
            }

            public bool IsValid()
            {
                return IsByrValid()
                    && IsIyrValid()
                    && IsEyrValid()
                    && IsHgtValid()
                    && IsHclValid()
                    && IsEclValid()
                    && IsPidValid();
            }

            private bool IsByrValid()
                => _dict.TryGetValue("byr", out var str)
                    && int.TryParse(str, out var num)
                    && 1920 <= num && num <= 2002;

            private bool IsIyrValid()
                => _dict.TryGetValue("iyr", out var str)
                    && int.TryParse(str, out var num)
                    && 2010 <= num && num <= 2020;

            private bool IsEyrValid()
                => _dict.TryGetValue("eyr", out var str)
                    && int.TryParse(str, out var num)
                    && 2020 <= num && num <= 2030;

            private bool IsHgtValid()
                => _dict.TryGetValue("hgt", out var str)
                    && ((str.EndsWith("cm")
                            && int.TryParse(str[0..^2], out var cms)
                            && 150 <= cms && cms <= 193)
                        || (str.EndsWith("in")
                            && int.TryParse(str[0..^2], out var ins)
                            && 59 <= ins && ins <= 76));



            private bool IsHclValid()
                => _dict.TryGetValue("hcl", out var str)
                   && str.StartsWith("#")
                   && str.Length == 7
                   && str[1..6].All(c => ('0' <= c && c <= '9') || ('a' <= c && c <= 'f'));

            private static readonly HashSet<string> ValidEcs = new HashSet<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            private bool IsEclValid()
                => _dict.TryGetValue("ecl", out var str)
                   && ValidEcs.Contains(str);

            private bool IsPidValid()
                => _dict.TryGetValue("pid", out var str)
                   && str.Length == 9
                   && str[1..6].All(c => '0' <= c && c <= '9');


            public bool HasRequiredFields()
            {
                return HasKey("byr")
                    && HasKey("iyr")
                    && HasKey("eyr")
                    && HasKey("hgt")
                    && HasKey("hcl")
                    && HasKey("ecl")
                    && HasKey("pid");
            }

            public bool HasKey(string key) => _dict.ContainsKey(key);

            public string this[string key] => _dict[key];
        }

        public class Tests
        {
            private readonly string[] Sample1 = new string[] {
                "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
                "byr:1937 iyr:2017 cid:147 hgt:183cm",
                "",
                "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
                "hcl:#cfa07d byr:1929",
                "",
                "hcl:#ae17e1 iyr:2013",
                "eyr:2024",
                "ecl:brn pid:760753108 byr:1931",
                "hgt:179cm",
                "",
                "hcl:#cfa07d eyr:2025 pid:166559648",
                "iyr:2011 ecl:brn hgt:59in" };

            private readonly string[] Sample2 = new string[] {
                "eyr:1972 cid:100",
                "hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
                "",
                "iyr:2019",
                "hcl:#602927 eyr:1967 hgt:170cm",
                "ecl:grn pid:012533040 byr:1946",
                "",
                "hcl:dab227 iyr:2012",
                "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
                "",
                "hgt:59cm ecl:zzz",
                "eyr:2038 hcl:74454a iyr:2023",
                "pid:3556412378 byr:2007" };

            private readonly string[] Sample3 = new string[] {
                "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
                "hcl:#623a2f",
                "",
                "eyr:2029 ecl:blu cid:129 byr:1989",
                "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
                "",
                "hcl:#888785",
                "hgt:164cm byr:2001 iyr:2015 cid:88",
                "pid:545766238 ecl:hzl",
                "eyr:2022",
                "",
                "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719" };

            [Fact]
            public void Sample1_Is_Parsing()
            {   
                var passports = Parser.Process(Sample1).ToList();

                Assert.Equal(4, passports.Count());
                Assert.Equal("147", passports[0]["cid"]);
                Assert.Equal("#cfa07d", passports[1]["hcl"]);
                Assert.Equal("2024", passports[2]["eyr"]);
                Assert.Equal("59in", passports[3]["hgt"]);
            }

            [Fact]
            public void Part1_Sample1_Works_As_Expected()
            {
                var passports = Parser.Process(Sample1).ToList();

                Assert.True(passports[0].HasRequiredFields());
                Assert.False(passports[1].HasRequiredFields());
                Assert.True(passports[2].HasRequiredFields());
                Assert.False(passports[3].HasRequiredFields()); 
            }

            [Fact]
            public void Part2_Sample2_All_Passwords_Are_Invalid()
            {
                var passports = Parser.Process(Sample2).ToList();

                Assert.True(passports.All(p => !p.IsValid()));
            }

            [Fact]
            public void Part2_Sample3_All_Passwords_Are_Valid()
            {
                var passports = Parser.Process(Sample3).ToList();

                Assert.True(passports.All(p => p.IsValid()));
            }
        }
    }
}
