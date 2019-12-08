using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdventOfCode2019.Solvers
{
    public class IntCodeMachine
    {
        private readonly int[] program;

        public IntCodeMachine(string program)
            : this(program.Split(',').Select(s => Convert.ToInt32(s)).ToArray())
        {
        }

        public IntCodeMachine(int[] program)
        {
            this.program = program;
        }

        public void Execute(BlockingCollection<int> input, BlockingCollection<int> output)
        {
            var memory = program.ToArray();
            var pointer = 0;

            while (true)
            {
                var opCodeAndModes = ExtractOpCodeAndModes(memory[pointer]);

                int Read(int i) => opCodeAndModes[i] == 0 ? memory[memory[pointer + i]] : memory[pointer + i];
                
                var opcode = opCodeAndModes[0];
                switch (opcode)
                {
                    case 1: // add
                        memory[memory[pointer + 3]] = Read(1) + Read(2);
                        pointer = pointer + 4;
                        break;
                    case 2: // multiply
                        memory[memory[pointer + 3]] = Read(1) * Read(2);
                        pointer = pointer + 4;
                        break;
                    case 3: // input
                        memory[memory[pointer + 1]] = input.Take();
                        pointer = pointer + 2;
                        break;
                    case 4: // output
                        output.Add(Read(1));
                        pointer = pointer + 2;
                        break;
                    case 5: // jump-if-true
                        // if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        pointer = Read(1) != 0 ? Read(2) : pointer + 3;
                        break;
                    case 6: // jump-if-false
                        // if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        pointer = Read(1) == 0 ? Read(2) : pointer + 3;
                        break;
                    case 7: // less than
                        // if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        memory[memory[pointer + 3]] = Read(1) < Read(2) ? 1 : 0;
                        pointer = pointer + 4;
                        break;
                    case 8: // equals
                        // if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        memory[memory[pointer + 3]] = Read(1) == Read(2) ? 1 : 0;
                        pointer = pointer + 4;
                        break;
                    case 99: // halt
                        return;
                    default:
                        throw new Exception($"Invalid opcode: {opcode}");
                }
            }
        }

        private int[] ExtractOpCodeAndModes(int fullOpCode)
        {
            var opcode = new int[4];

            opcode[0] = fullOpCode % 100;

            opcode[1] = (fullOpCode / 100) % 10;
            opcode[2] = (fullOpCode / 1000) % 10;
            opcode[3] = (fullOpCode / 10000) % 10;

            return opcode;
        }
    }
}