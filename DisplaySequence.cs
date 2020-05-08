using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = new object[] {new[] {1, 2, 3}, new object[] {new object[] {}, null, 1},new[]{new[]{new[]{new[]{1}}}}};
            DisplaySequence(sequence);
        }

        static void DisplaySequence(IEnumerable sequence)
        {
            var objects = sequence.Cast<object>();
            string PrintCommaIfNotFinalElement(object element)
            {
                return ((!objects.Last()?.Equals(element) ?? (objects.Last() != null)) ? "," : "");
            }

            foreach (var element in objects)
            {
                
                if (element is IEnumerable subsequence)
                {
                    Console.Write("[");
                    DisplaySequence(subsequence);
                    Console.Write("]");
                    Console.Write(PrintCommaIfNotFinalElement(element));
                }
                else
                {
                    Console.Write($" {element ?? "null"} " + PrintCommaIfNotFinalElement(element));
                }
            }
            
        }
    }

}
