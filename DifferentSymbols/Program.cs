using System;
using System.IO;
using System.Collections.Generic;

namespace DifferentSymbols
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            var text = "";

            if (args.Length == 1 && File.Exists(args[0]))
            {
                text = File.ReadAllText(args[0]);
            }
            else
            {
                Console.Write("Введите текст: ");
                text = Console.ReadLine();
            }

            var textSymbols = new HashSet<char>();

            foreach (var symbol in text)
            {
                textSymbols.Add(symbol);
            }

            Console.WriteLine($"\nРазличных символов в тексте: {textSymbols.Count}");

            foreach (var symbol in textSymbols)
            {
                Console.WriteLine($"{symbol}");
            }
        }
    }
}