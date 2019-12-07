using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Solvers
{
    public class Day07Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var machine = new IntCodeMachine(inputs.First());

            var maxThrusterSignal = 0;
            foreach (var phaseSettings in GetPhaseSettings(0, 1, 2, 3, 4))
            {
                var outputSignal = 0;
                foreach(var phaseSetting in phaseSettings)
                {
                    var input = new BlockingCollection<int>(new ConcurrentQueue<int>(new[] { phaseSetting, outputSignal }));
                    var output = new BlockingCollection<int>();
                    machine.Execute(input, output);
                    outputSignal = output.First();
                }

                if (outputSignal > maxThrusterSignal)
                    maxThrusterSignal = outputSignal;
            }

            return maxThrusterSignal.ToString();
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var machine = new IntCodeMachine(inputs.First());

            var maxThrusterSignal = 0;
            foreach (var phaseSettings in GetPhaseSettings(5, 6, 7, 8, 9))
            {

                var prevOutput = new BlockingCollection<int>();
                var firstInput = prevOutput;
                
                var amplifiers = phaseSettings.Select(phaseSetting =>
                {
                    var input = prevOutput;
                    input.Add(phaseSetting);

                    var output = phaseSetting != phaseSettings.Last() ? new BlockingCollection<int>() : firstInput;
                    prevOutput = output;

                    return Task.Factory.StartNew(() => machine.Execute(input, output));
                }).ToArray();

                firstInput.Add(0);

                Task.WaitAll(amplifiers);

                var outputSignal = firstInput.Take();

                if (outputSignal > maxThrusterSignal)
                    maxThrusterSignal = outputSignal;
            }

            return maxThrusterSignal.ToString();
        }

        private IEnumerable<int[]> GetPhaseSettings(params int[] inputs)
        {
            foreach (var i in inputs)
            {
                foreach (var j in inputs)
                {
                    if (j == i) continue;
                    foreach (var k in inputs)
                    {
                        if (k == i) continue;
                        if (k == j) continue;
                        foreach (var l in inputs)
                        {
                            if (l == i) continue;
                            if (l == j) continue;
                            if (l == k) continue;
                            foreach (var m in inputs)
                            {
                                if (m == i) continue;
                                if (m == j) continue;
                                if (m == k) continue;
                                if (m == l) continue;
                                yield return new int[] { i, j, k, l, m };
                            }
                        }
                    }
                }
            }
        }
    }
}