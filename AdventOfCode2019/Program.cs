using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AdventOfCode2019.Solvers;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var dayArg = args.SingleOrDefault();
            if (string.IsNullOrEmpty(dayArg))
            {
                Console.WriteLine($"Missing day parameter (eg. dotnet run 1)");
                return;
            }

            Console.WriteLine($"Solving day {dayArg}");

            var day = Convert.ToInt32(dayArg).ToString("0#");

            var inputs = LoadInputs(@$"Inputs\day{day}.txt");

            var solverType = Type.GetType($"AdventOfCode2019.Solvers.Day{day}Solver", true);
            var solver = Activator.CreateInstance(solverType) as ISolver;
            
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var answerPart1 = solver.SolvePart1(inputs);
            stopwatch.Stop();
            Console.WriteLine($"Answer Part 1: {answerPart1} in {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();
            var answerPart2 = solver.SolvePart2(inputs);
            stopwatch.Stop();
            Console.WriteLine($"Answer Part 2: {answerPart2} in {stopwatch.ElapsedMilliseconds}ms");
        }

        static IReadOnlyCollection<string> LoadInputs(string inputFilePath)
        {
            using (var reader = new StreamReader(inputFilePath))
            {
                return reader.ToEnumerable().ToList();
            }
        }
    }
}
