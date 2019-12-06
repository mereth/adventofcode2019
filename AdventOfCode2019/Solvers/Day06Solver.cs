using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Solvers
{
    public class Day06Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var bodiesByName = BuildBodiesByNameMap(inputs);
            var checksum = bodiesByName.Values.Sum(body => body.ComputeDistanceFromRootNode());
            return checksum.ToString();
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var bodiesByName = BuildBodiesByNameMap(inputs);

            var you = bodiesByName["YOU"];
            var santa = bodiesByName["SAN"];

            var youAncestors = GetAncestors(you).ToArray();
            var santaAncestors = GetAncestors(santa).ToArray();
            for (var i = 0; i < youAncestors.Length; i++)
            {
                var fromYou = youAncestors[i];
                for (var j = 0; j < santaAncestors.Length; j++)
                {
                    var fromSanta = santaAncestors[j];
                    if (fromYou.Name == fromSanta.Name)
                    {
                        return (i + j).ToString();
                    }
                }
            }

            throw new ArgumentException("Boom!");
        }

        public Dictionary<string, Node> BuildBodiesByNameMap(IEnumerable<string> inputs)
        {
            var bodiesByName = new Dictionary<string, Node>();
            foreach(var input in inputs.Select(input => input.Split(')')))
            {
                var a = input[0];
                if (!bodiesByName.TryGetValue(a, out var node1))
                {
                    node1 = new Node { Name = a };
                    bodiesByName.Add(node1.Name, node1);
                }

                var b = input[1];
                if (!bodiesByName.TryGetValue(b, out var node2))
                {
                    node2 = new Node { Name = b };
                    bodiesByName.Add(node2.Name, node2);
                }

                node1.ChildNodes.Append(node2);
                node2.Parent = node1;
            }
            return bodiesByName;
        }

        public IEnumerable<Node> GetAncestors(Node node)
        {
            while (node.Parent != null)
            {
                yield return node.Parent;
                node = node.Parent;
            }
        }
    }

    public class Node
    {
        public string Name { get; set; }
        public Node Parent { get; set; }
        public IReadOnlyCollection<Node> ChildNodes { get; set; } = new List<Node>();

        public Node FindRootNode() => Parent == null ? this : Parent.FindRootNode();

        public int ComputeDistanceFromRootNode() => Parent == null ? 0 : 1 + Parent.ComputeDistanceFromRootNode();
    }
}