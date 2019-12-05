using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Solvers
{
    public class Day05Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var program = inputs.First().Split(",")
                .Select(i => Convert.ToInt32(i))
                .ToArray();

            var input = new Queue<int>();
            input.Enqueue(1);

            var output = ExecuteProgram(program, input);

            if (output.Take(output.Count - 1).Any(o => o != 0))
            {
                throw new Exception("Diagnostic failed!");
            }

            return output.Last().ToString();
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var program = inputs.First().Split(",")
                .Select(i => Convert.ToInt32(i))
                .ToArray();

            var input = new Queue<int>();
            input.Enqueue(5);

            var output = ExecuteProgram(program, input);

            if (output.Take(output.Count - 1).Any(o => o != 0))
            {
                throw new Exception("Diagnostic failed!");
            }

            return output.Last().ToString();
        }

        public IReadOnlyCollection<int> ExecuteProgram(int[] program, Queue<int> input)
        {
            var memory = program.ToArray();

            return Execute(memory, input);
        }

        public IReadOnlyCollection<int> Execute(int[] memory, Queue<int> input)
        {
            var output = new List<int>();
            var pointer = 0;

            while (true)
            {
                var opcodeString = memory[pointer].ToString().PadLeft(5,'0');

                var opcode = opcodeString.Substring(opcodeString.Length - 2);
                var mode1 = opcodeString[2] == '0';
                var mode2 = opcodeString[1] == '0';
                var mode3 = opcodeString[0] == '0';
                
                switch (opcode)
                {
                    case "01": // add
                        memory[memory[pointer + 3]] = (mode1 ? memory[memory[pointer + 1]] : memory[pointer + 1]) +
                            (mode2 ? memory[memory[pointer + 2]] : memory[pointer + 2]);
                        pointer = pointer + 4;
                        break;
                    case "02": // multiply
                        memory[memory[pointer + 3]] = (mode1 ? memory[memory[pointer + 1]] : memory[pointer + 1]) *
                            (mode2 ? memory[memory[pointer + 2]] : memory[pointer + 2]);
                        pointer = pointer + 4;
                        break;
                    case "03": // input
                        memory[memory[pointer + 1]] = input.Dequeue();
                        pointer = pointer + 2;
                        break;
                    case "04": // output
                        output.Add(mode1 ? memory[memory[pointer + 1]] : memory[pointer + 1]);
                        pointer = pointer + 2;
                        break;
                    case "05": // jump-if-true
                        // if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        var test05 = mode1 ? memory[memory[pointer + 1]] : memory[pointer + 1];
                        if (test05 != 0)
                            pointer = mode2 ? memory[memory[pointer + 2]] : memory[pointer + 2];
                        else
                            pointer = pointer + 3;
                        break;
                    case "06": // jump-if-false
                        // if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        var test06 = mode1 ? memory[memory[pointer + 1]] : memory[pointer + 1];
                        if (test06 == 0)
                            pointer = mode2 ? memory[memory[pointer + 2]] : memory[pointer + 2];
                        else
                            pointer = pointer + 3;
                        break;
                    case "07": // less than
                        // if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        var test07 = (mode1 ? memory[memory[pointer + 1]] : memory[pointer + 1]) < (mode2 ? memory[memory[pointer + 2]] : memory[pointer + 2]);
                        memory[memory[pointer + 3]] = test07 ? 1 : 0;
                        pointer = pointer + 4;
                        break;
                    case "08": // equals
                        // if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        var test08 = (mode1 ? memory[memory[pointer + 1]] : memory[pointer + 1]) == (mode2 ? memory[memory[pointer + 2]] : memory[pointer + 2]);
                        memory[memory[pointer + 3]] = test08 ? 1 : 0;
                        pointer = pointer + 4;
                        break;
                    case "99": // halt
                        return output;
                    default:
                        throw new Exception($"Invalid opcode: {opcode}");
                }
            }
        }
    }
}