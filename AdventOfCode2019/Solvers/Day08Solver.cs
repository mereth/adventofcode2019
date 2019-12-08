using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Solvers
{
    public class Day08Solver : ISolver
    {
        public string SolvePart1(IEnumerable<string> inputs)
        {
            var encodedImage = inputs.First();

            var width = 25;
            var height = 6;
            var layerLength = width * height;

            var minNbZero = int.MaxValue;
            var layerWithFewestZero = string.Empty;

            for (var i = 0; i < encodedImage.Length; i = i + layerLength)
            {
                var layer = encodedImage.Substring(i, layerLength);
                var nbZero = layer.Count(pixel => pixel == '0');
                if (nbZero < minNbZero)
                {
                    minNbZero = nbZero;
                    layerWithFewestZero = layer;
                }
            }

            var answer = layerWithFewestZero.Count(pixel => pixel == '1') * layerWithFewestZero.Count(pixel => pixel == '2');

            return answer.ToString();
        }

        public string SolvePart2(IEnumerable<string> inputs)
        {
            var encodedImage = inputs.First();

            var width = 25;
            var height = 6;
            var imageSize = width * height;

            var transparent = '2';
            var image = string.Empty.PadLeft(imageSize, transparent).ToArray();

            for (var i = 0; i < encodedImage.Length; i = i + imageSize)
            {
                var layer = encodedImage.Substring(i, imageSize);
                
                for(var j = 0; j < imageSize; j++)
                {
                    if (image[j] == transparent && layer[j] != transparent)
                    {
                        image[j] = layer[j] == '0' ? ' ' : 'X';
                    }
                }
            }

            var builder = new StringBuilder();
            builder.AppendLine();
            for (var i = 0; i < imageSize; i = i + width)
            {
                builder.Append(image, i, width);
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}