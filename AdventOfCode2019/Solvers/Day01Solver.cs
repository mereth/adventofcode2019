using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Solvers
{
    public class Day01Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> lines)
        {
            var totalFuelMass = 0;
            foreach(var line in lines)
            {
                var moduleMass = Convert.ToInt32(line);
                var fuelMass = ComputeFuelMass(moduleMass);
                totalFuelMass += fuelMass;
            }

            return totalFuelMass.ToString();
        }

        public string SolvePart2(IEnumerable<string> lines)
        {
            var totalFuelMass = 0;
            foreach(var line in lines)
            {
                var moduleMass = Convert.ToInt32(line);
                var fuelMass = ComputeFuelMass(moduleMass);
                var additionalFuelMass = ComputeAdditionalFuelMass(fuelMass);
                totalFuelMass += additionalFuelMass + fuelMass;
            }

            return totalFuelMass.ToString();
        }

        public int ComputeFuelMass(int mass) => (mass / 3) - 2;

        public int ComputeAdditionalFuelMass(int fuelMass)
        {
            var additionalFuelMass = ComputeFuelMass(fuelMass);
            if (additionalFuelMass >= 0)
            {
                return additionalFuelMass + ComputeAdditionalFuelMass(additionalFuelMass);
            }
            return 0;
        }
    }
}