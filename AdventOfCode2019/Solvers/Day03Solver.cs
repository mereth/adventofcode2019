using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;

namespace AdventOfCode2019.Solvers
{
    public class Day03Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var wires = inputs
                .Select(input => ConvertVectorStringsToSegments(input.Split(',')).ToList())
                .ToList();

            var wire1 = wires[0];
            var wire2 = wires[1];

            var centralPort = new Point2D(0, 0);
            var minDistance = double.MaxValue;
            foreach (var segment1 in wire1)
            {
                foreach (var segment2 in wire2)
                {
                    if (segment1.TryIntersect(segment2, out var intersection, Angle.FromDegrees(0)) && intersection != centralPort)
                    {
                        var distance = Math.Abs(intersection.X) + Math.Abs(intersection.Y);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                        }
                    }
                }
            }

            return minDistance.ToString();
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var wires = inputs
                .Select(input => ConvertVectorStringsToSegments(input.Split(',')).ToList())
                .ToList();

            var wire1 = wires[0];
            var wire2 = wires[1];

            var centralPort = new Point2D(0, 0);
            var minDistance = double.MaxValue;

            var wire1steps = 0d;
            foreach (var segment1 in wire1)
            {
                var wire2steps = 0d;
                foreach (var segment2 in wire2)
                {
                    if (segment1.TryIntersect(segment2, out var intersection, Angle.FromDegrees(0)) && intersection != centralPort)
                    {
                        var distance1 = wire1steps + (new LineSegment2D(segment1.StartPoint, intersection)).Length;
                        var distance2 = wire2steps + (new LineSegment2D(segment2.StartPoint, intersection)).Length;
                        var distance = distance1 + distance2;

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                        }
                    }

                    wire2steps += segment2.Length;
                }

                wire1steps += segment1.Length;
            }

            return minDistance.ToString();
        }

        private IEnumerable<LineSegment2D> ConvertVectorStringsToSegments(IEnumerable<string> vectorStrings)
        {
            var origin = new Point2D(0, 0);
            foreach (var vectorString in vectorStrings)
            {
                var vector = ConvertVectorStringToVector(vectorString);
                var end = origin + vector;
                yield return new LineSegment2D(origin, end);
                origin = end;
            }
        }

        private Vector2D ConvertVectorStringToVector(string vectorString)
        {
            var direction = vectorString[0];
            var distance = Convert.ToInt32(vectorString.Substring(1));

            switch (direction)
            {
                case 'U':
                    return new Vector2D(0, distance);
                case 'D':
                    return new Vector2D(0, -distance);
                case 'R':
                    return new Vector2D(distance, 0);
                case 'L':
                    return new Vector2D(-distance, 0);
                default:
                    throw new Exception($"Unsupported direction: {direction}");
            }
        }
    }
}