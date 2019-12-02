using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Solvers
{
    public class Day02Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var program = inputs.First().Split(",")
                .Select(i => Convert.ToInt32(i))
                .ToArray();

            var ret = ExecuteProgram(program, 12, 2);

            return ret.ToString();
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var program = inputs.First().Split(",")
                .Select(i => Convert.ToInt32(i))
                .ToArray();

            // brute force
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var ret = ExecuteProgram(program, noun, verb);
                    if (ret == 19690720)
                    {
                        return (100 * noun + verb).ToString();
                    }
                }
            }

            throw new Exception("Brute force failed ?!");
        }

        public int ExecuteProgram(int[] program, int noun, int verb)
        {
            var memory = program.ToArray();

            memory[1] = noun;
            memory[2] = verb;

            Execute(memory);

            return memory[0];
        }

        public void Execute(int[] memory)
        {
            var pointer = 0;

            while (true)
            {
                var opcode = memory[pointer];
                switch (opcode)
                {
                    case 1: // add
                        memory[memory[pointer + 3]] = memory[memory[pointer + 1]] + memory[memory[pointer + 2]];
                        pointer = pointer + 4;
                        break;
                    case 2: // multiply
                        memory[memory[pointer + 3]] = memory[memory[pointer + 1]] * memory[memory[pointer + 2]];
                        pointer = pointer + 4;
                        break;
                    case 99: // halt
                        return;
                    default:
                        throw new Exception($"Invalid opcode: {opcode}");
                }
            }
        }
    }
}