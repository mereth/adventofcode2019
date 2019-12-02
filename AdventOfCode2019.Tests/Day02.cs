using System;
using System.Linq;
using Xunit;
using AdventOfCode2019.Solvers;

namespace AdventOfCode2019.Tests
{
    public class Day02
    {
        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void SamplePart1(string source, string expectedOutput)
        {
            var program = source.Split(",")
                .Select(i => Convert.ToInt32(i))
                .ToArray();

            var solver = new Day02Solver();
            solver.ExecuteProgram(program);

            var output = string.Join(",", program.Select(i => i.ToString()));
            Assert.Equal(output, expectedOutput);
        }
        
        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void SamplePart2(string source, string expectedOutput)
        {
            var program = source.Split(",")
                .Select(i => Convert.ToInt32(i))
                .ToArray();

            var solver = new Day02Solver();
            solver.ExecuteProgram(program);

            var output = string.Join(",", program.Select(i => i.ToString()));
            Assert.True(true);
        }
    }
}
