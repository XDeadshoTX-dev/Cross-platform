﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] inputLines = File.ReadAllLines("INPUT.TXT");
                string p1 = inputLines[0].ToLower();
                string p2 = inputLines[1].ToLower();
                int sizeP1 = p1.Length;
                int sizeP2 = p2.Length;

                Console.WriteLine($"[Input] p1: {p1} (Length: {sizeP1}), p2: {p2} (Length: {sizeP2})");

                if (sizeP2 > 9 || sizeP2 > 9)
                {
                    throw new ArgumentException("More than 9 characters!");
                }
                else if(sizeP1 != sizeP2)
                {
                    throw new ArgumentException("Length of characters is not equal!");
                }
                Console.WriteLine("[Ok] Length of characters is ok");

                int commonStrings = GetResult(p1, p2);

                Console.WriteLine($"[Output] Common strings count: {commonStrings}");
                File.WriteAllText("OUTPUT.TXT", commonStrings.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
        }
        public static int GetResult(string p1, string p2)
        {
            // Конвертація символів
            List<char[]> buffer1 = new List<char[]>();
            List<char[]> buffer2 = new List<char[]>();

            foreach (char symbol in p1)
            {
                buffer1.Add(GetReplace(symbol));
            }
            foreach (char symbol in p2)
            {
                buffer2.Add(GetReplace(symbol));
            }
            // Створення усіх комбінацій

            int[] amount = new int[p1.Length];

            for (int i = 0; i < p1.Length; i++) // поточний індекс
            {
                foreach (char symbol1 in buffer1[i])
                {
                    if (buffer2[i].Contains(symbol1))
                    {
                        amount[i]++;
                    }
                }
            }
            // Підрахунок кількості спільних комбінацій
            int result = 1;
            foreach (int count in amount)
            {
                result *= count;
            }
            return result;
        }
        private static char[] GetReplace(char symbol)
        {
            switch (symbol)
            {
                case '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9':
                    return new char[] { symbol };
                case 'a':
                    return "0123".ToCharArray();
                case 'b':
                    return "1234".ToCharArray();
                case 'c':
                    return "2345".ToCharArray();
                case 'd':
                    return "3456".ToCharArray();
                case 'e':
                    return "4567".ToCharArray();
                case 'f':
                    return "5678".ToCharArray();
                case 'g':
                    return "6789".ToCharArray();
                case '?':
                    return "0123456789".ToCharArray();
                default:
                    throw new ArgumentException($"Unknown symbol: {symbol}");
            }
        }
    }
}
