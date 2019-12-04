using System.Collections.Generic;

namespace AdventOfCode2019.Solvers
{
    public interface ISolver
    {
         string SolvePart1(IEnumerable<string> inputs);
         string SolvePart2(IEnumerable<string> inputs);
    }
}