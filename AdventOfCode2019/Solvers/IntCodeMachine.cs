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
                        //Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} ~");
                        memory[memory[pointer + 1]] = input.Take();
                        //Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} <= {memory[memory[pointer + 1]]}");
                        pointer = pointer + 2;
                        break;
                    case "04": // output
                        //Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} => {(mode1 ? memory[memory[pointer + 1]] : memory[pointer + 1])}");
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
                        return;
                    default:
                        throw new Exception($"Invalid opcode: {opcode}");
                }
            }
        }
    }
}