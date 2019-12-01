using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019
{
    public static class TextReaderExtensions
    {
        public static IEnumerable<string> ToEnumerable(this TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}