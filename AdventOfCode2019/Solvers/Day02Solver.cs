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

            // replace position 1 with the value 12 and replace position 2 with the value 2
            program[1] = 12;
            program[2] = 2;

            ExecuteProgram(program);

            return program[0].ToString();
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
                    var copyProgram = program.ToArray();
                    copyProgram[1] = noun;
                    copyProgram[2] = verb;

                    ExecuteProgram(copyProgram);

                    if (copyProgram[0] == 19690720)
                    {
                        return (100 * noun + verb).ToString();
                    }
                }
            }

            return "NAH!";
        }

        public void ExecuteProgram(int[] program)
        {
            var position = 0;

            while (true)
            {
                switch (program[position])
                {
                    case 1:
                        program[program[position + 3]] = program[program[position + 1]] + program[program[position + 2]];
                        position = position + 4;
                        break;
                    case 2:
                        program[program[position + 3]] = program[program[position + 1]] * program[program[position + 2]];
                        position = position + 4;
                        break;
                    case 99:
                        return;
                    default:
                        throw new Exception("Boom!");
                }
            }
        }
    }
}