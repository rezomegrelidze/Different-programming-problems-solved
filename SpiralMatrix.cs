using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CSharpScratchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = CreateMatrix(Enumerable.Range(1, 25).ToArray(),5,5);

            PrintMatrix(matrix);

            Console.WriteLine(string.Join(",",SpiralMatrix(matrix)));
        }

        private static int[,] CreateMatrix(int[] array,int width,int height)
        {
            var matrix = new int[width, height];

            for(int i = 0;i < width;i++)
                for (int j = 0; j < height; j++)
                    matrix[i, j] = array[i * width + j];
            return matrix;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}\t");
                }

                Console.WriteLine();
            }
        }


        private static IEnumerable<int> SpiralMatrix(int[,] matrix)
        {
            int rowSize = matrix.GetLength(0);
            int colSize = matrix.GetLength(1);
            int rowStart = 0;
            int colStart = 0;

            
            int count = 0;
            while (count < matrix.GetLength(0) * matrix.GetLength(1))
            {
                int i = rowStart, j = colStart;
                while (j < colSize)
                {
                    yield return matrix[i, j];
                    j++;
                    count++;
                }

                j = colSize - 1;

                rowStart++;
                i = rowStart;
                while (i < rowSize)
                {
                    yield return matrix[i, j];
                    i++;
                    count++;
                }

                i = rowSize - 1;
                colSize--;
                j = colSize - 1;

                while (j >= colStart)
                {
                    yield return matrix[i, j];
                    count++;
                    j--;
                }

                j = colStart;
                rowSize--;
                i = rowSize - 1;

                while (i >= rowStart)
                {
                    yield return matrix[i, j];
                    count++;
                    i--;
                }

                colStart++;
            }
        }
    }
}
