using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Solvers
{
    public class Day09Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var machine = new IntCodeMachine64(inputs.First());
            
            var input = new BlockingCollection<long>(new ConcurrentQueue<long>(new[] { 1L }));
            var output = new BlockingCollection<long>();
            
            machine.Execute(input, output);

            return string.Join(',', output);
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var machine = new IntCodeMachine64(inputs.First());
            
            var input = new BlockingCollection<long>(new ConcurrentQueue<long>(new[] { 2L }));
            var output = new BlockingCollection<long>();
            
            machine.Execute(input, output);

            return string.Join(',', output);
        }
    }
}