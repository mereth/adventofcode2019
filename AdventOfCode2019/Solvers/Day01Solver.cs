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
                var fuelMass = ComputeFuelMassWithAddedFuelMass(moduleMass);
                totalFuelMass += fuelMass;
            }

            return totalFuelMass.ToString();
        }

        public int ComputeFuelMass(int mass) => (mass / 3) - 2;

        public int ComputeFuelMassWithAddedFuelMass(int mass)
        {
            var fuelMass = ComputeFuelMass(mass);
            if (fuelMass >= 0)
            {
                return fuelMass + ComputeFuelMassWithAddedFuelMass(fuelMass);
            }
            return 0;
        }
    }
}