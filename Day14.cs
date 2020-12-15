using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day14
    {
        public static long Part1() => Part1(Input.ReadLines(day: 14));

        public static long Part1(string[] lines) => Program.Parse(lines).Run().Memory.Sum;

        public record Program(IEnumerable<Instruction> Instructions)
        {
            public MachineState Run() => Instructions.Aggregate
            (
                seed: new MachineState(new Mask(0L, 0L), Memory.Empty),
                func: (acc, inst) => acc.Apply(inst)
            );

            public static Program Parse(string[] lines)
            {
                return new Program(ParseInstructions());

                IEnumerable<Instruction> ParseInstructions()
                {
                    foreach (var line in lines)
                    {
                        yield return line[0..3] switch
                        {
                            "mem" => UpdateInstruction.Parse(line),
                            "mas" => SetMaskInstruction.Parse(line),
                            _ => throw new NotSupportedException()
                        };
                    }
                }
            }
        }

        public interface Instruction { }

        public record UpdateInstruction(ulong Address, long Value) : Instruction
        {
            public static UpdateInstruction Parse(string line)
            {
                var matches = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");
                var address = ulong.Parse(matches.Groups[1].Value);
                var value = long.Parse(matches.Groups[2].Value);
                return new UpdateInstruction(address, value);
            }
        }

        public record SetMaskInstruction(Mask Mask) : Instruction
        {
            public static SetMaskInstruction Parse(string line)
            {
                long zeros = 0;
                long ones = 0;
                for (int i = 7; i < line.Length; i++)
                {
                    zeros <<= 1;
                    ones <<= 1;
                    switch (line[i])
                    {
                        case '0': zeros++; continue;
                        case '1': ones++; continue;
                        default: continue;
                    }
                }

                return new SetMaskInstruction(new Mask(zeros, ones));
            }
        }

        public record MachineState(Mask Mask, Memory Memory)
        {
            public MachineState Apply(Instruction instruction) => instruction switch
            {
                UpdateInstruction umi => this with { Memory = Memory.Update(umi.Address, Mask.Apply(umi.Value)) },
                SetMaskInstruction smi => this with { Mask = smi.Mask },
                _ => throw new NotSupportedException()
            };
        }

        public record Mask(long Zeros, long Ones)
        {
            public long Apply(long value) => (value & ~Zeros) | Ones;
        }

        public record Memory(ImmutableDictionary<ulong, long> Values)
        {
            public static readonly Memory Empty = new Memory(ImmutableDictionary.Create<ulong, long>());

            public long Sum { get => Values.Values.Sum(); }

            public Memory Update(ulong address, long value) => this with { Values = Values.SetItem(address, value) };
        }

        public class Tests
        {
            public static readonly string[] Sample1 = new string[]
            {
                "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
                "mem[8] = 11",
                "mem[7] = 101",
                "mem[8] = 0"
            };


            [Fact]
            public void Part1_Sample1()
            {
                Assert.Equal(165L, Part1(Sample1)); ;
            }
        }
    }
}
