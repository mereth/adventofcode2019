using System;
using Xunit;
using AdventOfCode2019.Solvers;

namespace AdventOfCode2019.Tests
{
    public class Day01
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void SamplePart1(int moduleMass, int expectedFuelMass)
        {
            var solver = new Day01Solver();
            var fuelMass = solver.ComputeFuelMass(moduleMass);
            Assert.Equal(fuelMass, expectedFuelMass);
        }
        
        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void SamplePart2(int moduleMass, int expectedFuelMass)
        {
            var solver = new Day01Solver();
            var fuelMass = solver.ComputeFuelMass(moduleMass);
            var additionalFuelMass = solver.ComputeAdditionalFuelMass(fuelMass);
            Assert.Equal(fuelMass + additionalFuelMass, expectedFuelMass);
        }
    }
}
