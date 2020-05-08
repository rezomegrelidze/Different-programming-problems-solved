using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        private static Dictionary<int, char[]> telephone;
        private static char[] alphabet;
        static HashSet<string> words = new HashSet<string>();

        static void Main(string[] args)
        {
            alphabet = Enumerable.Range('A', 26).Select(i => (char)i).ToArray();
            ConstructTelephone();
            var input = "8862665";
            PrintAllCombinations(input,"");
            Console.WriteLine(words.Contains("TOOCOOL"));
        }
        private static void ConstructTelephone()
        {
            telephone = new Dictionary<int, char[]>();
            
            for (int i = 2; i <= 9; i++)
            {
                if (i != 7)
                {
                    telephone[i] = alphabet.Take(3).ToArray();
                    alphabet = alphabet.Skip(3).ToArray();
                }
                else
                {
                    telephone[i] = alphabet.Take(4).ToArray();
                    alphabet = alphabet.Skip(4).ToArray();
                }
            }
        }

        static void PrintAllCombinations(string number,string prefix)
        {
            if (string.IsNullOrEmpty(number))
            {
                Console.WriteLine(prefix);
                words.Add(prefix);
                return;
            }
            foreach (var digit in number.Select(x => int.Parse(x.ToString())))
            {
                for (int i = 1; i <= 3; i++)
                {
                    var c = GetCharKey(digit, i);
                    PrintAllCombinations(number.Substring(1),prefix + c);
                }
            }
        }


        static char GetCharKey(int telephoneKey, int place)
        {
            return telephone[telephoneKey][place - 1];
        }
    }

}
