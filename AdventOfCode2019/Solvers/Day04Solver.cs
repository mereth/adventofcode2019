using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Solvers
{
    public class Day04Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var edges = inputs.First().Split('-').ToArray();
            var min = edges[0];
            var max = edges[1];

            var count = GeneratePasswords(min, max, CheckPassword);

            return count.ToString();
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var edges = inputs.First().Split('-').ToArray();
            var min = edges[0];
            var max = edges[1];

            var count = GeneratePasswords(min, max, CheckPassword2);

            return count.ToString();
        }

        public int GeneratePasswords(string min, string max, Func<char[], bool> validate)
        {
            var password = min.ToArray();

            var count = 0;
            var last = password.Length - 1;
            do
            {
                if (validate(password))
                {
                    count++;
                }

                for (var i = last; i >= 0; i--)
                {
                    if (password[i] == '9')
                    {
                        continue;
                    }
                    else
                    {
                        password[i]++;
                        for (var j = i + 1; j <= last; j++)
                        {
                            password[j] = password[i];
                        }
                        break;
                    }
                }
            }
            while (string.CompareOrdinal((new String(password)), max) < 0);

            return count;
        }

        public bool CheckPassword(char[] password)
        {
            for (var i = 1; i < password.Length; i++)
            {
                if (password[i - 1] > password[i])
                    return false;

                if (password[i - 1] == password[i])
                    return true;
            }
            return false;
        }
        
        public bool CheckPassword2(char[] password)
        {
            var adjacents = 1;
            for (var i = 1; i < password.Length; i++)
            {
                if (password[i - 1] > password[i])
                    return false;

                if (password[i - 1] == password[i])
                {
                    adjacents++;
                }
                else
                {
                    if (adjacents == 2)
                    {
                        break;
                    }
                    adjacents = 1;
                }
            }
            return adjacents == 2;
        }
    }
}