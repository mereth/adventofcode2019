using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Solvers
{
    public class IntCodeMachine64
    {
        private readonly long[] program;

        public IntCodeMachine64(string program)
            : this(program.Split(',').Select(s => Convert.ToInt64(s)).ToArray())
        {
        }

        public IntCodeMachine64(long[] program)
        {
            this.program = program;
        }

        public void Execute(BlockingCollection<long> input, BlockingCollection<long> output)
        {
            var memory = new ShittyPagedMemory64(256);
            memory.Load(program);
            
            long pointer = 0;
            long relative = pointer;

            while (true)
            {
                var opCodeAndModes = ExtractOpCodeAndModes(memory.Read(pointer));

                long Read(long i) => opCodeAndModes[i] == 0 ?
                    memory.Read(memory.Read(pointer + i)) :
                    opCodeAndModes[i] == 1 ?
                        memory.Read(pointer + i) :
                        memory.Read(relative + memory.Read(pointer + i));

                void Write(long i, long value) {
                    if (opCodeAndModes[i] == 0)
                        memory.Write(memory.Read(pointer + i), value);
                    else
                        memory.Write(relative + memory.Read(pointer + i), value);
                }
                
                var opcode = opCodeAndModes[0];
                switch (opcode)
                {
                    case 1: // add
                        Write(3, Read(1) + Read(2));
                        pointer = pointer + 4;
                        break;
                    case 2: // multiply
                        Write(3, Read(1) * Read(2));
                        pointer = pointer + 4;
                        break;
                    case 3: // input
                        var inputValue = input.Take();
                        Write(1, inputValue);
                        pointer = pointer + 2;
                        break;
                    case 4: // output
                        //Console.WriteLine(">" + Read(1));
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
                        Write(3, Read(1) < Read(2) ? 1 : 0);
                        pointer = pointer + 4;
                        break;
                    case 8: // equals
                        // if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        Write(3, Read(1) == Read(2) ? 1 : 0);
                        pointer = pointer + 4;
                        break;
                    case 9: // adjusts the relative base
                        relative = relative + Read(1);
                        pointer = pointer + 2;
                        break;
                    case 99: // halt
                        return;
                    default:
                        throw new Exception($"Invalid opcode: {opcode}");
                }
            }
        }

        private long[] ExtractOpCodeAndModes(long fullOpCode)
        {
            //Console.WriteLine("#" + fullOpCode);
            var opcode = new long[4];

            opcode[0] = fullOpCode % 100;

            opcode[1] = (fullOpCode / 100) % 10;
            opcode[2] = (fullOpCode / 1000) % 10;
            opcode[3] = (fullOpCode / 10000) % 10;

            return opcode;
        }

        private class ShittyPagedMemory64
        {
            private readonly long pageSize;
            private readonly Dictionary<long, long[]> pages;

            public ShittyPagedMemory64(long pageSize)
            {
                this.pageSize = pageSize;
                this.pages = new Dictionary<long, long[]>();
            }

            public long Read(long position)
            {
                var pageId = position / pageSize;
                if (!pages.TryGetValue(pageId, out var page))
                {
                    page = new long[pageSize];
                    pages[pageId] = page;
                }
                return page[position % pageSize];
            }
            
            public void Write(long position, long value)
            {
                var pageId = position / pageSize;
                if (!pages.TryGetValue(pageId, out var page))
                {
                    page = new long[pageSize];
                    pages[pageId] = page;
                }
                page[position % pageSize] = value;
            }

            public void Load(long[] array)
            {
                for (long i = 0; i < array.Length; i++)
                {
                    Write(i, array[i]);
                }
            }
        }
    }
}