using System;
using System.Collections.Generic;
using System.Linq;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day8
    {
        public static long Part1()
        {
            var instructions = Input.ReadLines(day: 8).Select(Parse).ToArray();
            var console = new Console(instructions);
            var init = new MachineState(0, 0);
            var lines = new HashSet<int>();
            var lastAcc = init.Acc;

            foreach (var state in console.Run(init))
            {
                var ip = state.Ip;
                if (!lines.Add(ip))
                {
                    return lastAcc;
                }
                lastAcc = state.Acc;
            }

            throw new NotSupportedException("☠☠☠ Kernel panic. ☠☠☠");
        }

        public static long Part2()
        {
            var instructions = Input.ReadLines(day: 8).Select(Parse).ToArray();
            var init = new MachineState(0, 0);

            for (int i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i];
                if (instruction is JmpInstruction)
                {
                    instructions[i] = new NopInstruction(0);
                    var console = new Console(instructions);
                    var states = console.Run(init).Take(instructions.Length + 1).ToList();

                    if (states.Count <= instructions.Length)
                    {
                        return states.Last().Acc;
                    }

                    instructions[i] = instruction;
                }
            }

            throw new NotSupportedException("☠☠☠ Kernel panic. ☠☠☠");
        }

        public static Instruction Parse(string line)
        {
            var arg = int.Parse(line[4..]);
            return line[0..3] switch
            { 
                "nop" => new NopInstruction(arg),
                "acc" => new AccInstruction(arg),
                "jmp" => new JmpInstruction(arg),
                _ => throw new NotSupportedException()
            };
        }
        
        public record Console(Instruction[] Instructions)
        {
            public IEnumerable<MachineState> Run(MachineState initState)
            {
                var state = initState;
                while (state.Ip < Instructions.Length)
                {
                    state = Instructions[state.Ip].Execute(state);
                    yield return state;
                }
            }
        }

        public record MachineState(int Ip, long Acc);

        public record NopInstruction(int Arg) : Instruction
        {
            public MachineState Execute(MachineState state) => state with { Ip = state.Ip + 1 };
        }

        public record AccInstruction(int Arg) : Instruction
        {
            public MachineState Execute(MachineState state) => new MachineState(state.Ip + 1, state.Acc + Arg);
        }

        public record JmpInstruction(int Arg) : Instruction
        {
            public MachineState Execute(MachineState state) => new MachineState(state.Ip + Arg, state.Acc);
        }

        public interface Instruction
        {
            public MachineState Execute(MachineState state);
        }
    }
}
