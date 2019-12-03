using System;
using System.Linq;
using Xunit;
using AdventOfCode2019.Solvers;

namespace AdventOfCode2019.Tests
{
    public class Day03
    {
        [Theory]
        [InlineData(new [] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" }, "159")]
        [InlineData(new [] { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" }, "135")]
        public void SamplePart1(string[] wires, string expectedDistance)
        {
            var solver = new Day03Solver();
            var distance = solver.SolvePart1(wires);
            Assert.Equal(expectedDistance, distance);
        }
        
        [Theory]
        [InlineData(new [] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" }, "610")]
        [InlineData(new [] { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" }, "410")]
        public void SamplePart2(string[] wires, string expectedSteps)
        {
            var solver = new Day03Solver();
            var steps = solver.SolvePart2(wires);
            Assert.Equal(expectedSteps, steps);
        }
    }
}
