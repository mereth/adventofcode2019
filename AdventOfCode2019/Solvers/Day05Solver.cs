using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Solvers
{
    public class Day05Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var machine = new IntCodeMachine(inputs.First());
            
            var input = new BlockingCollection<int>(new ConcurrentQueue<int>(new[] { 1 }));
            var output = new BlockingCollection<int>();
            
            machine.Execute(input, output);

            if (output.Take(output.Count - 1).Any(o => o != 0))
            {
                throw new Exception("Diagnostic failed!");
            }

            return output.Last().ToString();
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var machine = new IntCodeMachine(inputs.First());

            var input = new BlockingCollection<int>(new ConcurrentQueue<int>(new[] { 5 }));
            var output = new BlockingCollection<int>();

            machine.Execute(input, output);

            if (output.Take(output.Count - 1).Any(o => o != 0))
            {
                throw new Exception("Diagnostic failed!");
            }

            return output.Last().ToString();
        }
    }
}